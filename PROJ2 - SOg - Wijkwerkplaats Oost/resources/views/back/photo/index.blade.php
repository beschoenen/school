@extends('back.layout')

@section('title', 'Foto\'s')

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
                        <a href="{{ route('back.photo.create') }}">Upload één nieuwe foto</a>
                    </div>
                    <div class="col-md-6">
                        <a class="pull-right" href="{{ route('back.photo.createMultiple') }}">Upload meerdere nieuwe fotos</a>
                    </div>
                </div>
            </li>
            @each('back.photo.listItem', $photos, 'photo')
            <li class="list-group-item">
                <div class="row">
                    <div class="col-md-12">
                        <input type="submit" value="Verwijder" class="btn btn-warning"/>
                    </div>
                </div>
            </li>
        </ul>
        {{ $photos->links() }}
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je foto's toevoegen of verwijderen. Alleen de foto's die hier zijn toegevoegd kun je op de verschillende pagina's laten zien.");
@endsection

@push('css')
    <link rel="stylesheet" href="{{ asset('/assets/css/photos.css') }}" type="text/css"/>
@endpush