tinymce.init({
	mode : "specific_textareas",
	editor_selector : "tiny_text",
	lang: 'nl',
	browser_spellcheck: true,
	menubar: false,
	statusbar: false,
	plugins: ['autoresize'],
	toolbar: 'undo redo | styleselect',
	autoresize_bottom_margin: 0,
	autoresize_max_height: 400,
	save_onsavecallback: function() {
		var newText = this.getContent({ format : 'html' });
	}
});