<?php

namespace App\Providers;

use App\Models\Database\Color;
use App\Models\Database\Logo;
use App\Models\Database\Page;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Input;
use Illuminate\Support\ServiceProvider;
use App\Models\Database\Text;

class ComposerServiceProvider extends ServiceProvider
{
    private static $currentPage;
    /**
     * Bootstrap the application services.
     *
     * @return void
     */
    public function boot()
    {
        view()->composer('front.*', function($view) {
            $split = explode('.', $view->getName());
            static::$currentPage = end($split);

            $font_class = "font-small";
            if(isset($_COOKIE['fontsize'])) {
                if ($_COOKIE['fontsize'] === 'large') {
                    $font_class = "font-large";
                } elseif ($_COOKIE['fontsize'] === 'medium') {
                    $font_class = "font-medium";
                }
            }

            $view->with('saying', Text::getText('home', 'saying'))
                ->with('editMode', Auth::check() && Input::get('edit') === "true")
                ->with('page', static::$currentPage)
                ->with('font_class', $font_class)
                ->with('menucolor', Color::find(1)->hexcode)
                ->with('textcolor', Color::find(2)->hexcode)
                ->with('sayingcolor', Color::find(3)->hexcode)
                ->with('sponsers', Logo::select('url', 'id')->get());
        });

        view()->composer('back.*', function($view) {
            $split = explode('.', $view->getName());
            static::$currentPage = end($split);

            $view->with('color', Color::find(1)->hexcode)
                ->with('menucolor', Color::find(1)->hexcode)
                ->with('textcolor', Color::find(2)->hexcode)
                ->with('sayingcolor', Color::find(3)->hexcode);
        });

        view()->composer('fragments.image_pick_modal', function($view) {
            $view->with('image_modal_id', "modal-" . str_shuffle('wijkwerkplaatsoostdenbosch'));
        });

        view()->composer('fragments.image_carousel', function($view) {
            $view->with('images', Page::where('name', static::$currentPage)->firstOrNew([])->photos);
        });
    }

    /**
     * Register the application services.
     *
     * @return void
     */
    public function register()
    {
        //
    }
}
