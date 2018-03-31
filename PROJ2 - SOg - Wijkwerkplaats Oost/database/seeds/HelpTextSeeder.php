<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class HelpTextSeeder extends Seeder 
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
                'page' => 'help',
                'section' => 'main',
                'text' => '<p>Welkom.</p>
                           <p>Bedankt dat u ons wilt helpen.</p>
                           <p>Selecteer nu aan de linkerkant een specefieke pagina.</p>'
            ],
            [
                'page' => 'help',
                'section' => 'sponsors',
                'text' => '<p>Heeft u een bedrijf rond Hertogenbosch.</p>
                           <p>Wij van wijkwerkplaats Oost kunnen u help gebruiken.</p>
                           <p>Heeft u materialen over die u toch weggooit.</p>
                           <p>Doneer het aan ons.</p>
                           <p>U bent van die materialen af en wij kunnen mensen aan het werk houden.</p>
                           <p>Neem gerust contact met ons op.</p>
                           <p>Alvast bedankt.</p>'
            ],
            [
                'page' => 'help',
                'section' => 'trainees',
                'text' => '<p>Ben jij opzoek naar een stageplek en werk je graag met mensen?</p>
                           <p>Kom dan stage lopen bij Wijkwerkplaats oost.</p>
                           <p>&nbsp;</p>
                           <p>Heb je intresse om stage te komen lopen, neem dan contact met ons op.</p>'
            ],
            [
                'page' => 'help',
                'section' => 'volunteers',
                'text' => '<p>Wij van Wijkgroep Oost zijn een vrijwilligers orginisatie.</p>
                           <p>Om onze mensen allemaal goed te blijven begeleiden.</p>
                           <p>Zijn we bereid om het team te versterken.</p>
                           <p>Bent u een persoon die graag mensen helpt, en is uw intresse gewekt?</p>
                           <p>Neem gerust contact met ons op.</p>'
            ]
        ];
        Text::insert($records);
    }
}
