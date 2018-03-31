@extends('front.layout')

@section('title', 'Contactformulier')

@push('css')
<link rel="stylesheet" type="text/css" href="{{ asset('assets/css/contactform.css') }}"/>
@endpush

@section('content')
    @if($contact_form_enabled->value == "1")
    <div class="row form">
        <div class="col-md-6 col-md-offset-3">
            @if (isset($message))
                <div class="alert alert-info" role="alert">{{ $message }}</div>
            @endif
            @if (count($errors) > 0)
                <div class="alert alert-danger">
                    <ul>
                        @foreach ($errors->all() as $error)
                            <li>{{ $error }}</li>
                        @endforeach
                    </ul>
                </div>
            @endif
            <form action="" method="POST">
                <input type="hidden" name="_token" value="{{ csrf_token() }}">
                <div class="row">
                    <div class="col-md-4">
                        <label for="name">Uw naam</label>
                    </div>
                    <div class="col-md-8">
                        <input class="form-control" type="text" name="name" id="name" maxlength="75"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="email">Uw email</label>
                    </div>
                    <div class="col-md-8">
                        <input class="form-control" type="text" name="email" id="email" maxlength="75"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="subject">Onderwerp</label>
                    </div>
                    <div class="col-md-8">
                        <input class="form-control" type="text" name="subject" id="subject" maxlength="125"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <textarea class="form-control" name="message" placeholder="Uw bericht" rows="10"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <span class="pull-left">Alle velden zijn vereist!</span>
                        <div class="pull-right">
                            {!! Recaptcha::render() !!}
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <input class="btn btn-danger pull-right" type="submit" value="Verzenden"/>
                    </div>
                </div>
            </form>
        </div>
    </div>
    @else
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="alert alert-warning">
                    Het contactformulier is uitgeschakeld.
                </div>
            </div>
        </div>
    @endif
@endsection