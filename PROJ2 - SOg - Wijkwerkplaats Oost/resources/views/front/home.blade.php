@extends('front.designs.splitPage')

@section('title', 'Thuis')

@section('left')
    <div class="edit-container">
        <div @if($editMode)data-editable data-text data-save="{{ route('back.api.text') }}" data-section="home.main"@endif>
            {!! $text !!}
        </div>
    </div>
@endsection

@section('right')
    @include('fragments.image_carousel')
@endsection

@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de tekst en foto's aanpassen op deze pagina. Klik op een onderdeel om deze aan te passen of klik op 'Ga terug' om terug te gaan naar het overzicht.");
@endsection