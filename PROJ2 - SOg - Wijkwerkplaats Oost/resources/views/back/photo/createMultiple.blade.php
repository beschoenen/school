@extends('back.layout')

@section('title', 'Upload foto\'s')

@section('image')
    <label for="image">Fotos</label>
    <input type="file" name="image[]" value="Selecteer een foto" class="form-control" accept="image/*" required multiple/>
@endsection

@section('content')
    <form method="POST" action="{{ $url }}" enctype="multipart/form-data" >
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="form-group">
            @yield('image')
        </div>
        <div class="form-group">
            <input type="submit" class="btn btn-primary" value="Opslaan"/>
        </div>
    </form>
@endsection
@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je meerdere foto's uploaden. De naam en de beschrijving kun je later aanpassen.");
@endsection