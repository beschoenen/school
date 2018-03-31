<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Route;

Auth::routes();

Route::get('/', ['uses' => 'MainController@welcome', 'as' => 'main.welcome']);
Route::get('/home', ['uses' => 'MainController@home', 'as' => 'main.home']);

Route::get('user/profile', ['uses' => 'UserController@profile', 'as' => 'user.profile']);

Route::resource('beacons', 'BeaconController', ['except' => ['store', 'create']]);
Route::resource('tours', 'TourController');
