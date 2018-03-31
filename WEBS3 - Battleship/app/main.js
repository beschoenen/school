$(document).ready(function() {
	var key = getParameterByName('token');

	// Check for api_key cookie
	if(key == null) {
		key = getCookie("api_key");
	}
	// If the key is still empty ask for one
	if(key == null) {
		key = prompt("Please paste your API key here");
	}

	if(key == null) {
		alert("No API key given");
		throw "No key exception";
	}

	CONST.APIKEY = key;
	document.cookie = "api_key=" + key;

	// Create the api route helper
	window.router = new Router();
	// Enable the socket connection
	window.socket = enableSocket();

	window.router.getUserInfo(function(response) {
		window.userInfo = response;
		$("#user-name").text("Hello, " + response.name);
	});

	window.gameListScreen = new GameList(function(game) {
		if(game.status == CONST.STATES.SETUP) {
			window.gameListScreen.destruct();
			placeBoats(game);
		} else if(game.state = CONST.STATES.STARTED) {
			window.gameListScreen.destruct();
			playGame(game);
		}
	});

	window.gameListScreen.show();

	function placeBoats(game) {
		var boatPlacementScreen = new BoatPlacement(game, function() {
			playGame(game);
			boatPlacementScreen.destruct();
		});
	}

	function playGame(game) {
		var gameScreen = new Game(game._id);
	}
});