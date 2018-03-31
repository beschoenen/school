@extends('front.designs.menuPage')

@section('title', 'Activiteiten')

@section('menu-items')
    <li><a href="{{ route('front.activities') }}">Algemeen</a></li>
    @foreach($activities as $activity)
        <li><a href="{{ route('front.activities', $activity->section) }}">{{ $activity->section }}</a></li>
    @endforeach
@endsection

@section('topic')
    <div class="edit-container">
        <div @if($editMode)data-editable data-text data-save="{{ route('back.api.text') }}" data-section="{{ $section }}"@endif>
            {!! $text !!}
        </div>
    </div>
@endsection

@section('image')
    @include('fragments.image_carousel')
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de tekst en foto's aanpassen op deze pagina. Klik op een onderdeel om deze aan te passen. Wil je de tekst van een ander kopje aanpassen klik hier dan eerst op. Klik op 'Ga terug' om terug te gaan naar het overzicht, hier kunnen de kopjes worden aangepast.");
@endsection