<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Tour extends Model
{
    public $guarded = ['id'];

    public $casts = [
        'id' => 'integer',
    ];

    public $dates = [
        'created_at',
        'updated_at',
    ];

    public function nodes(): HasMany
    {
        return $this->hasMany(TourNode::class);
    }
}
