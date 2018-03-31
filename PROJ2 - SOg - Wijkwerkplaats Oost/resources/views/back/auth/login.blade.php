@extends('back.layout')

@section('title', 'Login')

@section('content')

<form class="form-horizontal" role="form" method="POST" action="{{ route('back.postLogin') }}">
    {!! csrf_field() !!}

    <div class="form-group{{ $errors->has('email') ? ' has-error' : '' }}">
        <label class="col-md-4 control-label">E-Mail Adres</label>

        <div class="col-md-6">
            <input type="email" class="form-control" name="email" value="{{ old('email') }}">

            @if ($errors->has('email'))
                <span class="help-block">
                    <strong>{{ $errors->first('email') }}</strong>
                </span>
            @endif
        </div>
    </div>

    <div class="form-group{{ $errors->has('password') ? ' has-error' : '' }}">
        <label class="col-md-4 control-label">Wachtwoord</label>

        <div class="col-md-6">
            <input type="password" class="form-control" name="password">

            @if ($errors->has('password'))
                <span class="help-block">
                    <strong>{{ $errors->first('password') }}</strong>
                </span>
            @endif
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-6 col-md-offset-4">
            <div class="checkbox">
                <label>
                    <input type="checkbox" name="remember"> Onthoud mij
                </label>
            </div>
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-6 col-md-offset-4">
            <button type="submit" class="btn btn-primary">
                <i class="fa fa-btn fa-sign-in"></i>Login
            </button>

            <a class="btn btn-link" href="{{ route('back.getReset') }}">Wachtwoord vergeten?</a>
        </div>
    </div>
</form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Vul hieronder de juiste e-mail en het juiste wachtwoord in om pagina's aan te kunnen passen.");
@endsection
