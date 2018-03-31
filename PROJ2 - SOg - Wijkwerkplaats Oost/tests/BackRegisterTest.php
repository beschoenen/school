<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class BackRegisterTest extends TestCase
{
    public function testBackRegister()
    {
        $this->visit('/back/register')
            ->type('Jan Janssen', 'name')
            ->type('JanJanssen@hetnet.nl', 'email')
            ->type('MijnWachtwoord', 'password')
            ->type('MijnWachtwoord', 'password_confirmation')
            ->press('Registreer')
            ->seePageIs('/back');

        $this->seeInDatabase('users', ['email' => 'JanJanssen@hetnet.nl']);
    }
}
