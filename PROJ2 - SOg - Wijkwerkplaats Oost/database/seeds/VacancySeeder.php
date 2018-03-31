<?php

use Illuminate\Database\Seeder;
use App\Models\Database\Vacancy;

class VacancySeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $seeder = [
            'title' => 'Administratief Medewerker Projectleiding',
            'summary' => 'Tijdelijke Administratief Medewerker Projectleiding',
            'text' => 'De afdeling projectleiding verzorgt de begeleiding, bewaking en planning van de projecten die wij in opdracht hebben. Tevens is de projectleiding verantwoordelijk voor de planning en aansturing van alle montagewerkzaamheden op de diverse bouwprojecten. Binnen het team bied jij de administratieve en secretariële ondersteuning aan de projectleiders en de projectplanner. Je taken bestaan uit het afhandelen van het inkomende telefoon- en emailverkeer, het voorbereiden van de facturatie en het verwerken van gegevens in de administratieve systemen. Hierbij verzorg je ook alle controletaken voor de diverse projectonderdelen. Kortom, het is een veelzijdige functie binnen een dynamische werkomgeving.',
			'created_at' => '2016-02-06 06:05:12'
        ];
        
        $vacancy = new Vacancy($seeder);
        $vacancy->save();
    }
}
