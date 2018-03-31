<?php

namespace App\Http\Controllers;

use App\Models\Beacon;
use App\Http\Requests\Beacon\BeaconUpdateRequest;

class BeaconController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }

    public function index()
    {
        return view('beacons.index', [
            'beacons' => Beacon::all(),
        ]);
    }

    public function show($id)
    {
        return view('beacons.show', [
            'beacon' => Beacon::find($id),
            'beacons' => Beacon::all(),
        ]);
    }

    public function edit($id)
    {
        return view('beacons.edit', [
            'beacon' => Beacon::find($id),
            'beacons' => Beacon::all(),
        ]);
    }

    public function update(BeaconUpdateRequest $request, $id)
    {
        if (! Beacon::update($id, $request->except(['_method', '_token']))) {
            return redirect()->back()->withErrors('Er ging iets fout tijdens het updaten');
        }

        return redirect(route('beacons.show', $id));
    }

    public function destroy($id)
    {
        if (! Beacon::clearData($id)) {
            return redirect()->back()->withErrors('Er ging iets fout tijdens het verwijderen');
        }

        return redirect(route('beacons.index'));
    }
}
