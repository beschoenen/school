<?php

Route::group(['middleware' => 'web'], function() {
    // Misc routes
    Route::get('/image/{photo}', ['uses' => 'MiscController@image', 'as' => 'misc.image']);
    Route::get('/image/{photo}/thumb', ['uses' => 'MiscController@thumb', 'as' => 'misc.thumb']);
    Route::get('/logo/{logo}/', ['uses' => 'MiscController@logo', 'as' => 'misc.logo']);

    // Front-end pages
    Route::group(['namespace' => 'FrontEnd'], function() {
        Route::get('/', ['uses' => 'PageController@home', 'as' => 'front.home']);
        Route::get('/news/{article?}', ['uses' => 'PageController@news', 'as' => 'front.news']);
        Route::get('/activities/{topic?}', ['uses' => 'PageController@activities', 'as' => 'front.activities']);
        Route::get('/vacancies/{vacancy?}', ['uses' => 'PageController@vacancies', 'as' => 'front.vacancies']);
        Route::get('/help/{topic?}', ['uses' => 'PageController@help', 'as' => 'front.help']);
        Route::get('/about/{topic?}', ['uses' => 'PageController@about', 'as' => 'front.about']);
        Route::get('/contact/form', ['uses' => 'ContactController@contactForm', 'as' => 'front.contactform']);
        Route::post('/contact/form', ['uses' => 'ContactController@sendEmail', 'as' => 'front.contactform']);
        Route::get('/contact', ['uses' => 'ContactController@contact', 'as' => 'front.contact']);
    });

    // Back-end
    Route::group(['namespace' => 'BackEnd', 'prefix' => 'back'], function() {
        // Login, Register & Reset routes
        Route::group(['namespace' => 'Auth'], function() {
            Route::get('/login', ['uses' => 'AuthController@showLoginForm', 'as' => 'back.getLogin']);
            Route::post('/login', ['uses' => 'AuthController@login', 'as' => 'back.postLogin']);
            Route::get('/logout', ['uses' => 'AuthController@logout', 'as' => 'back.logout']);
            Route::get('/register', ['uses' => 'AuthController@showRegistrationForm', 'as' => 'back.getRegister']);
            Route::post('/register', ['uses' => 'AuthController@register', 'as' => 'back.postRegister']);
            Route::post('/password/email', ['uses' => 'PasswordController@sendResetLinkEmail', 'as' => 'back.sendReset']);
            Route::post('/password/reset', ['uses' => 'PasswordController@reset', 'as' => 'back.postReset']);
            Route::get('/password/reset/{token?}', ['uses' => 'PasswordController@showResetForm', 'as' => 'back.getReset']);
        });

        // Back-end pages
        Route::group(['middleware' => 'auth'], function() {
            Route::get('/', ['as' => 'back.homepage.show', function() {
                return view('back.homepage');
            }]);

            // Route for news
            Route::get('news/{article}/delete', ['uses' => 'NewsController@delete', 'as' => 'back.news.delete']);
            Route::resource('news', 'NewsController');

            // Route for vacancies
            Route::get('vacancies/{vacancies}/delete', ['uses' => 'VacanciesController@delete', 'as' => 'back.vacancies.delete']);
            Route::resource('vacancies', 'VacanciesController');
            
            // Route for photos
            Route::get('photo/multiple/create', ['uses' => 'PhotoController@createMultiple', 'as' => 'back.photo.createMultiple']);
            Route::post('photo/multiple/create', ['uses' => 'PhotoController@storeMultiple', 'as' => 'back.photo.storeMultiple']);
            Route::post('photo/multiple/delete', ['uses' => 'PhotoController@deleteMultiple', 'as' => 'back.photo.deleteMultiple']);
            Route::delete('photo/multiple/delete', ['uses' => 'PhotoController@destroyMultiple', 'as' => 'back.photo.destroyMultiple']);
            Route::get('photo/{photo}/delete', ['uses' => 'PhotoController@delete', 'as' => 'back.photo.delete']);
            Route::resource('photo', 'PhotoController');

            // Route for logos
            Route::post('logo/multiple/delete', ['uses' => 'LogoController@deleteMultiple', 'as' => 'back.logo.deleteMultiple']);
            Route::delete('logo/multiple/delete', ['uses' => 'LogoController@destroyMultiple', 'as' => 'back.logo.destroyMultiple']);
            Route::get('logo/{logo}/delete', ['uses' => 'LogoController@delete', 'as' => 'back.logo.delete']);
            Route::resource('logo', 'LogoController');

            //Routes for saying
            Route::get('saying', ['uses' => 'SayingPageController@show', 'as' => 'back.saying.show']);
            Route::post('saying', ['uses' => 'SayingPageController@save', 'as' => 'back.saying.save']);

            //Routes for colorpicker
            Route::get('colorpicker', ['uses' => 'ColorpickerController@show', 'as' => 'back.colorpicker.show']);
            Route::post('colorpicker', ['uses' => 'ColorpickerController@save', 'as' => 'back.colorpicker.save']);
            
            //Routes for activities
            Route::post('activities/update', ['uses' => 'ActivitiesPageController@update', 'as' => 'back.activities.update']);
            Route::get('activities/delete/{topic?}', ['uses' => 'ActivitiesPageController@delete', 'as' => 'back.activities.delete']);
            Route::resource('activities', 'ActivitiesPageController', ['except' => ['update']]);

            //Routes for about
            Route::post('about/update', ['uses' => 'AboutPageController@update', 'as' => 'back.about.update']);
            Route::get('about/delete/{topic?}', ['uses' => 'AboutPageController@delete', 'as' => 'back.about.delete']);
            Route::resource('about', 'AboutPageController', ['except' => ['update']]);

            //User settings
            Route::get('changepass', ['uses' => 'UserSettingsController@show', 'as' => 'back.changepass']);
            Route::post('changepass', ['uses' => 'UserSettingsController@savePass', 'as' => 'back.changepass']);

            //Global settings
            Route::get('settings', ['uses' => 'SettingsController@show', 'as' => 'back.settings']);
            Route::post('settings', ['uses' => 'SettingsController@saveSettings', 'as' => 'back.settings']);

            // Backend API
            Route::group(['prefix' => 'api'], function() {
                Route::post('text', ['uses' => 'ApiController@text', 'as' => 'back.api.text']);
                Route::post('images', ['uses' => 'ApiController@images', 'as' => 'back.api.images']);
                Route::get('images', ['uses' => 'ApiController@listImages', 'as' => 'back.api.listImages']);
            });
        });
    });
});
