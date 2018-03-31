<?php

use App\Models\Database\Photo;
use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class ApiEditorTest extends TestCase
{
	public function testTextApi()
	{
		$this->seeInDatabase('texts', [
			'page' => 'home',
			'section' => 'main'
		]);

		$user = factory(User::class)->create();

		$this->actingAs($user)
			->call('POST', '/back/api/text', [
				'section' => 'home.main',
				'text' => 'Test text'
			]);

		$this->seeInDatabase('texts', [
			'page' => 'home',
			'section' => 'main',
			'text' => 'Test text'
		]);
	}

	public function testPhotoApi()
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

		$request = $this->actingAs($user)->call('GET', '/back/api/images');

		$this->assertResponseOk();

		$json = json_decode($request->getContent());

		$this->assertEquals(1, $json->data[0]->id);

		$this->actingAs($user)
			->call('POST', '/back/api/images', [
				'page' => 'home',
				'images' => [1]
			]);

		$this->seeInDatabase('page_photos', [
			'page' => 'home',
			'photos_id' => 1
		]);
	}
}
