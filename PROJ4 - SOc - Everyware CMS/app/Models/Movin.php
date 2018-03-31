<?php

namespace App\Models;

use GuzzleHttp\Client;

abstract class Movin
{
    /**
     * @var string
     */
    private static $host = 'https://bartimeus-c.movin.io';

    private static function makeRequest(string $route, array $queryParams = [], array $body = [], string $method = 'GET'): string
    {
        $route = ltrim($route, '/');

        $queryParams['apikey'] = getenv('MOVIN_API_KEY');

        $url = self::$host."/$route?".http_build_query($queryParams);

        $options = [
            'headers' => ['Content-Type' => 'application/json'],
        ];

        if (count($body) > 0) {
            $options['body'] = json_encode($body);
        }

        $request = (new Client(['verify' => false]))->request($method, $url, $options);

        return (string) $request->getBody();
    }

    protected static function get(string $route, array $queryParams = [])
    {
        return self::makeRequest($route, $queryParams);
    }

    protected static function post(string $route, array $properties = [])
    {
        return self::makeRequest($route, [], $properties, 'POST');
    }
}
