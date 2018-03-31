<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Database\Text;

class ContactPageController extends Controller
{
	public function show()
	{
		return view('back.contact', [
			'url' => route('back.contact.save'),
			'left' => Text::getText('contact', 'left'),
			'right' => Text::getText('contact', 'right')
		]);
	}

	public function save(Request $request)
	{
		$this->validate($request, [
			'left' => 'required|string',
			'right' => 'required|string'
		]);

		Text::where('page', 'contact')->where('section', 'left')->update(['text' => $request->left]);
		Text::where('page', 'contact')->where('section', 'right')->update(['text' => $request->right]);

		return redirect()->route('back.contact.show')->withMessage("De pagina is aangepast");
	}
}