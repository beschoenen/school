<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Database\Text;

class SayingPageController extends Controller
{
	public function show()
	{
		return view('back.saying', [
			'url' => route('back.saying.save'),
			'text' => Text::getText('home', 'saying')
		]);
	}

	public function save(Request $request)
	{
		$this->validate($request, [
			'text' => 'required|string'
		]);

		Text::where('page', 'home')->where('section', 'saying')->update(['text' => $request->text]);

		return redirect()->route('back.saying.show')->withMessage("De spreuk is aangepast");
	}
}