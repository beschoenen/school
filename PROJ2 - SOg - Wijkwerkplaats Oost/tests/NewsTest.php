<?php

use App\Models\Database\User;
use Illuminate\Foundation\Testing\DatabaseMigrations;

class NewsTest extends TestCase
{
    public function testNews()
    {
        $user = factory(User::class)->create();

        //Create news
        $this->actingAs($user)
            ->visit('/back/news/create')
            ->type('This is a test', 'title')
            ->type('This is a test for the news pages in the front and backend', 'text')
            ->press('Opslaan')
            ->seePageIs('/back/news');

        $this->seeInDatabase('news', ['title' => 'This is a test']);

        //Update news
        $this->actingAs($user)
            ->visit('/back/news/1/edit')
            ->type('This is a automatic test', 'title')
            ->type('This is a automatic test for the news pages in the front and backend', 'text')
            ->press('Opslaan')
            ->seePageIs('/back/news');

        $this->seeInDatabase('news', ['title' => 'This is a automatic test']);

        //Front news
        $this->visit('/news')->see('Wijk werkplaats Oost - Nieuws');

        //Front article
        $this->visit('/news/1')->see('Wijk werkplaats Oost - Nieuws');
    }
}
