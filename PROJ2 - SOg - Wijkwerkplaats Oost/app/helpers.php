<?php

function isActiveRoute($routeName, $output = "active")
{
	if(Route::currentRouteName() == $routeName) {
		return $output;
	}
	return null;
}

function areActiveRoutes(array $routeNames, $output = "active")
{
	if(in_array(Route::currentRouteName(), $routeNames)) {
		return $output;
	}
	return null;
}