@extends('front.layout')

@section('content')
    <div class="row">
        <div class="col-lg-2 col-md-2 col-sm-3 hidden-xs">
            <div class="over-ons-menu">
                <div class="over-ons-menu-header menu-header">
                    <span>Menu</span>
                </div>
                <ul class="nav nav-pills">
                    @yield('menu-items')
                </ul>
            </div>
        </div>
        <div class="visible-xs-inline-block col-md-offset-1">
            <ul class="mobile-menu">
                @yield('menu-items')
            </ul>
            <div style="clear: both;"></div>
            <hr class="fading-line"/>
        </div>
        <div class="col-lg-5 col-md-5 col-sm-6 col-xs-12">
            @yield('topic')
        </div>
        <div class="col-lg-5 col-md-5 hidden-sm hidden-xs">
            @yield('image')
        </div>
    </div>
@endsection