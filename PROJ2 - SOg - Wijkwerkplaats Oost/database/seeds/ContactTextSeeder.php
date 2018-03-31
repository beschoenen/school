<?php

use App\Models\Database\Text;
use Illuminate\Database\Seeder;

class ContactTextSeeder extends Seeder
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
                'page' => 'contact',
                'section' => 'left',
                'text' => '<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d536.3571355267064!2d5.330919251074667!3d51.69633144012219!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47c6efac269dd9af%3A0x87ce67c8bef776e8!2sVan+Broeckhovenlaan+4A%2C+5213+HX+&#39;s-Hertogenbosch!5e0!3m2!1snl!2snl!4v1457032949237" frameborder="0" style="border:0" allowfullscreen></iframe>'
            ],
            [
                'page' => 'contact',
                'section' => 'right',
                'text' => '<h2>Wijkwerkplaats Oost</h2>
                           <p>van Broeckhovenlaan 4A</p>
                           <p>5213 HX \'s-Hertogenbosch</p>
                           <p>&nbsp;</p>
                           <h2>Contactpersonen</h2>
                           <h4>Henk van der Heijden</h4>
                           <p>Telefoonnummer: {Telefoonnummer}</p>
                           <p>Emailadres: henkofria@home.nl</p>
                           <h4>Nelly van de Ven</h4>
                           <p>Telefoonnummer: 06-53352507</p>
                           <p>Emailadres: vdven21@hetnet.nl</p><br/>
                           <p>U kunt ook <a href="' . route('front.contactform', [], false) . '">hier</a> klikken om contact op te nemen. </p>'
            ],
        ];
        Text::insert($records);
    }
}