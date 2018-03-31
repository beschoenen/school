<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Auth;

class MainController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth')->except('welcome');
    }

    public function welcome()
    {
        if (Auth::check()) {
            return redirect(route('main.home'));
        }

        return view('welcome');
    }

    public function home()
    {
        return redirect(route('beacons.index'));
    }
}
