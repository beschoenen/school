<?php

use App\Models\TourNode;
use Illuminate\Support\Collection;

function getBeaconName(Collection $list, TourNode $node)
{
    $beacon = $list->first(function ($beacon) use ($node) {
        return $beacon->beacon->_id == $node->node;
    });

    return $beacon->data->name;
}

function getDirectionArrow(TourNode $node)
{
    switch ($node->direction) {
        case 0:
            return '↑';
        case 1:
            return '↗';
        case 2:
            return '↱';
        case 3:
            return '↘';
        case 4:
            return '↓';
        case 5:
            return '↙';
        case 6:
            return '↰';
        case 7:
            return '↖';
        case 8:
            return '○';
        default:
            return 'X';
    }
}

function getActiveClass($route)
{
    return strpos(request()->getUri(), $route) !== false ? 'active' : '';
}
