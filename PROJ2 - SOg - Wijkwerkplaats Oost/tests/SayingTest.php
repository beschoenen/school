<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\WithoutMiddleware;
use Illuminate\Foundation\Testing\DatabaseMigrations;
use Illuminate\Foundation\Testing\DatabaseTransactions;
use App\Models\Database\Text;

class SayingTest extends TestCase
{
    public function testSaying()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/saying')
            ->type('testspreuk', 'text')
            ->press('Opslaan')
            ->seePageIs('/back/saying');

        $this->seeInDatabase('texts', ['page' => 'home', 'section' => 'saying', 'text' => 'testspreuk']);
    }
}
