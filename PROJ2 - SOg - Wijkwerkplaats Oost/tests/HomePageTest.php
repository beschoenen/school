<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class HomePageTest extends TestCase
{
    public function testHomePage()
    {
        $this->visit('/')->see('Wijk werkplaats Oost - Thuis');
    }
}
