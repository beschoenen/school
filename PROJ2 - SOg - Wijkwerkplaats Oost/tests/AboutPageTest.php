<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class AboutPageTest extends TestCase
{    
    public function testActivitiesPage()
    {
        $this->visit('/about')->see('Wijk werkplaats Oost - Over ons');
    }

    public function testBackEdit()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/about/Geschiedenis/edit')
            ->type('newtopic', 'section')
            ->press('Opslaan')
            ->seePageIs('/back/about');

        $this->seeInDatabase('texts', ['page' => 'about', 'section' => 'newtopic']);
    }

    public function testBackCreate()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/about/create')
            ->type('newtopic', 'section')
            ->press('Opslaan')
            ->seePageIs('/back/about');

        $this->seeInDatabase('texts', ['page' => 'about', 'section' => 'newtopic']);
    }

    public function testBackRemove()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/about/delete/Geschiedenis')
            ->press('Verwijder')
            ->seePageIs('/back/about');

        $this->notSeeInDatabase('texts', ['page' => 'about', 'section' => 'Geschiedenis']);
    }
}
