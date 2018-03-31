@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                @if($errors->any())
                    <div class="alert alert-danger">
                        <ul>
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Beacon
                        <span class="pull-right">
                            <a href="{{ route('beacons.edit', $beacon->beacon->_id) }}">
                                <i class="fa fa-pencil"></i>
                            </a>
                            &nbsp;
                            <i class="fa fa-trash clickable" data-toggle="modal" data-target="#delete-modal"></i>
                        </span>
                    </div>
                    <div class="panel-body horizontally-scrollable">
                        <table class="table table-striped">
                            <tr>
                                <th>Beacon ID</th>
                                <td>{{ $beacon->beacon->_id }}</td>
                                <th>Beacon Code</th>
                                <td>{{ $beacon->beacon->beaconCode ?? '' }}</td>
                            </tr>
                            <tr>
                                <th>Status</th>
                                <td>{{ $beacon->beacon->status ?? '' }}</td>
                                <th>Major</th>
                                <td>{{ $beacon->beacon->major ?? '' }}</td>
                            </tr>
                            <tr>
                                <th>Fabrikant</th>
                                <td>{{ $beacon->beacon->manufacturer ?? '' }}</td>
                                <th>Minor</th>
                                <td>{{ $beacon->beacon->minor ?? '' }}</td>
                            </tr>
                            <tr>
                                <th>Verdieping</th>
                                <td>{{ $beacon->beacon->floor ?? '' }}</td>
                                <th>Tag</th>
                                <td>{{ $beacon->beacon->tag ?? '' }}</td>
                            </tr>
                        </table>
                        <br>
                        <hr>
                        <h3>Bartiméus data</h3>
                        <table class="table table-striped">
                            <tr>
                                <td>Beacon naam</td>
                                <td colspan="3">{{ $beacon->data->name ?? '' }}</td>
                            </tr>
                            @foreach($beacon->data->pointsOfInterest ?? [] as $pointOfInterest)
                                <tr>
                                    <th colspan="4">POI #{{ $loop->iteration }}</th>
                                </tr>
                                <tr>
                                    <th>Naam</th>
                                    <td>{{ $pointOfInterest->name ?? '' }}</td>
                                    <th>Richting</th>
                                    <td>{{ $pointOfInterest->direction * 10 . '°' ?? '' }}</td>
                                </tr>
                                <tr>
                                    <th>Beschrijving</th>
                                    <td>{{ $pointOfInterest->description ?? '' }}</td>
                                    <th>Gesproken beschrijving</th>
                                    <td>{{ $pointOfInterest->spokenDescription ?? '' }}</td>
                                </tr>
                            @endforeach
                            @foreach($beacon->data->linkedBeacon ?? [] as $linkedBeacon)
                                <tr>
                                    <th colspan="4">Linked beacon #{{ $loop->iteration }}</th>
                                </tr>
                                <tr>
                                    <th>Naam</th>
                                    @foreach($beacons as $b)
                                        @isset($b->data->name)
                                            @if($b->beacon->_id == $linkedBeacon->beacon)
                                                <td>{{ $b->data->name }}</td>
                                            @endif
                                        @endisset
                                    @endforeach
                                </tr>
                                @foreach($linkedBeacon->instructions ?? [] as $instruction)
                                    <tr></tr>
                                    <tr>
                                        <th colspan="4">Instructie #{{ $loop->iteration }}</th>
                                    </tr>
                                    <tr>
                                        <th>Vanaf</th>
                                        @foreach($beacons as $b)
                                            @isset($b->data->name)
                                                @if($b->beacon->_id == $instruction->from)
                                                    <td>{{ $b->data->name }}</td>
                                                @endif
                                            @endisset
                                        @endforeach
                                        <th>Richting</th>
                                        <td>{{ $instruction->direction * 50 . '°' ?? '' }}</td>
                                    </tr>
                                    <tr>
                                        <th>Instruction</th>
                                        <td>{{ $instruction->instruction ?? '' }}</td>
                                    </tr>
                                @endforeach
                            @endforeach
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    @include('beacons.deleteModal')
@endsection
