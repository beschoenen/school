<?php

namespace App\Http\Controllers;

use App\Models\Database\Logo;
use App\Models\Database\Photo;
use Illuminate\Support\Facades\File;
use Illuminate\Support\Facades\Response;

class MiscController extends Controller
{
	public function image(Photo $photo)
	{
		$path = storage_path("images/{$photo->filename}");
		return Response::make(File::get($path), 200)
			->header('Content-Type', File::mimeType($path))
			->header('Content-Disposition', "inline;filename*=UTF-8''" . rawurlencode($photo->filename));
	}

	public function thumb(Photo $photo)
	{
		$path = storage_path("thumbs/{$photo->filename}");
		return Response::make(File::get($path), 200)
			->header('Content-Type', File::mimeType($path))
			->header('Content-Disposition', "inline;filename*=UTF-8''" . rawurlencode($photo->filename));
	}

	public function logo(Logo $logo)
	{
		$path = storage_path("logos/{$logo->filename}");
		return Response::make(File::get($path), 200)
			->header('Content-Type', File::mimeType($path))
			->header('Content-Disposition', "inline;filename*=UTF-8''" . rawurlencode($logo->filename));
	}
}