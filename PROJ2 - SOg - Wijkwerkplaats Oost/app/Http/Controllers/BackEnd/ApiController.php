<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use App\Models\Database\Logo;
use App\Models\Database\Page;
use App\Models\Database\Photo;
use App\Models\Database\Text;
use Illuminate\Http\Request;

class ApiController extends Controller
{
	public function text(Request $request)
	{
		$section = explode(".", $request->input('section'));

		Text::where('page', $section[0])->where('section', $section[1])->firstOrFail()
			->update(['text' => $request->input('text')]);
	}

	public function images(Request $request)
	{
		Page::where('name', $request->input('page'))->firstOrFail()
			->photos()->sync($request->input('images', []));
	}

	public function listImages()
	{
		return Photo::select('id', 'name')->paginate(12);
	}
}