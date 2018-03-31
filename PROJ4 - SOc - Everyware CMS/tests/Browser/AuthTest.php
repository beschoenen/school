<?php

namespace Tests\Browser;

use App\Models\User;
use Tests\DuskTestCase;
use Laravel\Dusk\Browser;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class AuthTest extends DuskTestCase
{
    use DatabaseMigrations;

    /**
     * Test the authentication.
     *
     * @return void
     */
    public function testAuthTest()
    {
        // Create user
        $user = factory(User::class)->create([
            'name' => 'Bartimeus Avans',
        ]);

        // Check if HTTP 403 if not logged in
        $this->get('/user/profile')
             ->assertRedirect('/login');

        // Check if name is displayed when logged in
        $this->browse(function (Browser $browser) use ($user) {
            $browser->loginAs($user)
                    ->visit('/user/profile')
                    ->assertSee("Hallo $user->name!");
        });
    }
}
