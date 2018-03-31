<?php

namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\File;

/**
 * @property integer id
 * @property string  filename
 * @property string  name
 * @property string  description
 * @property mixed   logo
 */
class Photo extends Model
{
	protected $table = 'photos';

	protected $guarded = ['id'];

	public static $rules = [
		'id' => 'integer',
		'name' => 'required|string',
		'description' => 'string'
	];

	public function pages()
	{
		return $this->belongsToMany(Page::class, 'page_photos', 'photos_id', 'page');
	}

	public function delete()
	{
		File::delete(storage_path("thumbs/{$this->filename}"));
		File::delete(storage_path("images/{$this->filename}"));
		parent::delete();
	}
}