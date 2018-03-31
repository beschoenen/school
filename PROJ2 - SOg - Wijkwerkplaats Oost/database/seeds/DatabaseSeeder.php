<?php

use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $this->call(PageSeeder::class);

        $this->call(ContactTextSeeder::class);

        $this->call(HomeTextSeeder::class);
        $this->call(ActivitiesTextSeeder::class);
        $this->call(AboutTextSeeder::class);

        $this->call(HelpTextSeeder::class);

        $this->call(SayingTextSeeder::class);
        
        $this->call(ColorTextSeeder::class);
        
        $this->call(VacancySeeder::class);

        $this->call(PhotoSeeder::class);
        
        $this->call(LogoSeeder::class);

        $this->call(SettingsSeeder::class);
    }
}
