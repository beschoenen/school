<?php namespace App\Http\Controllers\FrontEnd;

use App\Http\Controllers\Controller;
use App\Models\Database\Text;
use App\Models\Database\Article;
use App\Models\Database\Vacancy;

class PageController extends Controller
{
	public function home()
	{
		return view('front.home', ['text' => Text::getText('home', 'main')]);
	}

	public function news(Article $article)
	{
		if($article->exists) {
			return view('front.article', ['article' => $article]);
		} else {
			return view('front.news', ['articles' => Article::paginate(4)]);
		}
	}
	
	public function vacancies(Vacancy $vacancy)
	{
		if($vacancy->exists) {
			return view('front.vacancy', ['vacancy' => $vacancy]);
		} else {
			return view('front.vacancies', ['vacancies' => Vacancy::paginate(4)]);
		}
	}
	
	public function contact()
	{
		return view('front.contact', [
			'left' => Text::getText('contact', 'left'),
			'right' => Text::getText('contact', 'right')
		]);
	}

	public function activities($topic = 'main')
	{
		return view('front.activities', [
			'section' => "activities.$topic",
			'text' => Text::getText('activities', $topic),
			'activities' => Text::getSections('activities')
		]);
	}

	public function help($topic = 'main')
	{
		return view('front.help', [
			'section' => "help.$topic",
			'topic' => Text::getText('help', $topic)
		]);
	}

	public function about($topic = 'main')
	{
		return view('front.about', [
			'section' => "about.$topic",
			'text' => Text::getText('about', $topic),
			'topics' => Text::getSections('about')
		]);
	}
}