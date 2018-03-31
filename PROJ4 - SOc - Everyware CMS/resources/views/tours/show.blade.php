@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Tour
                        <span class="pull-right">
                            <a href="{{ route('tours.edit', $tour->id) }}">
                                <i class="fa fa-pencil"></i>
                            </a>
                            &nbsp
                            <i class="fa fa-trash clickable" data-toggle="modal" data-target="#delete-modal"></i>
                        </span>
                    </div>
                    <div class="panel-body horizontally-scrollable">
                        <table class="table table-striped">
                            <tr>
                                <th>Tour ID</th>
                                <td>{{ $tour->id }}</td>
                                <th>Tour name</th>
                                <td>{{ $tour->name }}</td>
                            </tr>
                            <tr>
                                <th>Gesproken tekst</th>
                                <td>{{ $tour->spoken_description }}</td>
                                <th>Geschreven tekst</th>
                                <td>{{ $tour->written_description }}</td>
                            </tr>
                            <tr>
                                <th>Aanmaak datum</th>
                                <td>{{ $tour->created_at }}</td>
                                <th>Laatste update</th>
                                <td>{{ $tour->updated_at }}</td>
                            </tr>
                        </table>
                        <br>
                        <hr>
                        <h3>Tour punten</h3>
                        <table class="table table-striped">
                            @foreach($tour->nodes as $node)
                                <tr>
                                    <th colspan="4">Punt #{{ $loop->iteration }}</th>
                                </tr>
                                <tr>
                                    <th>Punt Naam (Beacon naam)</th>
                                    <td>{{ getBeaconName($beacons, $node) }}</td>
                                    <th>Richting</th>
                                    <td>{{ getDirectionArrow($node) }}</td>
                                </tr>
                                <tr>
                                    <th>Gesproken tekst</th>
                                    <td>{{ $node->spoken_description }}</td>
                                    <th>Geschreven beschrijving</th>
                                    <td>{{ $node->written_description ?? '' }}</td>
                                </tr>
                            @endforeach
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    @include('tours.deleteModal')
@endsection
