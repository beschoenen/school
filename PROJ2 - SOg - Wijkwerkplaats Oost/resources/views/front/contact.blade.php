@extends('front.designs.splitPage')

@section('title', 'Contact')

@section('left')
    <div class="flexible-container">
        {!! $left !!}
    </div>
@endsection

@section('right')
    <div class="edit-container">
        <div @if($editMode)data-editable data-text data-save="{{ route('back.api.text') }}" data-section="contact.right"@endif>
            {!! $right !!}
        </div>
    </div>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je de tekst aanpassen op deze pagina. Klik op een de tekst om deze aan te passen of klik op 'Ga terug' om terug te gaan naar het overzicht.");
@endsection