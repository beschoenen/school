<?php namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;
use Carbon\Carbon;

/**
 * @property mixed text
 */
class Article extends Model
{
	protected $table = "news";

	protected $fillable = [
		'title',
		'text',
		'created_at'
	];

	public function getText()
	{
		if (strlen($this->text) > 100) {
			$offset = (100 - 3) - strlen($this->text);

			return substr($this->text, 0, strrpos($this->text, ' ', $offset)) . '...';
		}

		return $this->text;
	}
}
