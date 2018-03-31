@extends('front.layout')

@section('title', 'Vacatures')

@section('content')
    @foreach($vacancies as $vacancy)
        <div class="row">
            <div class="col-md-offset-1 col-md-10">
                <h4>
                    <a href="{{ route('front.vacancies', $vacancy->id) }}" class="news-link-text-color">{{ $vacancy->title }}</a>
                </h4>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-10">
                <div class="col-md-2">
                    {{ date('d/m/Y', strtotime($vacancy->created_at)) }}
                </div>
                <div class="col-md-10">
                    {!! $vacancy->getsummary() !!}
                </div>
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