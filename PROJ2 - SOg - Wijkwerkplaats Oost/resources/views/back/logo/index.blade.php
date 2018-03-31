@extends('back.layout')

@section('title', 'Logo\'s')

@section('content')
    <form action="{{ $deleteUrl }}" method="POST">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <ul class="list-group">
            <li class="list-group-item">
                <div class="row">
                    <div class="col-md-1">
                        <input type="checkbox" title="select all" onclick="$('input[type=checkbox]').prop('checked', $(this).prop('checked'));"/>
                    </div>
                    <div class="col-md-5">
                        <a href="{{ route('back.logo.create') }}">Upload één nieuw logo</a>
                    </div>
                </div>
            </li>
            @each('back.logo.listItem', $logos, 'logo')
            <li class="list-group-item">
                <div class="row">
                    <div class="col-md-12">
                        <input type="submit" value="Verwijder" class="btn btn-warning"/>
                    </div>
                </div>
            </li>
        </ul>
        {{ $logos->links() }}
    </form>
@endsection

@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je logo's toevoegen of verwijderen. De logo's die hier zijn toegevoegd worden aan de onderkant van elke pagina weergegeven.");
@endsection

@push('css')
    <link rel="stylesheet" href="{{ asset('/assets/css/photos.css') }}" type="text/css"/>
@endpush