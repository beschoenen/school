<?php

namespace Tests\Feature;

use Tests\TestCase;
use App\Models\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class AuthTest extends TestCase
{
    use DatabaseMigrations;

    public function testProfilePageWillRedirect()
    {
        $response = $this->get('user/profile');

        $response->assertRedirect('/login');
    }

    public function testUserCanViewProfilePageAfterLogin()
    {
        $user = factory(User::class)->create(['name' => 'Bartimeus Avans']);

        $response = $this->actingAs($user)->get('user/profile');

        $response->assertSee("Hallo $user->name!");
    }
}
