@extends('back.layout')

@section('title', 'Upload logo')

@section('image')
    <label for="image">Logo</label>
    <input type="file" name="image" value="Selecteer een logo" class="form-control" accept="image/*" required/>
@endsection

@section('content')
    <form method="POST" action="{{ $url }}" enctype="multipart/form-data">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="form-group">
            @yield('image')
        </div>
        <div class="form-group">
            <label for="name">Naam</label>
            <input id="name" type="text" name="name" class="form-control" value="{{ $logo->name }}" required/>
        </div>
        <div class="form-group">
            <label for="url">Url</label>
            <input id="url" type="text" name="url" class="form-control" value="{{ $logo->url }}"/>
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een logo uploaden. Selecteer een logo en vul een naam in. Als je wilt kun je ook de link naar hun website invullen onder het kopje url.");
@endsection