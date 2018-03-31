<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class HelpPageTest extends TestCase 
{
    public function testHelpPage()
    {
        $this->visit('/help')->see('Wijk werkplaats Oost - Hulp');
    }
}
