<?php

namespace App\Models\Database;
use Illuminate\Database\Eloquent\Model;

class Color extends Model
{
    protected $table = "color";
    protected $primaryKey = "id";

    protected $fillable = [
		'id',
		'section',
		'hexcode'
    ];

	public static function getDarkerColor($hex)
	{
		list($r, $g, $b) = sscanf($hex, "#%02x%02x%02x");
		return "#" .
			str_pad(dechex(max(0, $r - 40)), 2, "0", STR_PAD_LEFT) .
			str_pad(dechex(max(0, $g - 40)), 2, "0", STR_PAD_LEFT) .
			str_pad(dechex(max(0, $b - 40)), 2, "0", STR_PAD_LEFT);
	}

	public static function getActiveColor($hex)
	{
		list($r, $g, $b) = sscanf($hex, "#%02x%02x%02x");
		return "#" .
			str_pad(dechex(max(0, $r - 70)), 2, "0", STR_PAD_LEFT) .
			str_pad(dechex(max(0, $g - 70)), 2, "0", STR_PAD_LEFT) .
			str_pad(dechex(max(0, $b - 70)), 2, "0", STR_PAD_LEFT);
	}
}