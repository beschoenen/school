<?php

namespace App\Models;

use Illuminate\Support\Collection;
use Illuminate\Support\Facades\Cache;

class Beacon extends Movin
{
    /**
     * Basic beacon info from Movin.
     *
     * @var \stdClass
     */
    public $beacon;

    /**
     * The extra information for a beacon.
     *
     * @var \stdClass
     */
    public $data;

    /**
     * The map information for the beacon.
     *
     * @var \stdClass
     */
    private $map;

    private function __construct(array $properties = [])
    {
        $this->beacon = new \stdClass();

        foreach ($properties as $key => $value) {
            if ($key === 'description') {
                $this->setUpData($value);
                continue;
            }

            $this->beacon->{$key} = $value;
        }
    }

    public static function all(): Collection
    {
        $response = self::get('data/beacons/getbeacongroups', [
            'mapid' => '5694baa8f7edd56dd43877fa',
            'includebeacons' => 'true',
        ]);

        $body = json_decode($response, true);

        if (! $body['success']) {
            throw new \Exception($body['errorMessage']);
        }

        return collect($body['beaconGroups'][0]['beacons'])->map(function ($beacon) {
            return new self($beacon);
        });
    }

    public static function find(string $id): Beacon
    {
        $response = self::get('data/beacons/getbeacon', [
            'beaconid' => $id,
        ]);

        $body = json_decode($response, true);

        return new self($body['beacon']);
    }

    public function save(): bool
    {
        $response = self::post('data/beacons/updatebeacon', [
            '_id' => $this->beacon->_id,
            'description' => json_encode($this->data),
        ]);

        return json_decode($response)->success;
    }

    public static function update(string $id, array $properties = [])
    {
        $response = self::post('data/beacons/updatebeacon', [
            '_id' => $id,
            'description' => json_encode($properties),
        ]);

        return json_decode($response)->success;
    }

    public static function clearData(string $id): bool
    {
        $response = self::post('data/beacons/updatebeacon', [
            '_id' => $id,
            'description' => null,
        ]);

        return json_decode($response)->success;
    }

    public function map(bool $reload = false): \stdClass
    {
        if ($reload) {
            Cache::tags('maps')->forget("map.{$this->beacon->mapID}");
        }

        return Cache::tags('maps')->remember("map.{$this->beacon->mapID}", 30, function () {
            $this->map = new \stdClass();

            $response = self::get('data/maps/getmap', [
                'mapid' => $this->beacon->mapID,
            ]);

            $response = json_decode($response);

            if (! $response->success) {
                return $this->map;
            }

            $this->map = $response->map->properties;

            return $this->map;
        });
    }

    //////////////////
    // Helpers methods

    private function setUpData($data)
    {
        $this->data = new \stdClass();

        $json = @json_decode($data);

        if ((bool) json_last_error()) {
            return;
        }

        foreach ($json as $key => $value) {
            $this->data->{$key} = $value;
        }
    }
}
