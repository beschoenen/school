@extends('back.layout')

@section('title', 'Activiteitenpagina beheren')

@section('content')
    <div class="row">
        <a href="{{ route('back.activities.create') }}" class="btn btn-primary createbutton">Nieuw</a>

        <table class="table table-striped">
            @foreach($activities as $activity)
                <tr>
                    <td>{{ $activity->section }}</td>
                    <td><a href="{{ route('back.activities.edit', $activity->section) }}" class="btn btn-primary pull-right">Naam aanpassen</a></td>
                    <td><a href="{{ route('front.activities', $activity->section) }}?edit=true" class="btn btn-primary pull-right">Tekst aanpassen</a></td>
                    <td><a href="{{ route('back.activities.delete', $activity->section) }}" class="btn btn-danger pull-right">Verwijderen</a></td>
                </tr>
            @endforeach
        </table>
    </div>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de kopjes van de Activiteiten pagina aanpassen. Klik op 'Nieuw' voor een nieuw kopje.");
@endsection
@push('css')
<link rel="stylesheet" type="text/css" href="{{ asset('assets/css/back-activity.css') }}"/>
@endpush