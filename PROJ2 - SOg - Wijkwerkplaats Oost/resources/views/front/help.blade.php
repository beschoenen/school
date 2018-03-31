@extends('front.designs.menuPage')

@section('title', 'Hulp')

@section('menu-items')
    <li><a href="{{ route('front.help') }}">Algemeen</a></li>
    <li><a href="{{ route('front.help','sponsors') }}">Sponsoren</a></li>
    <li><a href="{{ route('front.help','volunteers') }}">Vrijwilligers</a></li>
    <li><a href="{{ route('front.help','trainees') }}">Stagiaires</a></li>
    <li><a href="{{ route('front.vacancies') }}">Vacatures</a></li>
@endsection

@section('topic')
    <div class="edit-container">
        <div @if($editMode)data-editable data-text data-save="{{ route('back.api.text') }}" data-section="{{ $section }}"@endif>
            {!! $topic !!}
        </div>
    </div>
@endsection

@section('image')
    @include('fragments.image_carousel')
@endsection

@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de tekst en foto's aanpassen op deze pagina. Wil je de tekst van een ander kopje aanpassen klik hier dan eerst op. Klik op 'Ga terug' om terug te gaan naar het overzicht, hier kunnen de vacatures worden aangepast.");
@endsection
