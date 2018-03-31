<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTournodesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('tour_nodes', function (Blueprint $table) {
            $table->increments('id');
            $table->string('node');
            $table->integer('tour_id');
            $table->string('next_node')->nullable();
            $table->string('spoken_description');
            $table->string('written_description')->nullable();
            $table->integer('direction')->unsigned();
            $table->boolean('is_start')->default(false);
            $table->timestamps();

            $table->foreign('tour_id')->references('id')->on('tours')->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('tour_nodes');
    }
}
