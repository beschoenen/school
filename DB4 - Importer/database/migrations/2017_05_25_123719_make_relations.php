<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class MakeRelations extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('user_movie_ratings', function (Blueprint $table) {
            $table->foreign('user_id')->references('id')->on('users');
            $table->foreign('movie_id')->references('id')->on('movies');
        });

        Schema::table('movie_genres', function (Blueprint $table) {
            $table->foreign('genre_id')->references('id')->on('genres');
            $table->foreign('movie_id')->references('id')->on('movies');
        });

        Schema::table('movie_actors', function (Blueprint $table) {
            $table->foreign('actor_id')->references('id')->on('actors');
            $table->foreign('movie_id')->references('id')->on('movies');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('user_movie_ratings', function (Blueprint $table) {
            $table->dropForeign(['user_id']);
            $table->dropForeign(['movie_id']);
        });

        Schema::table('movie_genres', function (Blueprint $table) {
            $table->dropForeign(['genre_id']);
            $table->dropForeign(['movie_id']);
        });

        Schema::table('movie_actors', function (Blueprint $table) {
            $table->dropForeign(['actor_id']);
            $table->dropForeign(['movie_id']);
        });
    }
}
