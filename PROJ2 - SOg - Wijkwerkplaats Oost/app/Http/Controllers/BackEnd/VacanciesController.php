<?php namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Input;
use App\Models\Database\Vacancy;

class VacanciesController extends Controller
{
	public function index()
	{
		return view('back.vacancies.index', [
			'vacancies' => Vacancy::paginate(),
		]);
	}

	public function create()
	{
		return view('back.vacancies.create', [
			'url' => route('back.vacancies.store'),
			'type' => 'POST',
			'vacancy' => new Vacancy()
		]);
	}

	public function store(Request $request)
	{
		$this->validate($request, [
			'title' => 'required|string',
			'summary' => 'required|string',
			'text' => 'required|string'
		]);

		$vacancy = new Vacancy([
			'title' => Input::get('title'),
			'summary' => Input::get('summary'),
			'text' => Input::get('text')
		]);

		$vacancy->save();

		return redirect()->route('back.vacancies.index')->withMessage("Uw vacature is geplaatst");
	}

	public function edit(Vacancy $vacancies)
	{
		return view('back.vacancies.edit', [
			'url' => route('back.vacancies.update', $vacancies->id),
			'type' => 'PATCH',
			'vacancy' => $vacancies
		]);
	}

	public function update(Request $request, Vacancy $vacancies)
	{
		$this->validate($request, [
			'title' => 'required|string',
			'summary' => 'required|string',
			'text' => 'required|string'
		]);

		$vacancies->update(Input::only('title', 'summary', 'text'));

		return redirect()->route('back.vacancies.index')->withMessage('Uw vacature is bijgewerkt');
	}

	public function delete(Vacancy $vacancies)
	{
		return view('back.vacancies.delete', [
			'url' => route('back.vacancies.destroy', $vacancies->id),
			'type' => 'delete',
			'return' => route('back.vacancies.index'),
			'vacancy' => $vacancies
		]);
	}

	public function destroy(Vacancy $vacancies)
	{
		$vacancies->delete();

		return redirect()->route('back.vacancies.index')->withMessage("Uw vacature is verwijderd");
	}
}
