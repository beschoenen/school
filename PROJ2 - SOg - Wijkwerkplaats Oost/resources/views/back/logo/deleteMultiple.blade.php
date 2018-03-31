@extends('back.layout')

@section('title', 'Logo\'s verwijderen')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        @foreach($rows as $row)
            <div class="row">
                @foreach($row as $logo)
                    <div class="col-md-4">
                        <img class="delete-image" alt="{{ $logo->name }}" src="{{ route('misc.logo', $logo->id) }}"/>
                        <input type="hidden" name="logo[]" value="{{ $logo->id }}"/>
                    </div>
                @endforeach
            </div>
        @endforeach
        <div class="row">
            <div class="col-md-12">
                <span class="delete-notify">Weet u zeker dat u deze logos wilt verwijderen?</span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-md-offset-4 col-sm-2 col-sm-offset-4 col-xs-6 col-xs-offset-0">
                <input type="submit" class="btn btn-danger" value="Verwijder"/>
            </div>
            <div class="col-md-2 col-md-offset-0 col-sm-2 col-sm-offset-0 col-xs-6 col-xs-offset-0">
                <a href="{{ $return }}" class="btn btn-default pull-xs-right">Annuleer</a>
            </div>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je meerdere logo's verwijderen. Als je zeker weet dat je deze logo's wil verwijderen klik dan op 'Verwijder'.");
@endsection

@push('css')
    <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/photos.css') }}"/>
@endpush