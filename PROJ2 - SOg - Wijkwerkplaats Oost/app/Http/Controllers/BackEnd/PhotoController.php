<?php namespace App\Http\Controllers\BackEnd;

use App\Http\Controllers\Controller;
use App\Models\Database\Photo;
use Folklore\Image\Facades\Image;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\File;
use Illuminate\Support\Facades\Input;
use Imagine\Image\Box;
use Response;

class PhotoController extends Controller
{
	/**
	 * Display a listing of the resource.
	 *
	 * @return Response
	 */
	public function index()
	{
		return view('back.photo.index', [
			'photos' => Photo::paginate(),
			'deleteUrl' => route('back.photo.deleteMultiple'),
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
		return view('back.photo.create', [
			'url' => route('back.photo.store'),
			'type' => 'POST',
			'photo' => new Photo()
		]);
	}

	public function createMultiple()
	{
		return view('back.photo.createMultiple', [
			'url' => route('back.photo.storeMultiple'),
			'type' => 'POST',
			'photo' => new Photo()
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
			'description' => 'string'
		]);

		$image = Input::file('image');
		$filename = $image->getClientOriginalName();

		$photo = new Photo([
			'name' => Input::get('name'),
			'filename' => $filename,
			'description' => Input::get('description', null)
		]);

		$image->move(storage_path("images"), $filename);
		$this->createThumbnail($filename);

		$photo->save();

		return redirect()->route('back.photo.index')->withMessage("Uw foto is geupload");
	}

	/**
	 * @param Request $request
	 * @return Response
	 */
	public function storeMultiple(Request $request)
	{
		$this->validate($request, [
			'image' => 'array|required'
		]);

		foreach(Input::file('image') as $image) {
			$filename = $image->getClientOriginalName();

			$photo = new Photo([
				'name' => $filename,
				'filename' => $filename
			]);

			$image->move(storage_path("images"), $filename);
			$this->createThumbnail($filename);

			$photo->save();
		}

		return redirect()->route('back.photo.index')->withMessage("Uw foto's zijn geupload");
	}

	/**
	 * Show the form for editing the specified resource.
	 *
	 * @param  Photo $photo
	 * @return Response
	 */
	public function edit(Photo $photo)
	{
		return view('back.photo.edit', [
			'url' => route('back.photo.update', $photo->id),
			'type' => 'PATCH',
			'photo' => $photo
		]);
	}

	/**
	 * Update the specified resource in storage.
	 *
	 * @param  Photo $photo
	 * @return Response
	 */
	public function update(Photo $photo)
	{
		$photo->update(Input::only('name', 'description'));

		return redirect()->route('back.photo.index')->withMessage('Uw foto is bijgewerkt');
	}

	/**
	 * Remove the specified resource from storage.
	 *
	 * @param  Photo $photo
	 * @return Response
	 */
	public function delete(Photo $photo)
	{
		return view('back.photo.delete', [
			'url' => route('back.photo.destroy', $photo->id),
			'type' => 'delete',
			'return' => route('back.photo.index'),
			'photo' => $photo
		]);
	}

	/**
	 * Remove the specified resource from storage.
	 *
	 * @param  Photo $photo
	 * @return Response
	 */
	public function destroy(Photo $photo)
	{
		$photo->delete();

		return redirect()->route('back.photo.index')->withMessage("Uw foto is verwijderd");
	}

	/**
	 * Remove multiple resources from storage.
	 *
	 * @return Response
	 */
	public function deleteMultiple()
	{
		return view('back.photo.deleteMultiple', [
			'url' => route('back.photo.destroyMultiple'),
			'type' => 'delete',
			'return' => route('back.photo.index'),
			'rows' => Photo::whereIn('id', Input::get('photo'))->get()->chunk(4)
		]);
	}

	/**
	 * Remove multiple resources from storage.
	 *
	 * @return Response
	 */
	public function destroyMultiple()
	{
		foreach(Photo::whereIn('id', Input::get('photo'))->get() as $photo) {
			$photo->delete();
		}

		return redirect()->route('back.photo.index')->withMessage("Uw foto's zijn verwijderd");
	}

	private function createThumbnail($filename)
	{
		Image::open(storage_path("images/$filename"))
			->thumbnail(new Box(300, 300))
			->save(storage_path("thumbs/$filename"));

	}
}