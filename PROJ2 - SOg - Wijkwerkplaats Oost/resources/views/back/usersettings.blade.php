@extends('back.layout')

@section('title', 'Wachtwoord wijzigen')

@section('content')
    <h1>Wachtwoord wijzigen</h1>
    <form method="POST" action="{{ route('back.changepass') }}">
        {!! csrf_field() !!}
        <div class="form-group">
            <label for="description">Huidig wachtwoord: </label>
            <input type="password" class="form-control" name="currentpass" id="currentpass" title="Huidig wachtwoord" />
        </div>
        <div class="form-group">
            <label for="description">Nieuw wachtwoord: </label>
            <input type="password" class="form-control" name="newpass" id="newpass" title="Nieuw wachtwoord" placeholder="Minimaal zes tekens"/>
        </div>
        <div class="form-group">
            <label for="description">Herhaal wachtwoord: </label>
            <input type="password" class="form-control" name="repeatpass" id="repeatpass" title="Herhaal wachtwoord" />
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Wachtwoord wijzigen"/>
        </div>
    </form>
    <br/>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je het wachtwoord veranderen voor het inloggen aan de achterkant. Vul eerst het oude wachtwoord in en vervolgens tweemaal het nieuwe wachtwoord, klik vervolgens op 'Wachtwoord wijzigen'.");
@endsection