<?php

use App\Models\Database\Page;
use Illuminate\Database\Seeder;

class PageSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $records = [
            ['name' => 'home'],
            ['name' => 'activities'],
            ['name' => 'about'],
            ['name' => 'contact'],
            ['name' => 'help']
        ];

        Page::insert($records);
    }
}
