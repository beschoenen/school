@extends('back.layout')

@section('title', 'Over ons pagina beheren')

@section('content')
    <div class="row">
        <a href="{{ route('back.about.create') }}" class="btn btn-primary createbutton">Nieuw</a>

        <table class="table table-striped">
            @foreach($sections as $section)
                <tr>
                    <td>{{ $section->section }}</td>
                    <td><a href="{{ route('back.about.edit', $section->section) }}" class="btn btn-primary pull-right">Naam aanpassen</a></td>
                    <td><a href="{{ route('front.about', $section->section) }}?edit=true" class="btn btn-primary pull-right">Tekst aanpassen</a></td>
                    <td><a href="{{ route('back.about.delete', $section->section) }}" class="btn btn-danger pull-right">Verwijderen</a></td>
                </tr>
            @endforeach
        </table>
    </div>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de kopjes van de Over ons pagina aanpassen. Klik op 'Nieuw' voor een nieuw kopje.");
@endsection
@push('css')
<link rel="stylesheet" type="text/css" href="{{ asset('assets/css/back-activity.css') }}"/>
@endpush