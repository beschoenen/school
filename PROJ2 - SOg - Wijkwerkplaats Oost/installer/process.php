<?php

use Illuminate\Support\Facades\Artisan;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Hash;

ini_set('display_errors', '1');
ini_set('memory_limit', '100M');
set_time_limit(180);
$root = __DIR__;

//////////////////////////
// Check MySQL Connection
$mysqli = null;
if ($_POST['DB_CONNECTION'] == "mysql") {
	// Test connection, suppress errors so the script doesn't die
	$mysqli = @(new mysqli($_POST['DB_HOST'], $_POST['DB_USERNAME'], $_POST['DB_PASSWORD'], null, $_POST['DB_PORT']));

	if ($mysqli->connect_errno > 0) {
		print_message($mysqli->connect_error);
	}
}

///////////////////////////////
// Check if tar archive exists
if (!file_exists('Website.tar.gz')) {
	print_message("Het archief kan niet worden gevonden");
}

//////////////////////
// Unpack the archive
try {
	// Extract .gz file
	$p = new PharData('Website.tar.gz');
	$p->decompress();

	// Delete compressed archive
	unlink('Website.tar.gz');

	// Extract files
	$phar = new PharData('Website.tar');
	$phar->extractTo($root);

	// Delete .tar file
	unset($phar);
	PharData::unlinkArchive('Website.tar');
} catch (Exception $e) {
	print_message($e->getMessage());
}

/////////////////
// Make .env file
make_env_file($root);

create_missing_folders($root);

/////////////////
// Make database
if($_POST['DB_CONNECTION'] == "sqlite") {
	$file = fopen("$root/database/database.sqlite", 'w');
	fclose($file);
} else {
	$mysqli->query("CREATE SCHEMA IF NOT EXISTS {$_POST['DB_DATABASE']}");
	$mysqli->close();
}

////////////////
// Boot Laravel
require "$root/bootstrap/autoload.php";
$app = require_once "$root/bootstrap/app.php";

$kernel = $app->make('Illuminate\Contracts\Http\Kernel');

$response = $kernel->handle(
	$request = Illuminate\Http\Request::capture()
);

////////////////////////
// Migrate the database
Artisan::call('migrate:refresh', ['--seed' => true, '--force' => true]);

///////////////
// Create admin
DB::insert('insert into users (name, email, password) values ("Beheerder", ?, ?)', [
	$_POST['_USER_EMAIL'],
	Hash::make($_POST['_USER_PASSWORD'])
]);

if($_POST['_FORM_ENABLE'] == "1") {
	DB::table('settings')->where('setting', 'contact_form_enabled')->update(['value' => 1]);
	DB::table('settings')->where('setting', 'contact_form_email')->update(['value' => $_POST['_FORM_EMAIL']]);
}

////////////////////////
// Print .htaccess file
print_htaccess($root);

////////////////////////
// Delete install files
unlink(__FILE__);
unlink("index.php");

///////////////////////
// Send finish message
print_message("De website is geinstalleerd!", false);


//////////////////
// Helper methods

function create_missing_folders($root = '.') {
	foreach (['images', 'thumbs', 'logos', 'framework/cache', 'framework/sessions', 'framework/views'] as $folder) {
		if (!is_dir("$root/storage/$folder")) {
			mkdir("$root/storage/$folder", 0755, true);
		}
	}
}

function make_env_file($root = '.') {
	$file = "";
	$array = array_merge($_POST, [
		'APP_KEY' => str_shuffle("wijkwerkplaatsoostshertogenbosch"),
		'MAIL_DRIVER' => 'smtp'
	]);

	foreach($array as $key => $object) {
		if(!empty($object) && substr($key, 0, 1) != '_') {
			$file .= "$key=$object\n";
		}
	}
	file_put_contents("$root/.env", $file);
}

function print_htaccess($root = '.') {
	file_put_contents("$root/.htaccess", trim("
		<IfModule mod_rewrite.c>
			RewriteEngine On
			RewriteRule ^(.*)$ public/$1 [L]
		</IfModule>
	"));
}

function print_message($message, $error = true) {
	echo json_encode([
		'error' => $error,
		'message' => $message
	]);
	exit;
}