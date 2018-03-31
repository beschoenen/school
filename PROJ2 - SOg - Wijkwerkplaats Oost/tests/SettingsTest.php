<?php

use App\Models\Database\User;

class SetingsTest extends TestCase
{
    public function testContactPrefs()
    {
        $user = factory(User::class)->create();
        $this->actingAs($user)
            ->visit('/back/settings')
            ->check('pref_form')
            ->type('admin@admin.com', 'email')
            ->press('Opslaan')
            ->seePageIs('/back/settings');

        $this->seeInDatabase('settings', ['setting' => 'contact_form_enabled', 'value' => 1]);
        $this->seeInDatabase('settings', ['setting' => 'contact_form_email', 'value' => "admin@admin.com"]);
    }
}