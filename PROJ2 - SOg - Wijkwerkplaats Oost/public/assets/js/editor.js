$(document).ready(function() {
	// Add Onclick listeners to all editable text fields
	$(".edit-container").each(function(index, container) {
		var text = $(container).find('[data-editable]')[0];
		text.onclick = function() {
			createContainer(container, text);
		};
		text.title = "Klik om te bewerken";
	});

	// Change all local a tags to include "?edit=true"
	$("a:not(.not-edit)[href^='" + window.location.origin + "'").each(function(index, item) {
		item.href += (/\?/.test(item.href) ? "&" : "?") + "edit=true";
	});
});

function createContainer(container, text) {
	var random = Math.floor(Math.random() * 100000);

	var editor = document.createElement('textarea');
	editor.id = "editor-box-" + random;
	editor.innerHTML = text.innerHTML;

	$(container).append(editor);

	tinymce.init({
		selector: "#" + editor.id,
		lang: 'nl',
		browser_spellcheck: true,
		menubar: false,
		statusbar: false,
		plugins: ['save', 'autoresize'],
		toolbar: 'undo redo | styleselect | save exit',
		autoresize_bottom_margin: 0,
		autoresize_max_height: 400,
		setup: function(mce) {
			mce.addButton('exit', {
				type: 'button',
				text: 'Afsluiten',
				icon: false,
				onclick: closeEditor
			});
		},
		save_onsavecallback: function() {
			var newText = this.getContent({ format : 'html' });
			saveText(newText, $(text).attr('data-section'), $(text).attr('data-save'), function(status) {
				if(status !== 200) {
					alert("Er ging iets fout, probeer het opnieuw");
				} else {
					$(text).html(newText);
					closeEditor();
				}

			}.bind(this));
		}
	});
	$(text).hide();

	function saveText(text, section, url, callback) {
		$.ajax({
			url: url,
			type: "POST",
			dataType: "json",
			data: { text: text, section: section  },
			headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') }
		}).complete(function(object) {
			callback(object.status);
		});
	}

	function closeEditor() {
		$("#" + editor.id).remove();
		$(tinymce.activeEditor.editorContainer).remove();
		$(text).show();
	}
}