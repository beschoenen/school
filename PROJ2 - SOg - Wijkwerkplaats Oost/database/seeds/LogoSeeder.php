<?php

use App\Models\Database\Logo;
use App\Models\Database\Photo;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\File;

class LogoSeeder extends Seeder
{
	/**
	 * Run the database seeds.
	 *
	 * @return void
	 */
	public function run()
	{
		$records = [
			['filename' => 'DenBoschLogo.png', 'name' => 'Den Bosch', 'url' => 'www.s-hertogenbosch.nl'],
			['filename' => 'PeterPeters.jpg', 'name' => 'Peter Peters', 'url' => 'www.petersbouw.nl'],
			['filename' => 'SchadeboLogo.jpg', 'name' => 'Schadebo', 'url' => 'www.schadebo.nl'],
		];

		foreach ($records as $record) {
			$logo = new Logo([
				'filename' => $record['filename'],
				'name' => $record['name'],
				'url' => $record['url']
			]);

			File::copy(base_path("database/seeds/files/logos/{$record['filename']}"), storage_path("logos/{$record['filename']}"));

			$logo->save();
		}
	}
}
