<!DOCTYPE html>
<html lang="nl">
    <head>
        <title>Wijk Werkplaats Oost - @yield('title', 'Welkom')</title>

        <meta charset="UTF-8">
        <meta name="description" content="Website van Wijk werkplaats Oost">
        <meta name="keywords" content="den bosch,werkwijkplaats,nederland">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        @if ($editMode)
            <meta name="csrf-token" content="{{ csrf_token() }}">

            <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/editor.css') }}"/>
        @endif
        <link rel="icon" href="{{ asset('assets/img/logo.png') }}" type="image/png" sizes="150x133">

        <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/bootstrap.min.css') }}"/>
        <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/style.css') }}"/>

        <style>
            .color-navbar {
                background-color: {{ $menucolor }} !important;
            }

            .color-navbar .navbar-nav a {
                color: {{ $textcolor }} !important;
            }

            .color-navbar .navbar-nav a:hover {
                background-color: {{ \App\Models\Database\Color::getDarkerColor($menucolor) }} !important;
            }

            .color-navbar .navbar-nav .active a {
                background-color: {{ \App\Models\Database\Color::getActiveColor($menucolor) }} !important;
            }
            .color-saying {
                color: {{ $sayingcolor }} !important;
            }

            .menu-header {
                background-color: {{ $menucolor }} !important;
            }

            .column-separator {
                border: 0;
                width: 3px;
                min-height: 250px;
                background-image: -webkit-linear-gradient(top, #FFFFFF, {{ $menucolor }}, #FFFFFF);
                background-image: -moz-linear-gradient(top, #FFFFFF, {{ $menucolor }}, #FFFFFF);
                background-image: -ms-linear-gradient(top, #FFFFFF, {{ $menucolor }}, #FFFFFF);
                background-image: -o-linear-gradient(top, #FFFFFF, {{ $menucolor }}, #FFFFFF);
                background-image: linear-gradient(top, #FFFFFF, {{ $menucolor }}, #FFFFFF);
                margin: 0 auto;
            }
            .navbar-header button{
                color: {{ $textcolor }} !important;
            }
            .navbar-brand{
                color: {{ $textcolor }} !important;
            }
            .font-selector.xs a{
                color: {{ $textcolor }} !important;
            }
            .mobile-menu ul{
                position:relative; left:-50%;
            }
            .mobile-menu li{
                border: 1px solid {{ \App\Models\Database\Color::getDarkerColor($menucolor) }};
                border-radius: 15px;
                margin: 2px;
                background-color: {{ $menucolor }};
            }
            .mobile-menu li:hover{
                border: 1px solid {{ $menucolor }};
                background-color: {{ \App\Models\Database\Color::getDarkerColor($menucolor) }};
            }
            .mobile-menu a{
                color: {{ $textcolor }} !important;
            }
            .mobile-menu a:hover{
                text-decoration: none;
            }
        </style>

        @stack('css')
    </head>
    <body class="{{ $font_class }}">
        <div class="container-fluid container">
            <div class="row hidden-xs">
                <div class="col-md-8 col-md-offset-2">
                    <a href="{{ route('front.home') }}"><img class="img-responsive" src="{{ asset('assets/img/logo3.png') }}"/></a>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-sm-12 col-sx-12 no-padding">
                    <nav class="navbar navbar-inverse color-navbar" id="navbar">
                        <div class="container-fluid">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#menu-bar" aria-expanded="false">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                                <div class="hidden-lg hidden-md hidden-sm">
                                    <a class="navbar-brand" href="#">Wijk Werkplaats Oost</a>
                                <span class="font-selector xs">
                                    <a href="javascript:void(0)" class="font-small">A</a>
                                    <a href="javascript:void(0)" class="font-medium">A</a>
                                    <a href="javascript:void(0)" class="font-large">A</a>
                                </span>
                                </div>
                            </div>
                        </div>
                        <div class="collapse navbar-collapse" id="menu-bar">
                            <span class="font-selector hidden-xs" style="height:0;">
                                <a href="javascript:void(0)" class="font-small">A</a>
                                <a href="javascript:void(0)" class="font-medium">A</a>
                                <a href="javascript:void(0)" class="font-large">A</a>
                            </span>
                            <ul class="nav navbar-nav">
                                <li class="{{ isActiveRoute('front.home') }}">
                                    <a href="{{ route('front.home') }}">Thuis</a>
                                </li>
                                <li class="{{ isActiveRoute('front.news') }}">
                                    <a href="{{ route('front.news')}}">Nieuws</a>
                                </li>
                                <li class="{{ isActiveRoute('front.activities') }}">
                                    <a href="{{ route('front.activities') }}">Activiteiten</a>
                                </li>
                                <li class="{{ isActiveRoute('front.help') }}">
                                    <a href="{{ route('front.help') }}">Hulp</a>
                                </li>
                                <li class="{{ isActiveRoute('front.about') }}">
                                    <a href="{{ route('front.about') }}">Over ons</a>
                                </li>
                                <li class="{{ isActiveRoute('front.contact') }}">
                                    <a href="{{ route('front.contact') }}">Contact</a>
                                </li>
                            </ul>
                        </div>
                    </nav>
                </div>
            </div>
            @if ($editMode)
                <div class="row editmode">
                    <div class="col-md-8 col-md-offset-2">
                        <label class="editmode-user-label">

                            Ingelogd als: {{  Auth::user()->name  }}, PAS OP U ZIT IN BEWERK MODUS!&nbsp;&nbsp;
                            <a href="{{ route('back.homepage.show') }}" class="not-edit">Ga terug</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <a class="pull-right" data-toggle="tooltip"  data-placement="bottom" title="schaap" id="tooltip" >?</a>
                        </label>
                    </div>
                </div>
            @endif
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-md-12">
                            <h1 id="pagetitle">
                                @yield('title')
                            </h1>
                        </div>
                    </div>
                    <div class="row" id="mainbody">
                        <div class="col-md-12">
                            @yield('content')

                            @if($editMode)
                                @include('fragments.image_pick_modal')
                            @endif
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="saying col-lg-12 col-md-12 col-sm-12 col-xs-12 color-saying">
                    <p>{!! $saying !!}</p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-offset-2 col-lg-8 col-md-offset-2 col-md-8 col-sm-offset-0 col-sm-12 col-xs-offset-3 col-xs-offset-0 col-xs-12">
                    <div class="row">
                        <div class="footer-sponsors">
                            @foreach($sponsers as $sponser)
                                <div class="col-lg-3 col-md-4 col-sm-4 col-xs-6">
                                    @if(!empty($sponser->url))
                                        <a href="//{{ $sponser->url }}">
                                            @endif
                                            <img class="img-responsive" src="{{ route('misc.logo', $sponser->id) }}"/>
                                            @if(!empty($sponser->url))
                                        </a>
                                    @endif
                                </div>
                            @endforeach
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="{{ asset('/assets/js/jquery-1.12.1.min.js') }}"></script>
        <script src="{{ asset('/assets/js/bootstrap.min.js') }}"></script>
        <script src="{{ asset('/assets/js/fontchanging.js') }}"></script>
        @if($editMode)
            <script src="{{ asset('/assets/js/editor.js') }}"></script>
            <script src="{{ asset('/assets/js/tinymce/tinymce.min.js') }}"></script>
            <script src="{{ asset('/assets/js/tinymce/langs/nl.js') }}"></script>
            <script>
                $(document).ready(function(){
                    $('[data-toggle="tooltip"]').tooltip();
                    @yield('tooltip')
                });
            </script>
        @endif
        @stack('js')
    </body>
</html>