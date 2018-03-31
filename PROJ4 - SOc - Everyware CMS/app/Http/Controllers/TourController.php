<?php

namespace App\Http\Controllers;

use App\Models\Tour;
use App\Models\Beacon;
use App\Models\TourNode;
use App\Http\Requests\Tour\TourCreateRequest;
use App\Http\Requests\Tour\TourUpdateRequest;

class TourController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }

    public function index()
    {
        return view('tours.index', [
            'tours' => Tour::all(),
        ]);
    }

    public function create()
    {
        return view('tours.create');
    }

    public function show(Tour $tour)
    {
        return view('tours.show', [
            'tour' => $tour,
            'beacons' => Beacon::all(),
        ]);
    }

    public function store(TourCreateRequest $request)
    {
        $tour = Tour::create($request->except(['_method', '_token']));

        return redirect(route('tours.show', $tour->id));
    }

    public function edit(Tour $tour)
    {
        return view('tours.edit', [
            'tour' => $tour,
            'beacons' => Beacon::all(),
        ]);
    }

    public function update(TourUpdateRequest $request, Tour $tour)
    {
        $tour->update($request->except(['_method', '_token', 'nodes']));

        TourNode::where('tour_id', $tour->id)->delete();

        for ($i = 0; $i < count($request->nodes); $i++) {
            $node = $request->nodes[$i];

            TourNode::create(array_merge($node, [
                'tour_id' => $tour->id,
                'is_start' => ! $i,
                'next_node' => $request->nodes[$i + 1]['node'] ?? null,
            ]));
        }

        return redirect(route('tours.show', $tour->id));
    }

    public function destroy(Tour $tour)
    {
        $tour->delete();

        return redirect(route('tours.index'));
    }
}
