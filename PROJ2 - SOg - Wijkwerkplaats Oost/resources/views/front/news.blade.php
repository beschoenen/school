@extends('front.layout')

@section('title', 'Nieuws')

@push('css')
<link rel="stylesheet" type="text/css" href="{{ asset('assets/css/news.css') }}"/>
@endpush

@section('content')
    @foreach($articles as $article)
        <div class="row">
            <a href="{{ route('front.news', $article->id) }}" class="news-link-text-color">
                <div class="col-md-8 col-md-offset-2 article">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h3>{{ $article->title }}</h3>
                            <p class="date">{{ $article->created_at }}</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-8 col-md-offset-2 article">
                    <p>{!! $article->getText() !!}</p>
                </div>
            </a>
        </div>
        <hr>
    @endforeach
    <div class="col-md-4 col-md-offset-4">
        {{ $articles->links() }}
    </div>
@endsection

@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hieronder staan de nieuwsberichten. Klik op 'Ga terug' en vervolgens boven in de pagina op 'Nieuws' om de nieuwsberichten aan te passen.");
@endsection