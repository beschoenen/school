@extends('back.layout')

@section('title', 'Vacatures')

@section('content')
    <a href="{{ route('back.vacancies.create') }}">Maak één nieuwe vacature</a>
    <hr>
    @foreach($vacancies as $vacancy)
        <div class="row">
            <div class="col-md-6">
                <span class="news-header">{{ $vacancy->title }}</span>
            </div>
            <div class="col-md-4 pull-right">
                <div class="pull-right">
                    <a href="{{ route('back.vacancies.edit', $vacancy->id) }}">Aanpassen</a> |
                    <a href="{{ route('back.vacancies.delete', $vacancy->id) }}">Verwijderen</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                {{ $vacancy->getsummary() }}
            </div>
        </div>
        <hr>
    @endforeach
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            {{ $vacancies->links() }}
        </div>
    </div>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je vacatures toevoegen verwijderen of aanpassen. Klik op 'Maak één nieuwe vacature' om een vacature toe te voegen.");
@endsection
@push('css')
    <link rel="stylesheet" href="{{ asset('/assets/css/news.css') }}" type="text/css"/>
@endpush