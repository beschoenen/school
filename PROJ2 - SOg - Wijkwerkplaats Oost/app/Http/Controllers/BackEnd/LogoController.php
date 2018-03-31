<?php namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use App\Models\Database\Logo;
use Folklore\Image\Facades\Image;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\File;
use Illuminate\Support\Facades\Input;
use Imagine\Image\Box;

class LogoController extends Controller
{
	/**
	 * Display a listing of the resource.
	 *
	 * @return Response
	 */
	public function index()
	{
		return view('back.logo.index', [
			'logos' => Logo::paginate(),
			'deleteUrl' => route('back.logo.deleteMultiple'),
			'type' => 'POST'
		]);
	}

	/**
	 * Show the form for creating a new resource.
	 *
	 * @return Response
	 */
	public function create()
	{
		return view('back.logo.create', [
			'url' => route('back.logo.store'),
			'type' => 'POST',
			'logo' => new Logo()
		]);
	}

	/**
	 * Store a newly created resource in storage.
	 *
	 * @param Request $request
	 *
	 * @return Response
	 */
	public function store(Request $request)
	{
		$this->validate($request, [
			'image' => 'required|image',
			'name' => 'required|string',
		]);

		$image = Input::file('image');
		$filename = $image->getClientOriginalName();

		$logo = new Logo([
			'filename' => $filename,
			'name' => Input::get('name'),
			'url' => $this->trimUrl(Input::get('url'))
		]);

		$image->move(storage_path("images"), $filename);
		$this->createThumbnail($filename);

		$logo->save();

		return redirect()->route('back.logo.index')->withMessage("Uw logo is geupload");
	}

	/**
	 * Show the form for editing the specified resource.
	 *
	 * @param  Logo $logo
	 * @return Response
	 */
	public function edit(Logo $logo)
	{
		return view('back.logo.edit', [
			'url' => route('back.logo.update', $logo->id),
			'type' => 'PATCH',
			'logo' => $logo
		]);
	}

	/**
	 * Update the specified resource in storage.
	 *
	 * @param  Logo $logo
	 * @return Response
	 */
	public function update(Logo $logo)
	{
		$logo->update([
			'name' => Input::get('name'),
			'url' => $this->trimUrl(Input::get('url'))
		]);

		return redirect()->route('back.logo.index')->withMessage('Uw logo is bijgewerkt');
	}

	/**
	 * Remove the specified resource from storage.
	 *
	 * @param  Logo $logo
	 * @return Response
	 */
	public function delete(Logo $logo)
	{
		return view('back.logo.delete', [
			'url' => route('back.logo.destroy', $logo->id),
			'type' => 'delete',
			'return' => route('back.logo.index'),
			'logo' => $logo
		]);
	}

	/**
	 * Remove the specified resource from storage.
	 *
	 * @param  Logo $logo
	 * @return Response
	 */
	public function destroy(Logo $logo)
	{
		$logo->delete();

		return redirect()->route('back.logo.index')->withMessage("Uw logo is verwijderd");
	}

	/**
	 * Remove multiple resources from storage.
	 *
	 * @return Response
	 */
	public function deleteMultiple()
	{
		return view('back.logo.deleteMultiple', [
			'url' => route('back.logo.destroyMultiple'),
			'type' => 'delete',
			'return' => route('back.logo.index'),
			'rows' => Logo::whereIn('id', Input::get('logo'))->get()->chunk(4)
		]);
	}

	/**
	 * Remove multiple resources from storage.
	 *
	 * @return Response
	 */
	public function destroyMultiple()
	{
		foreach(Logo::whereIn('id', Input::get('logo'))->get() as $logo) {
			$logo->delete();
		}

		return redirect()->route('back.logo.index')->withMessage("Uw logo's zijn verwijderd");
	}

	// Remove the http(s) part from the url
	private function trimUrl($url)
	{
		return preg_replace("/^https?:\/\//", '', $url);
	}

	private function createThumbnail($filename)
	{
		Image::open(storage_path("images/$filename"))
			->thumbnail(new Box(200, 200))
			->save(storage_path("logos/$filename"));

		File::delete(storage_path("images/$filename"));
	}
}
