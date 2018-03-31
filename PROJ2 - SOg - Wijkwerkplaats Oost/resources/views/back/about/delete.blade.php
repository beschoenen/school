@extends('back.layout')

@section('title', 'Pagina verwijderen')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label for="section">Naam</label>
                    <input id="section" name="section" type="text" class="form-control" value="{{ $section->section }}" readonly>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <p>Weet u zeker dat u deze pagina wilt verwijderen?</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-md-offset-4 col-sm-2 col-sm-offset-4 col-xs-6 col-xs-offset-0">
                <input type="submit" class="btn btn-danger" value="Verwijder"/>
            </div>
            <div class="col-md-2 col-md-offset-0 col-sm-2 col-sm-offset-0 col-xs-6 col-xs-offset-0">
                <a href="{{ route('back.about.index') }}" class="btn btn-default pull-xs-right">Annuleer</a>
            </div>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een kopje verwijderen. Als je zeker weet dat je dit kopje wil verwijderen klik dan op 'Verwijder'.");
@endsection