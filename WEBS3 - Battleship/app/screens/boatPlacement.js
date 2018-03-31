var BoatPlacement = function(game, done) {

	var ships = null;

	var activeShip = null;
	var currentField = null;
	var currentFields = null;

	$.get('app/templates/grid.hbs', function(result) {
		var render = Handlebars.compile(result);
		$('#board').html(render({
			rows: CONST.FIELDS.ROWS,
			cols: CONST.FIELDS.COLUMNS,
			id: "sea"
		}));
	});

	$.get('app/templates/boatList.hbs', function(result) {
		var render = Handlebars.compile(result);
		window.router.getShips(function(result) {
			ships = result;
			$('#board').prepend(render({
				ships: result
			}));
		})
	});

	var verticalListener = $(document.body).on('click', '#vertical', function(item) {
		$(item.currentTarget).toggleClass("active");
	});

	var shipClickListener = $(document.body).on('click', '.ship .field', function(item) {
		var ship = $(item.currentTarget).closest('.ship');
		if(!ship.hasClass('set')) {
			$('.ship').removeClass('selected');

			activeShip = ship;
			activeShip.addClass('selected');
		}
	});

	var fieldLeaveListener = $(document.body).on('mouseover', '#sea .field', function(item) {
		var currentTarget = $(item.currentTarget);
		if(currentTarget !== currentField) {
			currentField = currentTarget;

			if(activeShip !== null) {
				$("#sea").find(".field").removeClass('selected');
				currentFields = getShipFields(
					currentField.attr('data-field'),
					activeShip.attr('data-length'),
					$("#vertical").hasClass('active')
				);
				currentFields.forEach(function(field) {
					$(".field-" + field).addClass('selected');
				});
			}
		}
	});

	var fieldClickListener = $(document.body).on('click', '#sea .field', function(target) {
		var field = $(target.currentTarget).attr('data-field');

		if(activeShip !== null) {

			currentFields.forEach(function(item) {
				var field = $(".field-" + item);
				if(field.hasClass('set')) {
					throw "Has been set!";
				}
			});

			currentFields.forEach(function(item) {
				var field = $($(".field-" + item)[0]);
				field.addClass('set');
				field.attr('data-ship', activeShip.attr('data-name'));
			});

			ships.forEach(function(item) {
				if(item.name == activeShip.attr('data-name')) {
					item.isVertical = $("#vertical").hasClass('active');
					item.startCell = { x: field[0].toLocaleLowerCase(), y: field.substr(1) };
				}
			});
			$(".ship[data-name='" + activeShip.attr('data-name') + "']").addClass('set');
			activeShip = null;

			if($(".ship").length === $(".ship.set").length) {
				$("#done").removeAttr('disabled');
			}
		}
	});

	var leaveListener = $(document.body).on('mouseleave', '#sea', function() {
		$("#sea").find(".field").removeClass('selected');
	});

	var doneListener = $(document.body).on('click', '#done', function() {
		window.router.postGameboard(game._id, { ships: ships }, function() {
			window.gameListScreen.show();
		});
	});

	var cancelButtonListener = $(document.body).on('click', '#cancel', function() {
		window.gameListScreen.show();
	});

	this.destruct = function() {
		verticalListener.unbind();
		shipClickListener.unbind();
		fieldLeaveListener.unbind();
		fieldClickListener.unbind();
		leaveListener.unbind();
		doneListener.unbind();
		cancelButtonListener.unbind();
	}
};