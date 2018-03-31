<?php

namespace App\Http\Controllers;

use App\Models\Tour;
use App\Models\Beacon;

class APIController extends Controller
{
    public function tours()
    {
        return Tour::all();
    }

    public function tour(Tour $tour)
    {
        $foundBeacons = Beacon::all();

        foreach ($tour->nodes as $node) {
            $node->beacon = $foundBeacons->first(function ($beacon) use ($node) {
                return $beacon->beacon->_id == $node->node;
            });
        }

        return $tour;
    }
}
