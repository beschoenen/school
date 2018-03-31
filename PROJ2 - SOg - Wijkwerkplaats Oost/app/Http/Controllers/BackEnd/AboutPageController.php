<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Database\Text;

class AboutPageController extends Controller
{
	public function index()
	{
		return view('back.about.index', [
			'sections' => Text::getSections('about'),
		]);
	}

	public function edit($section)
	{
		return view('back.about.edit', [
			'url' => route('back.about.update'),
			'section' => Text::getSection('about', $section)->section,
		]);
	}

	public function update(Request $request)
	{
		$this->validate($request, [
			'old-section' => 'required|string',
			'section' => 'required|string',
		]);

		Text::where('section', $request->input('old-section'))
			->where('page', 'about')
			->update($request->only(['section']));

		return redirect()->route('back.about.index')->withMessage("De pagina is aangepast");
	}

	public function create()
	{
		return view('back.about.create', [
			'url' => route('back.about.store'),
			'section' => '',
		]);
	}

	public function store(Request $request)
	{
		$this->validate($request, [
			'section' => 'required|string|unique:texts,section,page,!about',
		]);

		$section = new Text([
			'page' => 'about',
			'section' => $request->input('section'),
			'text' => 'Nieuwe tekst'
		]);
		$section->save();

		return redirect()->route('back.about.index')->withMessage("De pagina is aangemaakt");
	}

	public function delete($section)
	{
		return view('back.about.delete', [
			'url' => route('back.about.destroy', $section),
			'type' => 'delete',
			'section' => Text::getSection('about', $section)
		]);
	}

	public function destroy(Request $request)
	{
		Text::where('page', 'about')->where('section', $request->input('section'))->delete();

		return redirect()->route('back.about.index')->withMessage("De pagina is verwijderd");
	}
}