@extends('front.layout')

@section('content')
    <div class="row">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            @yield('left')
        </div>
        <div class="col-lg-1 col-md-1 hidden-sm hidden-xs">
            <div class="column-separator"></div>
        </div>
        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
            @yield('right')
        </div>
    </div>
@endsection

@push('js')
<script>
    $(document).ready(function(){
        $(".column-separator").height( $("#mainbody").height() );
    });
</script>
@endpush

@push('css')
<style>
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
</style>
@endpush