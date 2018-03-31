<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <title>Wijkwerkplaats Oost - @yield('title', 'Admin')</title>

        <link href="{{ asset('/assets/css/font-awesome.min.css') }}" rel='stylesheet' type='text/css'>
        <link href="https://fonts.googleapis.com/css?family=Lato:100,300,400,700" rel='stylesheet' type='text/css'>
        <link rel="icon" href="{{ asset('assets/img/logo.png') }}" type="image/png" sizes="150x133">

        <link rel="stylesheet" type="text/css" href="{{ asset('assets/css/bootstrap.min.css') }}"/>
        <style>
            body {
                font-family: 'Lato', serif;
            }

            .fa-btn {
                margin-right: 6px;
            }
            
            a[data-toggle="tooltip"] {
                cursor: help;
            }

            @if(\App\Models\Database\Setting::getSetting('backend_color_scheme') == "1")

                #tooltip {
                    color: {{ $textcolor }} !important;
                }

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

                .navbar-header button {
                    color: {{ $textcolor }} !important;
                }

                .navbar-brand {
                    color: {{ $textcolor }} !important;
                }

                .panel {
                    border-color: {{ \App\Models\Database\Color::getDarkerColor($menucolor) }} !important;
                }

                .panel-heading {
                    background-color: {{ $menucolor }} !important;
                    color: {{ $textcolor }} !important;
                    border-color: {{ \App\Models\Database\Color::getDarkerColor($menucolor) }} !important;
                }
            @endif
        </style>
        @stack('css')
    </head>
    <body id="app-layout">
        @if (Auth::check())
            @include('back.menu')
        @else
            <br/><br/><br/><br/>
        @endif
        <div class="container">
            <div class="row">
                <div class="col-md-8 col-md-offset-2">
                    @if (session('message'))
                        <div class="alert alert-info" role="alert">{{ session('message') }}</div>
                    @endif
                    @if (count($errors) > 0)
                        <div class="alert alert-danger">
                            <ul>
                                @foreach ($errors->all() as $error)
                                    <li>{{ $error }}</li>
                                @endforeach
                            </ul>
                        </div>
                    @endif
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            @yield('title', 'Admin')
                            <a class="pull-right" data-toggle="tooltip" data-placement="bottom" id="tooltip">?</a>
                        </div>
                        <div class="panel-body">
                            @yield('content')
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="{{ asset('/assets/js/jquery-1.12.1.min.js') }}"></script>
        <script src="{{ asset('/assets/js/bootstrap.min.js') }}"></script>
        <script src="{{ asset('/assets/js/tinymce/tinymce.min.js') }}"></script>
        <script src="{{ asset('/assets/js/tinymce/langs/nl.js') }}"></script>
        <script src="{{ asset('/assets/js/editorbackend.js') }}"></script>
        <script>
            $(document).ready(function() {
                $('[data-toggle="tooltip"]').tooltip();
                @yield('tooltip')
            });
        </script>
        @stack('js')
    </body>
</html>
