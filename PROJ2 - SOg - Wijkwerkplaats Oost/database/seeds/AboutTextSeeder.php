<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class AboutTextSeeder extends Seeder
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
                'page' => 'about',
                'section' => 'main',
                'text' => '<p>Op deze pagina kunt u informatie over ons vinden. Hoe we zijn begonnen en wat we momenteel doen.</p>'
            ],
            [
                'page' => 'about',
                'section' => 'Geschiedenis',
                'text' => '<p>30 jaar geleden werd door het opbouwwerk PIET Willems. Activiteiten in het leven geroepen voor mensen die werkloos waren geworden, of in de WAO waren.</p>
                            <p>Oost was en is een arbeiderswijk, waar veel laaggeschoolden wonen, en de werkloosheid groot was.</p>
                            <p>Er werd een houtbewerkers groep opgericht, en een klokkenmakers groep. Beiden groepen bestaan nog, de klokkenmakers hebben een ruimte op zuid, en de houtbewerkers, zijn gegroeid naar Wijkwerkplaats Oost.</p>
                            <p>Door de jaren heen veranderde de interesse van de leden van de houtbewerking. Ze wilde een breder publiek bereiken, en zich maatschappelijk inzetten als groep.</p>
                            <p>17 jaar geleden zochten ze contact met Duinendaal, waar mensen met een beperking, ( Niet aangeboren hersenletsel ), hun dagbesteding hadden.</p>
                            <p>En nodigde hun uit om een dag per week in Wijkwerkplaats oost mee te doen, in de houtbewerking. Dit om de hospitalisering tegen te gaan, en mensen weer mee te laten doen in de Samenleving. Waardoor het ontwikkelen van activiteiten buiten houtbewerking mogelijkheden kreeg.</p>'
            ]

        ];
        Text::insert($records);
    }
}