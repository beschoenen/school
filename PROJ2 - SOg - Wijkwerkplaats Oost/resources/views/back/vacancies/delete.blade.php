@extends('back.layout')

@section('title', 'Verwijder vacature')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="row">
            <div class="col-md-12">
                <span class="delete-notify">Weet u zeker dat u deze vacature wilt verwijderen?</span>
            </div>
        </div>
        <br>
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
    $("#tooltip").attr("data-original-title", "Hier kun je een vacature verwijderen. Als je zeker weet dat je deze vacature wil verwijderen klik dan op 'Verwijder'.");
@endsection