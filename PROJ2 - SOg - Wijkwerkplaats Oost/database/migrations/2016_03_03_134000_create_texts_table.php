<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTextsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('texts', function(Blueprint $table) {
            $table->string('page');
            $table->string('section');
            $table->longText('text');
            $table->timestamps();

            $table->primary(['page', 'section']);

            $table->foreign('page')->references('name')->on('pages');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('texts');
    }
}
