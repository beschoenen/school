<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

use App\Message;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return view('view');
});

Route::post('/', function (Request $request) {
    if (!$request->has(['name', 'password'])) {
        return view('view', ['error' => 'Please fill out your name and password']);
    }

    $cypher = new \Kronos\Encrypt\Cipher\AES(null);

    if ($request->has('message')) {
        $encrypted = $cypher->encrypt($request->message, "$request->name-$request->password");

        $message = Message::where('name', $request->name)->first() ?? new Message();

        $message->message = $encrypted;
        $message->name = $request->name;

        $message->save();

        return view('view', [
            'name' => $request->name,
            'error' => 'Your message has been saved',
        ]);
    }

    $record = Message::where('name', $request->name)->first();

    if ($record == null) {
        return view('view', [
            'name' => $request->name,
            'error' => "No message found for name $request->name!",
        ]);
    }

    $decrypted = $cypher->decrypt($record->message, "$record->name-$request->password");

    if (!$decrypted) {
        return view('view', [
            'name' => $request->name,
            'error' => 'The password was invalid',
        ]);
    }

    return view('view', [
        'name' => $record->name,
        'message' => $decrypted,
    ]);
});