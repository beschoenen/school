@extends('back.layout')

@section('title', 'Instellingen')

@section('content')
    <h1>Algemene instellingen</h1>
    <form method="POST" action="{{ route('back.settings') }}">
        {!! csrf_field() !!}
        <div class="form-group">
            <input type="checkbox" name="pref_form" id="pref_form"{{ $contact_form_enabled->value == "1" ? ' checked' : '' }}/> <label for="pref_form">Email ontvangen via contactformulier</label>
        </div>
        <div class="form-group">
            <label>Emailadres:</label>
            <input class="form-control" type="text" name="email" value="{{ $contact_form_email->value }}"/>
        </div>
        <div class="form-group">
            <input type="checkbox" name="backend_color" id="backend_color"{{ $backend_color_scheme->value == "1" ? ' checked' : '' }}/> <label for="backend_color">Dezelfde kleuren gebruiken voor beheer als normale website</label>
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "'Hier kun je het e-mail adres veranderen waar contact mee wordt opgenomen voor de website. Vul het juiste e-mail adres in en klik op 'Opslaan'.");
@endsection