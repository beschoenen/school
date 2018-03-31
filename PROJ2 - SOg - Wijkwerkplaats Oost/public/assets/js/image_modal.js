/**
 * @param {object} options
 *        |-- {string} apiUrl - the url for the api
 *        |-- {string} imgUrl - the url for the image with "img-id" as fake id
 *        |-- {function} done - the method called when the user is done with the modal
 *            |-- @param {number|object} id - returns the id or array of ids -- dependent on multiMode -- selected by the user
 *        |-- {element|null} button - the button that will trigger the modal
 *        |-- {boolean} [multiMode=false] - if the user would be able to select multiple images
 *        |-- {string} [selected] - the already selected image id(s), comma separated
 *        |-- {number} [limit] - the maximum number of images to be selected
 * @returns {ImageModal}
 * @constructor
 */
var ImageModal = function(options) {
	var main = this;

	// The IDs of all selected images
	var selected = 0;

	// Init
	options.multiMode = options.multiMode || false;

	if(options.button) {
		$(document.body).on('click', options.button, function() {
			main.show();
		});
	}

	if(options.multiMode) {
		// Set title
		$("#modal-title").html("Selecteer afbeeldingen");

		// Create close button
		createDoneButton();

		selected = [];
		if(typeof options.selected == 'string') {
			var split = options.selected.split(',');
			for(var i = 0; i < split.length; i++) {
				if(split[i] != '') {
					selected.push(parseInt(split[i]));
				}
			}
		}
	} else {
		selected = parseInt(options.selected);
	}

	// Show the modal
	main.show = function() {
		$('#' + modalId).on('show.bs.modal', function() {
			getImages(1);
		}).modal('show');
	};

	// Hide the modal
	main.hide = function() {
		$('#' + modalId).modal('hide');
	};

	function selectImage(node, id) {
		if(options.multiMode) {
			if(selected.indexOf(id) > -1) {
				node.classList.remove('selected');
				selected.splice(selected.indexOf(id), 1);
			} else {
				if(!options.limit || selected.length < options.limit) {
					node.classList.add('selected');
					selected.push(id);
				}
			}
		} else {
			if(selected > 0) {
				$(".modal-image-" + selected)[0].classList.remove('selected');
			}
			node.classList.add('selected');
			options.done(id);
			selected = id;
			main.hide();
		}
	}

	function finish() {
		options.done(selected);
		main.hide();
	}

	function getImages(page) {
		page = page || 1;
		$.ajax(options.apiUrl + '?page=' + page, {
			accepts: 'json',
			success: function(data) {
				createPaginationController(data);
				createImages(data.data);
			}
		});
	}

	// Helper methods to create the HTML

	function createDoneButton() {
		var button = document.createElement('button');
		button.type = 'button';
		button.innerHTML = 'Klaar';
		button.classList.add('btn', 'btn-primary', 'pull-left');
		button.onclick = function() {
			finish();
		};
		$(".modal-footer").prepend(button);
	}

	function createImages(images) {
		var container = document.getElementById("modal-image-container");
		container.innerHTML = "";

		var row = null;
		for(var image in images) {
			if(images.hasOwnProperty(image)) {
				if(image % 3 === 0) {
					if(row !== null) {
						container.appendChild(row);
					}
					row = document.createElement('div');
					row.classList.add('row');
				}
				row.appendChild(createImageFragment(images[image]));
			}
		}
		if(row == null) {
			container.innerHTML = "Geen afbeeldingen beschikbaar.";
		} else {
			container.appendChild(row);
		}
	}

	function createImageFragment(image) {
		var col = document.createElement('div');
		col.classList.add('col-lg-4', 'col-md-4', 'col-sm-4', 'col-xs-12');

		var fragment = document.createElement('div');
		fragment.classList.add('modal-image-fragment');

		var image_container = document.createElement('div');
		image_container.classList.add('modal-image-fragment-image-container');

		var image_tag = document.createElement('img');
		image_tag.src = options.imgUrl.replace(/img-id/, image.id);
		image_tag.alt = image.name;
		image_tag.classList.add('modal-image-' + image.id);
		image_tag.onclick = function() {
			selectImage(this, image.id);
		};

		if(
			(options.multiMode && selected.indexOf(image.id) > -1) ||
			(!options.multiMode && selected == image.id)
		) {
			image_tag.classList.add('selected');
		}

		var name = document.createElement('span');
		name.innerHTML = image.name;

		image_container.appendChild(image_tag);

		fragment.appendChild(image_container);
		fragment.appendChild(name);

		col.appendChild(fragment);

		return col;
	}

	function createPaginationController(paginate) {
		if(paginate.last_page < 2) { return; }

		var control = document.createElement('ul');
		control.classList.add('pagination');

		control.appendChild(createPreviousButton(paginate.current_page));
		for(var i = 1; i <= paginate.last_page; i++) {
			var page = i;

			var item = document.createElement('li');
			var text;

			if(page == paginate.current_page) {
				item.classList.add('active');
				text = document.createElement('span');
			} else {
				text = document.createElement('a');
				text.href = "javascript:void(0)";
				text.onclick = function() {
					getImages(this.innerHTML);
				}
			}
			text.innerHTML = page;

			item.appendChild(text);
			control.appendChild(item);
		}
		control.appendChild(createNextButton(paginate.current_page, paginate.last_page));

		var container = document.getElementById("modal-pagination-control");
		container.innerHTML = "";
		container.appendChild(control);
	}

	function createPreviousButton(page) {
		var previous = document.createElement('li');
		var text;

		if(page == 1) {
			text = document.createElement('span');
		} else {
			text = document.createElement('a');
			text.href = "javascript:void(0)";
			text.onclick = function() {
				getImages(page -=1);
			}
		}
		text.innerHTML = "«";

		previous.appendChild(text);
		return previous;
	}

	function createNextButton(page, last_page) {
		var next = document.createElement('li');
		var text;

		if(page == last_page) {
			text = document.createElement('span');
		} else {
			text = document.createElement('a');
			text.href = "javascript:void(0)";
			text.onclick = function() {
				getImages(page +=1);
			}
		}
		text.innerHTML = "»";

		next.appendChild(text);
		return next;
	}

	return this;
};