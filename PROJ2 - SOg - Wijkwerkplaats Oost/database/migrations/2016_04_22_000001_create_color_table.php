<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateColorTable extends Migration   {
        /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('color', function(Blueprint $table) {
            $table->increments('id');
            $table->string('section');
            $table->string('hexcode');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('color');
    }
}
