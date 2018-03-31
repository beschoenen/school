<?php
use Illuminate\Foundation\Testing\DatabaseMigrations;
use App\Models\Database\User;
use Folklore\Image\Exception\Exception;

class VacanciesTest extends TestCase
{
	public function testVacancies()
	{
		$user = factory(User::class)->create();

		//Create news
		$this->actingAs($user)
			->visit('back/vacancies/create')
			->type('This is a test', 'title')
			->type('This is the shorttext test', 'summary')
			->type('This is the longtext test', 'text')
			->press('Opslaan')
			->seePageIs('back/vacancies');

		$this->seeInDatabase('vacancies', ['title' => 'This is a test']);

		//Update news
		$this->actingAs($user)
			->visit('back/vacancies/2/edit')
			->type('This is an automatic test', 'title')
			->type('This is the shorttext test', 'summary')
			->type('This is the longtext test', 'text')
			->press('Opslaan')
			->seePageIs('back/vacancies');

		$this->seeInDatabase('vacancies', ['title' => 'This is an automatic test']);

		//Front vacancy list
		$this->visit('vacancies')->see('Wijk werkplaats Oost - Vacatures');

		//Front vacancy view
		$this->visit('vacancies/1')->see('Wijk werkplaats Oost - Administratief Medewerker Projectleiding');
	}
}
