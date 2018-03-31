@extends('back.layout')

@section('title', 'Nieuwe vacature')

@section('content')
    <form method="POST" action="{{ $url }}" enctype="multipart/form-data">
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="form-group">
            <label for="title">Titel</label>
            <input id="title" type="text" name="title" class="form-control" value="{{ $vacancy->title }}" required/>
        </div>
        <div class="form-group">
            <label for="summary">Koptekst&nbsp;<i>(100 karakters)</i></label>
            <input type="text" size="100" maxlength="100" id="summary" name="summary" class="form-control" value="{{ $vacancy->summary }}"/>
        </div>
        <div class="form-group">
            <label for="text">Tekst</label>
            <textarea id="text" class="form-control tiny_text" rows="10">{{ $vacancy->text }}</textarea>
            <input type="hidden" name="text" value="" id="hidden-text"/>
        </div>
        <div class="form-group">
            <input type="button" class="btn btn-primary" id="save" value="Opslaan"/>
        </div>
    </form>
@endsection

@push('js')
<script>
    $("#save").on('click', function() {
        var text = tinymce.activeEditor.getContent({ format: 'html' });
        $("#hidden-text").val(text);
        $(this).closest('form').submit();
    });
</script>
@endpush

@section('tooltip')
    $("#tooltip").attr("data-original-title", "Hier kun je een nieuwe vacature maken. Vul een titel in en schrijf de vacature, bij 'Koptekst' kan een kleine samenvatting worden ingevuld. Als de vacature af is klik op 'Opslaan'.");
@endsection