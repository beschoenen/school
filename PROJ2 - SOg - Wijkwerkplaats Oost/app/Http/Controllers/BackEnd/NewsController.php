<?php namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Input;
use App\Models\Database\Article;
use Response;

class NewsController extends Controller
{
	// TODO update validation

	public function index()
	{
		return view('back.news.index', [
			'articles' => Article::paginate(5),
		]);
	}

	public function create()
	{
		return view('back.news.create', [
			'url' => route('back.news.store'),
			'type' => 'POST',
			'article' => new Article()
		]);
	}

	public function store(Request $request)
	{
		$this->validate($request, [
			'title' => 'required|string',
			'text' => 'required|string'
		]);

		$article = new Article([
			'title' => Input::get('title'),
			'text' => Input::get('text')
		]);

		$article->save();

		return redirect()->route('back.news.index')->withMessage("Uw nieuwsbericht is geplaatst");
	}

	public function edit(Article $news)
	{
		return view('back.news.edit', [
			'url' => route('back.news.update', $news->id),
			'type' => 'PATCH',
			'article' => $news
		]);
	}

	public function update(Article $news)
	{
		$news->update(Input::only('title', 'text'));

		return redirect()->route('back.news.index')->withMessage('Uw nieuwsbericht is bijgewerkt');
	}

	public function delete(Article $article)
	{
		return view('back.news.delete', [
			'url' => route('back.news.destroy', $article->id),
			'type' => 'delete',
			'return' => route('back.news.index'),
			'article' => $article
		]);
	}

	public function destroy(Article $news)
	{
		$news->delete();

		return redirect()->route('back.news.index')->withMessage("Uw nieuwsbericht is verwijderd");
	}
}
