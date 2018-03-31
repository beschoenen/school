<?php namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;

/**
 * @property string name
 */
class Page extends Model
{
	protected $table = "pages";

	protected $fillable = ['name'];

	protected $primaryKey = "name";

	public $timestamps = false;

	public $incrementing = false;

	public function texts()
	{
		return $this->hasMany(Text::class, 'page', 'name');
	}

	public function photos()
	{
		return $this->belongsToMany(Photo::class, 'page_photos', 'page', 'photos_id', 'name');
	}
}