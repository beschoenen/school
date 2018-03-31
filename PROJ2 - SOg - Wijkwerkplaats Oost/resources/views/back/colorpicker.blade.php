@extends('back.layout')

@section('title', 'Verander kleur')

@section('content')
    <form method="POST" action="{{ $url }}">
        {!! csrf_field() !!}
        <div class="form-group">
            <label for="menu">Menu achtergrond kleur</label>
            <div id="cp" class="input-group colorpicker-component">
                <input id="menucolor" type="text" data-format="hex" name="menucolor" value="{{ $menucolor }}" class="form-control" />
                <span class="input-group-addon"><i></i></span>
            </div>
            <br>
            <label for="text">Menu tekst kleur</label>
            <div id="cp1" class="input-group colorpicker-component">
                <input id="tekstcolor" type="text" data-format="hex" name="textcolor" value="{{ $textcolor  }}" class="form-control" />
                <span class="input-group-addon"><i></i></span>
            </div>
            <br>
            <label for="saying">Spreuk tekst kleur</label>
            <div id="cp2" class="input-group colorpicker-component">
                <input id="sayingcolor" type="text" data-format="hex" name="sayingcolor" value="{{ $sayingcolor  }}" class="form-control" />
                <span class="input-group-addon"><i></i></span>
            </div>      
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kan je de kleuren veranderen. Klik op het vierkantje om een nieuwe kleur te selecteren.");
@endsection

@push('js')
    <script src="{{ asset('/assets/js/bootstrap-colorpicker.min.js') }}"></script>
    <script src="{{ asset('assets/js/backEnd/colorpicker.js') }}"></script>
@endpush

@push('css')
    <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/bootstrap-colorpicker.min.css') }}"/>
    <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/colorpicker.css') }}"/>
@endpush