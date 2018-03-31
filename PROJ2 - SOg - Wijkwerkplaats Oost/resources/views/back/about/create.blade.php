@extends('back.layout')

@section('title', 'Over ons pagina aanmaken')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        <input id="old-section" name="old-section" type="hidden" class="form-control" value="{{ $section }}">
        <div class="form-group">
            <label for="section">Naam</label>
            <input id="section" name="section" type="text" class="form-control" value="{{ $section }}">
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-2 col-md-offset-4 col-sm-2 col-sm-offset-4 col-xs-6 col-xs-offset-0">
                    <input type="submit" class="btn btn-primary" value="Opslaan"/>
                </div>
                <div class="col-md-2 col-md-offset-0 col-sm-2 col-sm-offset-0 col-xs-6 col-xs-offset-0">
                    <a href="{{ route('back.about.index') }}" class="btn btn-default pull-xs-right">Annuleer</a>
                </div>
            </div>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een nieuw kopje maken. Vul de titel in van het nieuwe kopje en klik op 'Opslaan'.");
@endsection