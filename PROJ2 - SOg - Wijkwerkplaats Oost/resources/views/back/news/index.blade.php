@extends('back.layout')

@section('title', 'Nieuws')

@section('content')
    <a href="{{ route('back.news.create') }}">Maak één nieuw bericht</a>
    <hr>
    @foreach($articles as $article)
        <div class="row">
            <div class="col-md-6">
                <span class="news-header">{{ $article->title }}</span>
            </div>
            <div class="col-md-4 pull-right">
                <div class="pull-right">
                    <a href="{{ route('back.news.edit', $article->id) }}">Aanpassen</a> |
                    <a href="{{ route('back.news.delete', $article->id) }}">Verwijderen</a>
                </div>
            </div>
        </div>
        <hr>
    @endforeach
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            {{ $articles->links() }}
        </div>
    </div>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je nieuwsartikelen toevoegen verwijderen of aanpassen. Klik op 'Maak één nieuw bericht' om een nieuwsartikel toe te voegen.");
@endsection
@push('css')
    <link rel="stylesheet" href="{{ asset('/assets/css/news.css') }}" type="text/css"/>
@endpush