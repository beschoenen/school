<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class ActivitiesPageTest extends TestCase
{    
    public function testActivitiesPage()
    {
        $this->visit('/activities')->see('Wijk werkplaats Oost - Activiteiten');
    }

    public function testBackEdit()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/activities/Houtwerkplaats/edit')
            ->type('newtopic', 'section')
            ->press('Opslaan')
            ->seePageIs('/back/activities');

        $this->seeInDatabase('texts', ['page' => 'activities', 'section' => 'newtopic']);
    }

    public function testBackCreate()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/activities/create')
            ->type('newtopic', 'section')
            ->press('Opslaan')
            ->seePageIs('/back/activities');

        $this->seeInDatabase('texts', ['page' => 'activities', 'section' => 'newtopic']);
    }

    public function testBackRemove()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/activities/delete/Houtwerkplaats')
            ->press('Verwijder')
            ->seePageIs('/back/activities');

        $this->notSeeInDatabase('texts', ['page' => 'activities', 'section' => 'Houtwerkplaats']);
    }
}