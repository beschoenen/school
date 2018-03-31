@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Tours
                    </div>
                    <div class="panel-body horizontally-scrollable">
                        <a href="{{ route('tours.create') }}" type="button" class="btn btn-default pull-right">
                            Tour <i class="fa fa-plus"></i>
                        </a>
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Tour naam</th>
                                    <th>Aangemaakt op</th>
                                    <th>Aangepast op</th>
                                    <th class="table-details"></th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach($tours as $tour)
                                    <tr>
                                        <td>{{ $tour->name }}</td>
                                        <td>{{ $tour->created_at }}</td>
                                        <td>{{ $tour->updated_at }}</td>
                                        <td>
                                            <a href="{{ route('tours.show', $tour->id) }}">Details</a>
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
