@extends('front.layout')

@section('title', 'Nieuws')

@push('css')
<link rel="stylesheet" type="text/css" href="{{ asset('assets/css/news.css') }}"/>
@endpush

@section('content')
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <a href="{{ route('front.news') }}">Terug</a>
        </div>
        <div class="col-md-10 col-md-offset-1">
            <h2>{{ $article->title }}</h2>
            <h5 class="date">{{ $article->created_at }}</h5>
        </div>
        <div class="col-md-10 col-md-offset-1">
            <p>{!! $article->text !!}</p>
        </div>
        <div class="col-md-10 col-md-offset-1">
            <br><a href="{{ route('front.news') }}">Terug</a>
        </div>
    </div>
@endsection