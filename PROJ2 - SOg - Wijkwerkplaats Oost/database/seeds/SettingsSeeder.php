<?php

use Illuminate\Database\Seeder;
use App\Models\Database\Setting;

class SettingsSeeder extends Seeder
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
                'setting' => 'contact_form_enabled',
                'value' => '0'
            ],
            [
                'setting' => 'contact_form_email',
                'value' => ''
            ],
            [
                'setting' => 'backend_color_scheme',
                'value' => '1'
            ],
        ];
        Setting::insert($records);
    }
}
