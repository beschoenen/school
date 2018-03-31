<?php

use App\Models\Database\Photo;
use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class PhotosTest extends TestCase
{
	public function testCreate()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/photo/create')
			->type('Little Kitty', 'name')
			->type('This is a little kitty', 'description')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/photo');

		$this->seeInDatabase('photos', ['name' => 'Little Kitty']);

		Photo::where('name', 'Little Kitty')->first()->delete();
	}

	public function testUpdate()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/photo/create')
			->type('Little Kitty', 'name')
			->type('This is a little kitty', 'description')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/photo');

		$this->seeInDatabase('photos', ['name' => 'Little Kitty']);

		$this->actingAs($user)
			->visit('/back/photo/1/edit')
			->type('Big Kitty', 'name')
			->type('This is a big kitty', 'description')
			->press('Opslaan')
			->seePageIs('/back/photo');

		$this->seeInDatabase('photos', ['name' => 'Big Kitty']);

		Photo::where('name', 'Big Kitty')->first()->delete();
	}

	public function testDelete()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/photo/create')
			->type('Little Kitty', 'name')
			->type('This is a little kitty', 'description')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/photo');

		$this->seeInDatabase('photos', ['name' => 'Little Kitty', 'id' => 5]);

		$this->actingAs($user)
			->visit('/back/photo/5/delete')
			->press('Verwijder')
			->seePageIs('/back/photo');

		$this->notSeeInDatabase('logos', ['name' => 'Little Kitty', 'id' => 5]);
	}
}
