var Router = function() {

	/**
	 * @param route string
	 * @param type [string=GET]
	 * @param options [object]
	 * @param callback function
	 */
	function sendRequest(route, callback, type, options) {
		type = type || "GET";

		var request = new XMLHttpRequest();

		request.open(type, (CONST.HOST + route + "?token=" + CONST.APIKEY), true);

		request.responseType = "json";
		request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

		request.onreadystatechange = function() {
			if (request.readyState == 4 && request.status == 200) {
				if(typeof callback !== 'undefined') {
					callback(request.response);
				} else {
					console.log(request.response);
				}
			}
		};

		if(type !== "GET") {
			request.send(JSON.stringify(options));
		} else {
			request.send();
		}
	}

	return {

		// Get, Get user info
		getUserInfo: function(callback) {
			sendRequest(CONST.ROUTES.USERINFO, callback);
		},

		// GET, Get all games
		getGames: function(callback) {
			sendRequest(CONST.ROUTES.MYGAMES, callback);
		},

		// DELETE, Delete all games
		deleteGames: function(callback) {
			sendRequest(CONST.ROUTES.MYGAMES, callback, "DELETE");
		},

		// GET, Create a new game
		newGame: function(callback) {
			sendRequest(CONST.ROUTES.NEWGAME, callback);
		},

		// GET, Create a new game against the AI
		newAIGame: function(callback) {
			sendRequest(CONST.ROUTES.NEWGAMEAI, callback);
		},

		// GET, Get 1 specific id
		getGame: function(id, callback) {
			sendRequest(CONST.ROUTES.GAME.replace(/:id/, id), callback);
		},

		// GET, Get a list of possible ships to be placed on the game board
		getShips: function(callback) {
			sendRequest(CONST.ROUTES.SHIPS, callback);
		},

		// POST, Submit the placed ships to the server
		postGameboard: function(id, object, callback) {
			sendRequest(CONST.ROUTES.GAMEBOARDS.replace(/:id/, id), callback, "POST", object);
		},

		// POST, Submit a shot to the server
		postShot: function(id, x, y, callback) {
			sendRequest(CONST.ROUTES.SHOTS.replace(/:id/, id), callback, "POST", { x: x, y: y });
		}

	};
};