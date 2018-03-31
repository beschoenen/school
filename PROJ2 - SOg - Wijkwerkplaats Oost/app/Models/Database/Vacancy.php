<?php namespace App\Models\Database;

use Illuminate\Database\Eloquent\Model;
use Carbon\Carbon;

/**
 * @property integer id
 * @property mixed  summary
 * @property string created_at
 */
class Vacancy extends Model
{
	protected $table = "vacancies";

	protected $fillable = [
		'title',
		'summary',
		'text',
		'created_at'
	];

	public function getSummary()
	{
		if (strlen($this->summary) > 100) {
			$offset = (100 - 3) - strlen($this->summary);

			return substr($this->summary, 0, strrpos($this->summary, ' ', $offset)) . '...';
		}

		return $this->summary;
	}
}
