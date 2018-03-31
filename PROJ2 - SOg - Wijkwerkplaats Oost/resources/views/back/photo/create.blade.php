@extends('back.layout')

@section('title', 'Upload foto')

@section('image')
    <label for="image">Foto</label>
    <input type="file" name="image" value="Selecteer een foto" class="form-control" accept="image/*" required/>
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
            <input id="name" type="text" name="name" class="form-control" value="{{ $photo->name }}" required/>
        </div>
        <div class="form-group">
            <label for="description">Beschrijving</label>
            <input id="description" type="text" name="description" placeholder="Optioneel" class="form-control"
                   value="{{ $photo->description }}"/>
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een foto uploaden. Selecteer een foto en vul een naam in. Als je wilt kun je ook een beschrijving van een foto toevoegen.");
@endsection