<?php

use App\Models\Database\Color;
use Illuminate\Database\Seeder;

class ColorTextSeeder extends Seeder 
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $seeder = [
            [
                'id' => 1,
                'section' => 'menu background',
                'hexcode' => '#EF340E'
            ],
            [
                'id' => 2,
                'section' => 'menu text',
                'hexcode' => '#FFFFFF'
            ],
            [
                'id' => 3,
                'section' => 'saying',
                'hexcode' => '#EF340E'
            ]
        ];
        
        Color::insert($seeder);
    }
}
