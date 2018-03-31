<?php namespace App\Models\Database;

use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\Model;
use Mews\Purifier\Facades\Purifier;

/**
 * @property string page
 * @property string section
 * @property string text
 */
class Text extends Model
{
	protected $table = "texts";
    protected $primaryKey = "section";
	public $incrementing = false;

	protected $fillable = [
		'page',
		'section',
		'text'
	];

	public static function getText($page, $section) {
		return Purifier::clean(self::where('page', $page)->where('section', $section)->first()->text);
	}

	public static function getSections($page)
	{
		return self::where('page', $page)->where('section', '!=', 'main')->get();
	}

	public static function getSection($page, $section)
	{
		return Text::where('page', $page)->where('section', $section)->first();
	}

	// Override Laravel method for compound key support
	protected function setKeysForSaveQuery(Builder $query)
	{
		$query->where($this->getKeyName(), '=', $this->getKeyForSaveQuery())
			->where('page', $this->page);

		return $query;
	}
}