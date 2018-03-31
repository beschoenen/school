<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TourNode extends Model
{
    public $guarded = ['id'];

    public $casts = [
        'tour_id' => 'integer',
        'direction' => 'integer',
        'is_start' => 'boolean',
    ];

    public $dates = [
        'created_at',
        'updated_at',
    ];
}
