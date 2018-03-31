@extends('back.photo.create')

@section('title', 'Bewerk foto')

@section('image')
    <label for="image">Foto</label><br/>
    <img alt="foto" style="height: 100px;" src="{{ route('misc.thumb', $photo->id) }}"/>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een foto wijzigen. Verander de naam en beschrijving. Als je klaar bent klik op 'Opslaan'.");
@endsection