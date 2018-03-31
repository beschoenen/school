<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class SayingTextSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $records = [
            [
                'page' => 'home',
                'section' => 'saying',
                'text' => 'Noten mag je kraken, mensen nooit.'
            ]
        ];
        Text::insert($records);
    }
}