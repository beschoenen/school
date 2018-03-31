<?php

use App\Models\Database\Logo;
use App\Models\Database\Photo;
use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class LogosTest extends TestCase
{
	public function testCreate()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/logo/create')
			->type('Little Kitty', 'name')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/logo');

		$this->seeInDatabase('logos', ['name' => 'Little Kitty']);

		Logo::where('name', 'Little Kitty')->first()->delete();
	}

	public function testUpdate()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/logo/create')
			->type('Little Kitty', 'name')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/logo');

		$this->seeInDatabase('logos', ['name' => 'Little Kitty']);

		$this->actingAs($user)
			->visit('/back/logo/1/edit')
			->type('Big Kitty', 'name')
			->press('Opslaan')
			->seePageIs('/back/logo');

		$this->seeInDatabase('logos', ['name' => 'Big Kitty']);

		Logo::where('name', 'Big Kitty')->first()->delete();
	}

	public function testDelete()
	{
		$user = factory(User::class)->create();

		$this->actingAs($user)
			->visit('/back/logo/create')
			->type('Little Kitty', 'name')
			->attach(base_path('/tests/assets/cat.jpg'), 'image')
			->press('Opslaan')
			->seePageIs('/back/logo');

		$this->seeInDatabase('logos', ['name' => 'Little Kitty', 'id' => 4]);

		$this->actingAs($user)
			->visit('/back/logo/4/delete')
			->press('Verwijder')
			->seePageIs('/back/logo');

		$this->notSeeInDatabase('logos', ['name' => 'Little Kitty', 'id' => 4]);
	}


}
