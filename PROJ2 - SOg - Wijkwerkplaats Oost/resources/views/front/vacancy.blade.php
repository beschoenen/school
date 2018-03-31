@extends('front.layout')

@section('title', $vacancy->title)

@section('content')
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <a href="{{ route('front.vacancies') }}">Terug</a>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <h5>Geplaatst op: {{ date('d/m/Y', strtotime($vacancy->created_at)) }}</h5>
        </div>
    </div>
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            {!! $vacancy->text !!}
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-md-offset-1 col-md-10">
            <a href="{{ route('front.vacancies') }}">Terug</a>
        </div>
    </div>
@endsection
