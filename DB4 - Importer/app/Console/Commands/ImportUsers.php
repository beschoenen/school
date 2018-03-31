<?php

namespace App\Console\Commands;

use Illuminate\Console\Command;
use Illuminate\Support\Facades\DB;

class ImportUsers extends Command
{
    protected $signature = 'import:users';

    protected $url = 'https://raw.githubusercontent.com/sidooms/MovieTweetings/master/latest/users.dat';

    protected $regex = "/^(\d+)::(\d+)/";

    public function handle()
    {
        $this->info('Downloading users');

        $content = file_get_contents($this->url);

        $this->info('Importing users');

        $lines = explode("\n", $content);

        $this->output->progressStart(count($lines));

        foreach ($lines as $line) {
            preg_match($this->regex, $line, $matches);

            if (count($matches) < 1) {
                continue;
            }

            DB::table('users')->insert([
                'id' => $matches[1],
                'twitter_id' => $matches[2]
            ]);

            $this->output->progressAdvance();
        }

        $this->output->progressFinish();
    }
}
