var GameList = function(gotoGame) {

	var template = null;
	var games = null;

	var buttonListener;
	var gameButtonListener;

	this.show = function() {
		$.get('app/templates/gameList.hbs', function(result) {
			template = result;
			render();
		});

		socket.addListener('update', function(update) {
			var span = $(".game[data-game-id=" + update.gameId + "] span.status")[0];

			if(span) {
				span.innerHTML = update.status;
			}

			var gameStatus = null;
			var game = null;
			games.games.forEach(function(item) {
				if(item._id == update.gameId) {
					game = item;
					return false;
				}
			});

			if(game !== null) {
				gameStatus = game.status;
				game.status = update.status;
				if(gameStatus === CONST.STATES.QUEUE && update.status !== CONST.STATES.QUEUE) {
					window.router.getGame(game._id, function(result) {
						game = result;
						$(".game-box").find(".game[data-game-id=" + game._id + "] label:first")
							.first().html("Against: " + game.enemyName);
					});
				}
			}

		});

		function render() {
			window.router.getGames(function(result) {
				games = { games: (result.key !== "null" ? result : []) };
				var render = Handlebars.compile(template);
				$('#board').html(render(games));
			});
		}

		buttonListener = $(document.body).on('click', '.start-box.center button', function(item) {
			if(item.currentTarget.id == "new-game") {
				window.router.newGame(render);
			} else if(item.currentTarget.id == "new-ai-game") {
				window.router.newAIGame(render);
			} else if(item.currentTarget.id == "delete-games") {
				window.router.deleteGames(render);
			}
		});

		gameButtonListener = $(document.body).on('click', '.game-box .game', function(item) {
			var id = $(item.currentTarget).attr('data-game-id');
			games.games.forEach(function(game) {
				if(game._id == id) {
					gotoGame(game);
				}
			});
		});
	};

	this.destruct = function() {
		buttonListener.unbind();
		gameButtonListener.unbind();
	};

	return this;
};