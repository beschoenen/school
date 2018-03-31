<?php

use App\Models\Database\Page;
use App\Models\Database\Photo;
use App\Models\Database\Logo;
use App\Models\Database\Text;
use App\Models\Database\User;
use Faker\Generator;

/*
|--------------------------------------------------------------------------
| Model Factories
|--------------------------------------------------------------------------
|
| Here you may define all of your model factories. Model factories give
| you a convenient way to create models for testing and seeding your
| database. Just tell the factory how a default model should look.
|
*/

$factory->define(User::class, function(Generator $faker) {
    return [
        'name' => $faker->name,
        'email' => $faker->email,
        'password' => bcrypt(str_random(10)),
        'remember_token' => str_random(10)
    ];
});

$factory->define(Page::class, function(Generator $faker) {
    return [
        'name' => $faker->word
    ];
});

$factory->define(Photo::class, function(Generator $faker) {
    return [
        'id' => $faker->numberBetween(1, 100),
        'filename' => $faker->name . "." . $faker->fileExtension,
        'name' => $faker->name,
        'description' => $faker->text
    ];
});

$factory->define(Text::class, function(Generator $faker) {
    return [
        'page' => $faker->word,
        'section' => $faker->word,
        'text' => $faker->text
    ];
});

$factory->define(Logo::class, function(Generator $faker) {
    return [
        'id' => $faker->numberBetween(1, 100),
        'filename' => $faker->name . "." . $faker->fileExtension,
        'name' => $faker->name,
        'description' => $faker->text
    ];
});