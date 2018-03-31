<nav class="navbar navbar-default color-navbar">
    <div class="container">
        <div class="navbar-header">
            <!-- Collapsed Hamburger -->
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse"
                    data-target="#app-navbar-collapse">
                <span class="sr-only">Toggle Navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="{{ route('front.home') }}">
                Wijkwerkplaats Oost
            </a>
        </div>
        <div class="collapse navbar-collapse" id="app-navbar-collapse">
            <!-- Left Side Of Navbar -->
            <ul class="nav navbar-nav">
                <li><a href="{{ route('back.homepage.show') }}"><i class="fa fa-home"></i> Start</a></li>
                <li><a href="{{ route('back.activities.index') }}"><i class="fa fa-calendar"></i> Activiteiten</a></li>
                <li><a href="{{ route('back.about.index') }}"><i class="fa fa-file"></i> Over ons</a></li>
                <li><a href="{{ route('back.saying.show') }}"><i class="fa fa-info"></i> Spreuk</a></li>
                <li><a href="{{ route('back.photo.index') }}"><i class="fa fa-photo"></i> Fotos</a></li>
                <li><a href="{{ route('back.logo.index') }}"><i class="fa fa-photo"></i> Sponsoren</a></li>
                <li><a href="{{ route('back.news.index') }}"><i class="fa fa-file-text"></i> Nieuws</a></li>
                <li><a href="{{ route('back.vacancies.index') }}"><i class="fa fa-user"></i> Vacatures</a></li>
                <li><a href="{{ route('back.colorpicker.show') }}"><i class="fa fa-adjust"></i> Kleur</a></li>
                <li><a href="{{ route('back.settings') }}"><i class="fa fa-wrench"></i> Instellingen</a></li>
            </ul>
            <!-- Right Side Of Navbar -->
            <ul class="nav navbar-nav navbar-right">
                <!-- Authentication Links -->
                @if (Auth::guest())
                    <li><a href="{{ route('back.getLogin') }}">Login</a></li>
                    {{--<li><a href="{{ route('back.getRegister') }}">Registreer</a></li>--}}
                @else
                    <li class="dropdown color-navbar">
                        <a href="#" class="dropdown-toggle color-navbar" data-toggle="dropdown" role="button"
                            aria-expanded="false">
                            <i class="fa fa-user"></i> {{ Auth::user()->name }} <span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu color-navbar" role="menu">
                            <li><a href="{{ route('back.changepass') }}"><i class="fa fa-btn fa-wrench"></i>Instellingen</a>
                            <li><a href="{{ route('back.logout') }}"><i class="fa fa-btn fa-sign-out"></i>Log uit</a>
                            </li>
                        </ul>
                    </li>
                @endif
            </ul>
        </div>
    </div>
</nav>