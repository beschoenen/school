@extends('back.layout')

@section('title', 'Nieuw nieuwsbericht')

@section('content')
    <form method="POST" action="{{ $url }}" enctype="multipart/form-data" >
        {!! csrf_field() !!}
        {!! method_field($type) !!}
        <div class="form-group">
            <label for="title">Titel</label>
            <input id="title" type="text" name="title" class="form-control" value="{{ $article->title }}" required/>
        </div>
        <div class="form-group">
            <label for="text">Tekst</label>
            <textarea id="text" class="form-control tiny_text" rows="10">{{ $article->text }}</textarea>
            <input type="hidden" name="text" value="" id="hidden-text"/>
        </div>
        <div class="form-group">
            <input type="button" id="save" class="btn btn-primary" value="Opslaan"/>
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
    $("#tooltip").attr("data-original-title", "Hier kun je een nieuw nieuwsartikel maken. Vul een titel in en schrijf een artikel. Als het artikel af is klik op 'Opslaan'.");
@endsection