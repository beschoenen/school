@extends('back.logo.create')

@section('title', 'Bewerk logo')

@section('image')
    <label for="image">Logo</label><br/>
    <img alt="foto" style="height: 100px;" src="{{ route('misc.logo', $logo->id) }}"/>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een logo wijzigen. Verander de naam en url. Als je klaar bent klik op 'Opslaan'.");
@endsection