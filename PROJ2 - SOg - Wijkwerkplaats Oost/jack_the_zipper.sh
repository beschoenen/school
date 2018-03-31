#!/usr/bin/env bash

echo "## Removing old files";
rm -f Installer.tar.gz;

echo "## Updating dependencies";
composer update --no-interaction --prefer-source -v;

echo "## Running composer optimize";
php artisan clear-compiled;
php artisan optimize;

echo "## Zipping project";
tar -cvpzf installer/Website.tar.gz * --exclude-from='.zipignore';

cd installer;

echo "## Zipping installer";
tar -cvpzf ../Installer.tar.gz *;

echo "## Removing Website archive";
rm -f Website.tar.gz;

echo "## Done"