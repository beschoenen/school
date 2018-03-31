<?php

use App\Models\Database\User;

class ColorpickerTest extends TestCase
{
    public function testUpdate()
    {
        $user = factory(User::class)->create();

        $this->actingAs($user)
            ->visit('/back/colorpicker')
            ->type('#23ad7f', 'menucolor')
            ->type('#23ad7f', 'sayingcolor')
            ->type('#23ad7f', 'textcolor')
            ->press('Opslaan')
            ->seePageIs('/back/colorpicker');

        $this->seeInDatabase('color', ['id' => 1, 'hexcode' => '#23ad7f']);
        $this->seeInDatabase('color', ['id' => 2, 'hexcode' => '#23ad7f']);
        $this->seeInDatabase('color', ['id' => 3, 'hexcode' => '#23ad7f']);
    }
}

