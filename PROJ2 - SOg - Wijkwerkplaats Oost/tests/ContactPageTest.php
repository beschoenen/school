<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;
use Illuminate\Foundation\Testing\DatabaseTransactions;

class ContactPageTest extends TestCase
{
    use DatabaseTransactions;

    public function testFrontEnd()
    {
        $this->visit('/contact')->see('Wijk werkplaats Oost - Contact');
    }
}
