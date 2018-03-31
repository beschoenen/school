<?php

use App\Models\Database\User;

class UserSetingsTest extends TestCase
{
    public function testPasswordChange()
    {
        $user = new User([
            'name' => 'Bob',
            'email' => 'Bob@bob.nu',
            'password' => bcrypt('123456')
        ]);
        $user->save();

        $this->actingAs($user)
            ->visit('/back/changepass')
            ->type('123456', 'currentpass')
            ->type('654321', 'newpass')
            ->type('654321', 'repeatpass')
            ->press('Wachtwoord wijzigen')
            ->seePageIs('/back/changepass');
        
        $this->assertTrue(Auth::check(bcrypt('654321'), $user->password));
    }
}