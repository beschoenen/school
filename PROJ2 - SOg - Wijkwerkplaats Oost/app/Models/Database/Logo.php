<?php

namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\File;

/**
 * @property integer id
 * @property string  filename
 * @property string  name
 * @property string  url
 */
class Logo extends Model
{
	protected $table = 'logos';

	protected $guarded = ['id'];

	public static $rules = [
		'filename' => 'required|string',
		'name' => 'required|string',
		'url' => 'string'
	];

	public function delete()
	{
		File::delete(storage_path("logos/{$this->filename}"));
		parent::delete();
	}
}