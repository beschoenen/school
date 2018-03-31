<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class ActivitiesTextSeeder extends Seeder
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
                'page' => 'activities',
                'section' => 'main',
                'text' => '<p>Op deze pagina kunt u informatie vinden over de activiteiten die gedaan worden binnen Werkwijk oost.</p>
                           <p>Zo hebben we verschillende afdelingen zoals de houtwerkplaats of het creatief atelier waar dagelijks mensen aan het werk zijn met mooie voorwerpen maken.</p>'
            ],
            [
                'page' => 'activities',
                'section' => 'Houtwerkplaats',
                'text' => '<p>De houtwerkplaats wordt intensief gebruikt, hier zijn gemiddeld 9 personen werkzaam in de houtbewerking.</p>
                           <p>In de houtwerkplaats houden vrijwilligers zich bezig met het maken van houten voorwerpen zoals vogelhuisjes, beelden en kerstbomen.</p>
                           <p>Deze zijn vervolgens te koop op de markten of op bestelling.</p>
                           <p>Ook houdt de houtwerkplaats zich bezig met reparaties in het gebouw en het maken van hulpmiddelen voor mensen met een beperking.</p>'
            ],
            [
                'page' => 'activities',
                'section' => 'Creatief atelier',
                'text' => '<p>In het creatief atellier zijn er dagelijks 8 tot 9 personen actief bezig met het maken van kunstige voorwerpen.</p>
                           <p>Zo worden er mozaieken gemaakt, geschilderd, getekent en kleding gemaakt. Deze voorwerpen worden vervolgens in de voorraad opgeslagen en verkocht op de markten.</p>
                           <p>Het creatief atellier houdt zich ook bezig met het houden van workshops voor kinderen met het syndroom van down.</p>
                           <p>Dit wordt meestal op een zaterdag gedaan in verband met de drukte.</p>'
            ],
            [
                'page' => 'activities',
                'section' => 'Klaasjes snuffelhoek',
                'text' => '<p>Klaasjes snuffelhoek is de tweede hands winkel van Wijk werkplaats oost.</p>
                           <p>In onze snuffelhoek zijn gebruikte kleding en zelfgemaakte artikelen te koop voor een betaalbare prijs.</p>
                           <p>De Winkel wordt onderhouden door onze oudste vrijwilliger Klaas.</p>
                           <p>Aangezien het onverantwoord was om Klaas alleen de winkel te laten onderhouden helpen andere vrijwilligers Klaas met onder andere sorteren en toezicht houden.</p>'
            ],
            [
                'page' => 'activities',
                'section' => 'Rolstoelen werkplaats',
                'text' => '<p>In de rollators en rolstoelen werkplaats worden rollators en rolstoelen opgeslagen en gerepareerd.</p>
                           <p>Ook worden er huishoudelijke artikelen zoals koffiezetapparaten, lampen en soms computers gerepareerd in de werkplaats voor de wijk bewoners.</p>
                           <p>Rolstoelen en rollators worden soms uitgeleend aan mantelzorgers, om hun beperkte medemens een fijn dagje uit te geven.</p>'
            ]
        ];
        Text::insert($records);
    }
}
