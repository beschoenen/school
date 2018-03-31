@extends('back.layout')

@section('title', 'Start pagina')

@section('content')
    <h3>Pagina's aanpassen</h3>
    <hr>
    <ul class="list-group">
        <li class="list-group-item clearfix"><a href="{{ route('front.home') }}/?edit=true">Homepagina aanpassen </a><a class="pull-right" data-toggle="tooltip"  data-placement="right" title="Klik hier om de tekst of foto's op de Thuispagina te veranderen." >?</a></li>
        <li class="list-group-item clearfix"><a href="{{ route('front.activities') }}/?edit=true">Activiteiten pagina's aanpassen</a><a class="pull-right" data-toggle="tooltip"  data-placement="right" title="Klik hier om de tekst of foto's op de verschillende Activiteitenpagina's te veranderen.">?</a></li>
        <li class="list-group-item clearfix"><a href="{{ route('front.help') }}/?edit=true">Hulp pagina's aanpassen</a><a class="pull-right" data-toggle="tooltip"  data-placement="right" title="Klik hier om de tekst of foto's op de verschillende Hulp-pagina's te veranderen.">?</a></li>
        <li class="list-group-item clearfix"><a href="{{ route('front.about') }}/?edit=true">Over ons pagina's aanpassen</a><a class="pull-right" data-toggle="tooltip"  data-placement="right" title="Klik hier om de tekst of foto's op de verschillende Over-ons-pagina's te veranderen.">?</a></li>
        <li class="list-group-item clearfix"><a href="{{ route('front.contact') }}/?edit=true">Contact pagina aanpassen</a><a class="pull-right" data-toggle="tooltip"  data-placement="right" title="Klik hier om de tekst op de Contactpagina te veranderen.">?</a></li>
    </ul>
@endsection
@section('tooltip')
        $("#tooltip").attr("data-original-title", "Klik op een pagina hieronder die je wilt aanpassen.");
@endsection