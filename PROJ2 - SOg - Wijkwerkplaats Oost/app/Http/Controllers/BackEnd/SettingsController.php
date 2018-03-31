<?php

namespace App\Http\Controllers\BackEnd;

use Auth;
use Redirect;
use Hash;
use App\Http\Controllers\Controller;
use App\Models\Database\Setting;
use Illuminate\Http\Request;

class SettingsController extends Controller
{
    public function show()
    {
        $contact_form_enabled = Setting::where('setting', 'contact_form_enabled')->first();
        $contact_form_email = Setting::where('setting', 'contact_form_email')->first();
        $backend_color_scheme = Setting::where('setting', 'backend_color_scheme')->first();
        return view('back.settings', [
            'contact_form_enabled' => $contact_form_enabled,
            'contact_form_email' => $contact_form_email,
            'backend_color_scheme' => $backend_color_scheme
        ]);
    }
    public function saveSettings(Request $request)
    {
        $this->validate($request, [
            'email' => 'email|required_with:pref_form',
        ]);
        Setting::where('setting', 'contact_form_enabled')->update(array('value' => isset($request->pref_form)));
        Setting::where('setting', 'contact_form_email')->update(array('value' => $request->email));
        Setting::where('setting', 'backend_color_scheme')->update(array('value' => isset($request->backend_color)));

        return Redirect()->route('back.settings')->withMessage('Uw contactvoorkeur is aangepast!');
    }
}