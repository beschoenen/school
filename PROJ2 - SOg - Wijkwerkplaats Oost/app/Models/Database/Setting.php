<?php namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;

/**
 * @property mixed  summary
 * @property string created_at
 */
class Setting extends Model
{
    protected $table = "settings";

    protected $fillable = [
        'name',
        'value'
    ];

    public $timestamps = false;

    public static function getSetting($name){
        $setting = Setting::where('setting', $name)->first();
        return $setting->value;
    }
}
