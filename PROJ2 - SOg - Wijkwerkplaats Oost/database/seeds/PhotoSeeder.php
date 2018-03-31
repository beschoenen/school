<?php

use App\Models\Database\Photo;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\File;

class PhotoSeeder extends Seeder
{
	/**
	 * Run the database seeds.
	 *
	 * @return void
	 */
	public function run()
	{
		$records = [
			['filename' => 'foto_1.png', 'name' => 'Plein Oost', 'page' => ['home']],
			['filename' => 'foto_2.png', 'name' => 'Creatief atellier', 'page' => ['activities']],
			['filename' => 'foto_3.png', 'name' => 'Walnoten', 'page' => ['about', 'help']],
			['filename' => 'foto_4.png', 'name' => 'Klaasjes snuffelhoek', 'page' => ['activities']],
		];

		foreach($records as $record)
		{
			// Create record
			$photo = new Photo([
				'filename' => $record['filename'],
				'name' => $record['name']
			]);

			// Move files
			File::copy(base_path("database/seeds/files/{$record['filename']}"), storage_path("images/{$record['filename']}"));
			File::copy(base_path("database/seeds/files/thumbs/{$record['filename']}"), storage_path("thumbs/{$record['filename']}"));

			// Save model
			$photo->save();

			// Create pivot records
			$photo->pages()->sync($record['page']);
		}
	}
}
