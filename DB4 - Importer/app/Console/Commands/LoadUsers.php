<?php

namespace App\Console\Commands;

use Illuminate\Console\Command;
use Illuminate\Support\Facades\DB;
use Mockery\Exception;

class LoadUsers extends Command
{
    protected $signature = 'load:users {user} {limit}';

    protected $url = "https://api.twitter.com/1.1/users/show.json?user_id=";

    protected $tokens = [];

    public function __construct()
    {
        $this->tokens = json_decode(file_get_contents('tokens.json'));

        parent::__construct();
    }

    public function handle()
    {
        $users = DB::table('users')
            ->where('id', '>=', $this->argument('user'))
            ->where('id', '<', intval($this->argument('user')) + intval($this->argument('limit')))
            ->where('scraped', false)
            ->limit($this->argument('limit'))
            ->get();

        $this->output->progressStart(count($users));

        for ($i = 0; $i < count($users); $i++) {
            $token = $this->tokens[$i % count($this->tokens)];
            $user = $users[$i];

            $json = $this->getUser($user->twitter_id, $token);

            $location = isset($json->location) ? $json->location : null;

            DB::table('users')->where('id', $user->id)->update([
                'location' => $this->getLocation($location),
                'original_location' => $this->clean($location),
                'scraped' => true
            ]);

            $this->output->progressAdvance();
        }

        $this->output->progressFinish();
    }

    private function getUser($id, $token)
    {
        $success = false;

        $json = null;

        while (!$success) {
            $ch = curl_init($this->url . $id);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
            curl_setopt($ch, CURLOPT_HTTPHEADER, [
                "Authorization: Bearer $token"
            ]);

            $output = curl_exec($ch);
            curl_close($ch);

            $json = json_decode($output);

            if (isset($json->errors)) {
                if ($json->errors[0]->message == "Rate limit exceeded") {
                    $this->output->text($json->errors[0]->message);
                    $this->output->text("Sleeping for 1 minute");
                    sleep(60);

                    continue;
                }
            }

            $success = true;
        }

        return $json;
    }

    private function clean($value)
    {
        $value = trim($value);

        if (empty($value)) {
            return null;
        }

        return $value;
    }

    private function getLocation($location)
    {
        $location = $this->clean($location);

        if (empty($location)) {
            return null;
        }

        $response = null;

        $counter = 0;
        while (empty($response) && $counter < 5) {
            if ($counter > 0) {
                $this->output->text("Try " . ($counter + 1));
            }

            $response = @file_get_contents(
                'http://nominatim.openstreetmap.org/search?format=json&accept-language=en&addressdetails=1&q=' .
                urlencode($location)
            );

            $counter++;
        }

        if (empty($response)) {
            throw new Exception("I did my best, 5 tries");
        }

        sleep(1);

        $json = json_decode($response);

        if (count($json) < 1) {
            return null;
        }

        $country = isset($json[0]->address->country) ? $json[0]->address->country : null;

        return $country;
    }
}
