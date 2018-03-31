@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading">Beacons</div>
                    <div class="panel-body horizontally-scrollable">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Naam</th>
                                    <th>Locatie</th>
                                    <th>Verdieping</th>
                                    <th>Code</th>
                                    <th>Minor</th>
                                    <th>Major</th>
                                    <th class="table-details"></th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach($beacons as $beacon)
                                    <tr>
                                        <td>{{ $beacon->data->name ?? '' }}</td>
                                        <td>{{ $beacon->map()->name ?? '' }}</td>
                                        <td>{{ $beacon->beacon->floor ?? '' }}</td>
                                        <td>{{ $beacon->beacon->beaconCode ?? '' }}</td>
                                        <td>{{ $beacon->beacon->minor ?? '' }}</td>
                                        <td>{{ $beacon->beacon->major ?? '' }}</td>
                                        <td>
                                            <a href="{{ route('beacons.show', $beacon->beacon->_id) }}">Details</a>
                                        </td>
                                    </tr>
                                @endforeach
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
