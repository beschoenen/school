$('body .font-small').click(function() {
	$(document.body).removeClass('font-medium font-large').addClass('font-small');
	document.cookie = "fontsize=small";
});

$('body .font-medium').click(function() {
	$(document.body).removeClass('font-small font-large').addClass('font-medium');
	document.cookie = "fontsize=medium";
});

$('body .font-large').click(function() {
	$(document.body).removeClass('font-small font-medium').addClass('font-large');
	document.cookie = "fontsize=large";
});
