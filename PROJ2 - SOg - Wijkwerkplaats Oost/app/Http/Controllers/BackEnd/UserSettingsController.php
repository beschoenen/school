<?php

namespace App\Http\Controllers\BackEnd;

use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class UserSettingsController extends Controller
{
	public function show()
	{
		return view('back.usersettings', ['currentuser' => Auth::user()]);
	}

	public function savePass(Request $request)
	{
		$this->validate($request, [
			'currentpass' => 'string|required',
			'newpass' => 'string|required',
			'repeatpass' => 'string|same:newpass|required'
		]);

		if (!Hash::check($request->currentpass, Auth::user()->password)) {
			return redirect()->back()->withErrors('Het huidige wachtwoord komt niet overeen!');
		}

		Auth::user()->update(['password' => Hash::make($request->newpass)]);

		return redirect()->route('back.changepass')->withMessage('Uw wachtwoord is aangepast!');
	}
}