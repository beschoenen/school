<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class HomeTextSeeder extends Seeder
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
                'section' => 'main',
                'text' => '<p>Welkom op de website van de stichting Wijkwerkplaats oost.</p>
                           <p>Wij houden ons bezig met dagbesteding voor mensen met een beperking. Alle mensen die zich aanmelden als vrijwilliger worden ook zo behandeld en benoemd.</p>
                           <p>Ook de Cliënten vanuit Duinendaal, zijn vrijwilligers en zetten zich in naar KENNEN en KUNNEN.</p>'
            ]
        ];
        Text::insert($records);
    }
}
