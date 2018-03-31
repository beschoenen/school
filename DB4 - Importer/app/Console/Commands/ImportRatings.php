<?php

namespace App\Console\Commands;

use Carbon\Carbon;
use Illuminate\Console\Command;
use Illuminate\Support\Facades\DB;

class ImportRatings extends Command
{
    protected $signature = 'import:ratings';

    protected $url = 'https://raw.githubusercontent.com/sidooms/MovieTweetings/master/latest/ratings.dat';

    protected $regex = "/^(\d+)::(\d+)::(\d+)::(\d+)/";

    public function handle()
    {
        $this->info('Downloading ratings');

        $content = file_get_contents($this->url);

        $this->info('Importing ratings');

        $lines = explode("\n", $content);

        $this->output->progressStart(count($lines));

        foreach ($lines as $line) {
            preg_match($this->regex, $line, $matches);

            if (count($matches) < 5) {
                continue;
            }

            DB::table('user_movie_ratings')->insert([
                'user_id' => $matches[1],
                'movie_id' => $matches[2],
                'rating' => $matches[3],
                'timestamp' => Carbon::createFromTimestamp($matches[4])->toDateTimeString()
            ]);

            $this->output->progressAdvance();

        }

        $this->output->progressFinish();
    }
}
