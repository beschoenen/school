<?php

namespace App\Console\Commands;

use Illuminate\Console\Command;
use Illuminate\Support\Facades\DB;

class ImportMovies extends Command
{
    protected $signature = 'import:movies';

    protected $url = 'https://raw.githubusercontent.com/sidooms/MovieTweetings/master/latest/movies.dat';

    protected $regex = "/(\d+)::(.+)\s\((\d+)\)::(.*)/";

    public function handle()
    {
        $this->info('Downloading movies');

        $content = file_get_contents($this->url);

        $this->info('Importing movies');

        $lines = explode("\n", $content);

        $this->output->progressStart(count($lines));

        foreach ($lines as $line) {
            preg_match($this->regex, $line, $matches);

            if (count($matches) < 4) {
                continue;
            }

            $movieId = DB::table('movies')->insertGetId([
                'id' => $matches[1],
                'name' => $matches[2],
                'release_year' => $matches[3]
            ]);

            if (empty($matches[4])) continue;

            $genres = explode("|", $matches[4]);

            foreach ($genres as $genre) {
                $dbGenre = DB::table('genres')->where(['name' => $genre])->first();

                if ($dbGenre == null) {
                    $dbGenre = new \stdClass();
                    $dbGenre->id = DB::table('genres')->insertGetId(['name' => $genre]);
                }

                DB::table('movie_genres')->insert([
                    'movie_id' => $movieId,
                    'genre_id' => $dbGenre->id
                ]);
            }

            $this->output->progressAdvance();

        }

        $this->output->progressFinish();
    }
}
