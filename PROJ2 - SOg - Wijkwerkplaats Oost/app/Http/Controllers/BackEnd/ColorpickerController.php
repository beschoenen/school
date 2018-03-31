<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Database\Color;

class ColorpickerController extends Controller
{
	public function show()
	{
		return view('back.colorpicker', [
			'url' => route('back.colorpicker.save'),
			'menucolor' => Color::find(1)->hexcode,
			'textcolor' => Color::find(2)->hexcode,
			'sayingcolor' => Color::find(3)->hexcode
		]);
	}

	public function save(Request $request)
	{
		$this->validate($request, [
			'menucolor' => 'required|regex:/^#[a-fA-F0-9]{6}$/i',
			'textcolor' => 'required|regex:/^#[a-fA-F0-9]{6}$/i',
			'sayingcolor' => 'required|regex:/^#[a-fA-F0-9]{6}$/i'
		]);

		Color::find(1)->update(['hexcode' => $request->input('menucolor')]);
		Color::find(2)->update(['hexcode' => $request->input('textcolor')]);
		Color::find(3)->update(['hexcode' => $request->input('sayingcolor')]);

		return redirect()->route('back.colorpicker.show')->withMessage("De kleur is aangepast");
	}
}