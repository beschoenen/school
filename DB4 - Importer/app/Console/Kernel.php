<?php

namespace App\Console;

use App\Console\Commands\ImportMovies;
use App\Console\Commands\ImportRatings;
use App\Console\Commands\ImportUsers;
use App\Console\Commands\LoadUsers;
use Illuminate\Foundation\Console\Kernel as ConsoleKernel;

class Kernel extends ConsoleKernel
{
    /**
     * The Artisan commands provided by your application.
     *
     * @var array
     */
    protected $commands = [
        ImportMovies::class,
        ImportUsers::class,
        ImportRatings::class,
        LoadUsers::class
    ];
}
