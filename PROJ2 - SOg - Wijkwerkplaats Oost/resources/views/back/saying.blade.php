@extends('back.layout')

@section('title', 'Spreuk bewerken')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        <div class="form-group">
            <label for="description">Spreuk</label>
            <input type="text" class="form-control" name="text" title="saying" value="{{ $text }}"/>
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hieronder kan je de spreuk van het jaar aanpassen, verander de tekst en klik op opslaan.");
@endsection