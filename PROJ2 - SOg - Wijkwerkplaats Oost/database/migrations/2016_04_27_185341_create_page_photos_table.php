<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;
use Illuminate\Support\Facades\Schema;

class CreatePagePhotosTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('page_photos', function(Blueprint $table) {
            $table->string('page');
            $table->integer('photos_id')->unsigned();
            $table->timestamps();

            $table->primary(['page', 'photos_id']);

            $table->foreign('page')->references('name')->on('pages');
            $table->foreign('photos_id')->references('id')->on('photos');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('page_photos');
    }
}
