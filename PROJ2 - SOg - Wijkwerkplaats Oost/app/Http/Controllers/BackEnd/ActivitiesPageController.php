<?php

namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Database\Text;
use DB;

class ActivitiesPageController extends Controller
{
	public function index()
	{
		return view('back.activities.index', [
			'activities' => Text::getSections('activities'),
		]);
	}

	public function edit($section)
	{
		return view('back.activities.edit', [
			'url' => route('back.activities.update'),
			'section' => Text::getSection('activities', $section)->section,
		]);
	}

	public function update(Request $request)
	{
		$this->validate($request, [
			'old-section' => 'required|string|exists:texts,section',
			'section' => 'required|string',
		]);

		Text::where('section', $request->input('old-section'))
			->where('page', 'activities')
			->update($request->only(['section']));

		return redirect()->route('back.activities.index')->withMessage("De pagina is aangepast");
	}

	public function create()
	{
		return view('back.activities.create', [
			'url' => route('back.activities.store'),
			'section' => '',
		]);
	}

	public function store(Request $request)
	{
		$this->validate($request, [
			'section' => 'required|string|unique:texts,section,page,!activities',
		]);

		$section = new Text([
			'page' => 'activities',
			'section' => $request->input('section'),
			'text' => 'Nieuwe tekst'
		]);
		$section->save();

		return redirect()->route('back.activities.index')->withMessage("De pagina is aangemaakt");
	}

	public function delete($section)
	{
		return view('back.activities.delete', [
			'url' => route('back.activities.destroy', $section),
			'type' => 'delete',
			'activity' => Text::getSection('activities', $section)
		]);
	}

	public function destroy(Request $request)
	{
		Text::where('page', 'activities')->where('section', $request->input('section'))->delete();

		return redirect()->route('back.activities.index')->withMessage("De activiteit is verwijderd");
	}
}