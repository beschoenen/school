var Game = function(gameId) {

	var game = null;
	var myTurn = null;
	var lastTurn = null;

	var gameInfo = null;

	$.get('app/templates/game.hbs', function(result) {
		var template = Handlebars.compile(result);

		$("#board").html(template());

		$.get('app/templates/grid.hbs', function(result) {
			var render = Handlebars.compile(result);

			// Enemy board
			$('.enemy-container').first().html(render({
				rows: CONST.FIELDS.ROWS,
				cols: CONST.FIELDS.COLUMNS,
				id: "enemy"
			}));

			$('.own-container').first().html(render({
				rows: CONST.FIELDS.ROWS,
				cols: CONST.FIELDS.COLUMNS,
				id: "own"
			}));

			$.get('app/templates/gameInfo.hbs', function(template) {
				gameInfo = Handlebars.compile(template);

				download();
			});
		});
	});

	function download() {
		window.router.getGame(gameId, function(freshGame) {
			game = freshGame;
			myTurn = game.yourTurn;
			render();
		});
	}

	function renderGameInfo() {
		$(".game-info-container").first().html(gameInfo({
			turn: game.yourTurn ? window.userInfo.name : game.enemyName,
			won: game.status == "done" ? (game.youWon ? "You won" : "You lost") : "Still playing",
			last: lastTurn == null ? "No plays yet" : lastTurn.player + " shot at " + lastTurn.field + " and it " + lastTurn.status
		}));
	}

	function render() {
		renderGameInfo();

		game.myGameboard.ships.forEach(function(ship) {
			getShipFields(ship.startCell.x.toUpperCase() + ship.startCell.y, ship.length, ship.isVertical)
			.forEach(function(cell) {
				$("#own").find(".field-" + cell).addClass("is-ship");
			});
		});

		game.enemyGameboard.shots.forEach(function(shot) {
			var field = $("#enemy").find(".field-" + shot.x.toUpperCase() + shot.y);
			if(shot.isHit === true) {
				field.addClass('hit');
			} else {
				field.addClass('miss');
			}
		});

		game.myGameboard.shots.forEach(function(shot) {
			var field = $("#own").find(".field-" + shot.x.toUpperCase() + shot.y);
			if(shot.isHit === true) {
				field.addClass('hit');
			} else {
				field.addClass('miss');
			}
		});

		renderGameInfo();
	}

	function addOwnShot(shot) {
		var field = $("#enemy").find(".field-" + shot.field.x.toUpperCase() + shot.field.y);
		if([CONST.SHOTSTATES.BOOM, CONST.SHOTSTATES.WINNER].indexOf(shot.result) > -1) {
			field.addClass('hit');
		} else if(shot.result == CONST.SHOTSTATES.SPLASH) {
			field.addClass('miss');
		} else {
			console.log("Shot error!");
		}
	}

	function addEnemyShot(shot) {
		var field = $("#own").find(".field-" + shot.field.x.toUpperCase() + shot.field.y);
		if([CONST.SHOTSTATES.BOOM, CONST.SHOTSTATES.WINNER].indexOf(shot.result) > -1) {
			field.addClass('hit');
		} else if(shot.result == CONST.SHOTSTATES.SPLASH) {
			field.addClass('miss');
		} else {
			console.log("Shot error!");
		}
	}

	socket.addListener('shot', function(shot) {
		lastTurn = {
			player: game.enemyId == shot.user ? game.enemyName : userInfo.name,
			field: shot.field.x.toUpperCase() + shot.field.y,
			status: (function() {
				switch(shot.result) {
					case CONST.SHOTSTATES.BOOM:
					case CONST.SHOTSTATES.WINNER:
						return "hit!";
					case CONST.SHOTSTATES.FAIL:
						return "failed!";
					case CONST.SHOTSTATES.SPLASH:
						return "missed";
				}
			})()
		};

		if(game.enemyId == shot.user) {
			addEnemyShot(shot)
		} else {
			addOwnShot(shot);
		}

		if(shot.result == CONST.SHOTSTATES.WINNER) {
			game.youWon = (shot.user == window.userInfo._id);
			game.status = "done";
		}
	});

	socket.addListener('turn', function(turn) {
		game.yourTurn = turn.turn != game.enemyId;
		renderGameInfo();
	});

	$(document.body).on('click', '#enemy .field', function(item) {
		var field = $(item.currentTarget);
		window.router.postShot(gameId, field.attr('data-x').toLowerCase(), field.attr('data-y'));
	});

	this.destruct = function() {
		//
	};

	return this;
};