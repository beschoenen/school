var CONST = {
	APIKEY: null,
	HOST: "https://zeeslagavans.herokuapp.com",
	// HOST: "https://battleship-avans.herokuapp.com",
	// HOST: "http://localhost:3000",
	ROUTES: {
		USERINFO: "/users/me/info",
		MYGAMES: "/users/me/games",
		NEWGAME: "/games",
		NEWGAMEAI: "/games/AI",
		GAME: "/games/:id",
		SHIPS: "/ships",
		GAMEBOARDS: "/games/:id/gameboards",
		SHOTS: "/games/:id/shots"
	},
	STATES: {
		QUEUE: "queue",
		SETUP: "setup",
		STARTED: "started",
		DONE: "done"
	},
	SHOTSTATES: {
		BOOM: "BOOM",
		SPLASH: "SPLASH",
		FAIL: "FAIL",
		WINNER: "WINNER"
	},
	FIELDS: {
		COLUMNS: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'],
		ROWS: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
	}
};

function enableSocket() {

	var listeners = {
		'update': [],
		'turn': [],
		'shot': []
	};

	var socket = io(CONST.HOST, { query: "token=" + CONST.APIKEY });

	socket.on('update', function(update) {
		console.log(update);
		listeners['update'].forEach(function(listener) {
			listener(update);
		});
	});

	socket.on('turn', function(update) {
		console.log(update);
		listeners['turn'].forEach(function(listener) {
			listener(update);
		});
	});

	socket.on('shot', function(update) {
		console.log(update);
		listeners['shot'].forEach(function(listener) {
			listener(update);
		});
	});

	return {
		addListener: function(event, listener) {
			if(!listeners[event]) {
				listeners[event] = [];
			}
			return listeners[event].push(listener);
		},
		removeListener: function(event, listenerId) {
			delete listeners[event][listenerId -1];
		}
	}
}

function createElement(tag, classes, events) {
	classes = classes || null;
	var element = document.createElement(tag);

	if(classes !== null) {
		if(typeof classes == "object") {
			classes.forEach(function(item) {
				element.classList.add(item);
			});
		} else {
			element.classList.add(classes);
		}
	}

	for(var event in events) {
		if(events.hasOwnProperty(event)) {
			element.addEventListener(event, events[event]);
		}
	}
	return element;
}

function getShipFields(start, length, vertical) {
	length = parseInt(length);
	var fields = [];

	var startCol = start[0];
	var startColNumber = CONST.FIELDS.COLUMNS.indexOf(startCol);

	var startRow = parseInt(start.substr(1));

	if(vertical) {
		if(startRow + length > 10) {
			startRow -= (startRow + length - 11);
		}
	} else {
		if(startColNumber + length > 10) {
			startColNumber -= (startColNumber + length - 10);
		}
	}

	for(var i = 0; i < length; i++) {
		if(vertical) {
			fields.push(startCol + (startRow + i));
		} else {
			fields.push((CONST.FIELDS.COLUMNS[startColNumber + i]) + startRow);
		}
	}
	return fields;
}

Handlebars.registerHelper('for', function(cursor, options) {
	var accum = '';
	for(var i = 0; i < cursor; ++i)
		accum += options.fn(i);
	return accum;
});

function getParameterByName(name, url) {
	if (!url) url = window.location.href;
	name = name.replace(/[\[\]]/g, "\\$&");
	var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
		results = regex.exec(url);
	if (!results) return null;
	if (!results[2]) return '';
	return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function getCookie(name) {
	var value = "; " + document.cookie;
	var parts = value.split("; " + name + "=");
	if (parts.length == 2) return parts.pop().split(";").shift();
}