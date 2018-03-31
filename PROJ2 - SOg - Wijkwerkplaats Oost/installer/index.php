<!DOCTYPE>
<html>
	<head>
		<title>Wijk werkplaats Oost - Installer</title>
		<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet"
		      integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7"
		      crossorigin="anonymous">
	</head>
	<body>
		<div class="container">
			<br/>
			<br/>
			<form method="POST" action="process.php">
				<div class="panel panel-default">
					<div class="panel-heading">Website installeren</div>
					<div class="panel-body">
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h2>Wijk Werkplaats Oost</h2>
							</div>
						</div>
						<!-- //// -->
						<!-- User -->
						<!-- //// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h3>Beheerder</h3>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="_USER_EMAIL">Email</label>
									<input id="_USER_EMAIL" type="email" name="_USER_EMAIL" title="Beheerder email" class="form-control" required/>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="_USER_PASSWORD">Wachtwoord</label>
									<input id="_USER_PASSWORD" type="password" name="_USER_PASSWORD" title="Beheerder wachtwoord" class="form-control" required/>
								</div>
							</div>
						</div>
						<!-- //////// -->
						<!-- Database -->
						<!-- //////// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h3>Database</h3>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_CONNECTION">Connectie type</label>
									<select id="DB_CONNECTION" name="DB_CONNECTION" title="Database connectie" class="form-control" required>
										<option value="mysql">MySQL</option>
										<option value="sqlite">SQLite</option>
									</select>
								</div>
							</div>
						</div>
						<div class="row for-mysql">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_HOST">Hostnaam / IP adres</label>
									<input id="DB_HOST" type="text" name="DB_HOST" title="Database url" value="127.0.0.1" class="form-control mail-credentials" required>
								</div>
							</div>
						</div>
						<div class="row for-mysql">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_PORT">Poort</label>
									<input id="DB_PORT" type="text" name="DB_PORT" title="Database poort" value="3306" class="form-control">
								</div>
							</div>
						</div>
						<div class="row for-mysql">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_DATABASE">Database naam</label>
									<input id="DB_DATABASE" type="text" name="DB_DATABASE" title="Database naam" value="wijk_werkplaats_oost" class="form-control mail-credentials" required>
								</div>
							</div>
						</div>
						<div class="row for-mysql">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_USERNAME">Gebruikersnaam</label>
									<input id="DB_USERNAME" type="text" name="DB_USERNAME" title="Database gebruikersnaam" class="form-control mail-credentials" required>
								</div>
							</div>
						</div>
						<div class="row for-mysql">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="DB_PASSWORD">Wachtwoord</label>
									<input id="DB_PASSWORD" type="password" name="DB_PASSWORD" title="Database wachtwoord" class="form-control">
								</div>
							</div>
						</div>
						<!-- //////////////// -->
						<!-- Contactformulier -->
						<!-- //////////////// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h3>Contactformulier</h3>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="_FORM_ENABLE">Contactformulier inschakelen</label>
									<select id="_FORM_ENABLE" name="_FORM_ENABLE" title="Zet het contactformulier aan" class="form-control" required>
										<option value="1">Ja</option>
										<option value="0">Nee</option>
									</select>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="_FORM_EMAIL">Email Contactpersoon</label>
									<input id="_FORM_EMAIL" type="text" name="_FORM_EMAIL" title="Contactpersoon email adres" class="form-control" required/>
								</div>
							</div>
						</div>
						<!-- ///// -->
						<!-- Email -->
						<!-- ///// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h3>Email</h3>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="MAIL_HOST">Hostnaam / IP adres</label>
									<input id="MAIL_HOST" type="text" name="MAIL_HOST" title="Email hostnaam" class="form-control" required/>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="MAIL_PORT">Poort</label>
									<select id="MAIL_PORT" name="MAIL_PORT" title="Email poort" class="form-control" required>
										<option value="25">25</option>
										<option value="465">465</option>
										<option value="587" selected>587</option>
									</select>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="MAIL_USERNAME">Gebruikersnaam</label>
									<input id="MAIL_USERNAME" type="text" name="MAIL_USERNAME" title="Email gebruikersnaam" class="form-control" required/>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="MAIL_PASSWORD">Wachtwoord</label>
									<input id="MAIL_PASSWORD" type="password" name="MAIL_PASSWORD" title="Email wachtwoord" class="form-control" required/>
								</div>
							</div>
						</div>
						<!-- ///////// -->
						<!-- Recaptcha -->
						<!-- ///////// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<h3>Recaptcha</h3>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="RECAPTCHA_PUBLIC_KEY">Publieke sleutel</label>
									<input id="RECAPTCHA_PUBLIC_KEY" type="text" name="RECAPTCHA_PUBLIC_KEY" title="Recaptcha publieke sleutel" class="form-control" required/>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<label for="RECAPTCHA_PRIVATE_KEY">Privé sleutel</label>
									<input id="RECAPTCHA_PRIVATE_KEY" type="text" name="RECAPTCHA_PRIVATE_KEY" title="Recaptcha privé sleutel" class="form-control" required/>
								</div>
							</div>
						</div>
						<!-- ///// -->
						<!-- Other -->
						<!-- ///// -->
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<div class="form-group">
									<input type="submit" value="Opslaan" class="btn btn-primary" id="save"/>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-lg-6 col-lg-offset-3 col-md-8 col-md-offset-2 col-md-12">
								<span id="message"></span>
							</div>
						</div>
						<br/>
					</div>
				</div>
			</form>
			<script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>
			<script src="http://spin.js.org/spin.min.js"></script>
			<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"
			        integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS"
			        crossorigin="anonymous">
			</script>
			<script>
				$("#DB_CONNECTION").on('change', function() {
					$(".for-mysql").toggle($("#DB_CONNECTION :selected").val() == "mysql");
					$(".for-sqlite").toggle($("#DB_CONNECTION :selected").val() == "sqlite");
					$("#DB_CONNECTION :selected").val() == "mysql" ?
						$(".mail-credentials").attr('required', true) :
						$(".mail-credentials").removeAttr('required');
				});

				$("#_FORM_ENABLE").on('change', function() {
					$("#_FORM_ENABLE :selected").val() == "1" ?
						$("#_FORM_EMAIL").attr('required', true) :
						$("#_FORM_EMAIL").removeAttr('required');
				});

				$('form').submit(function(event) {
					event.target.checkValidity();

					event.preventDefault();

					var spinner = new Spinner().spin();
					$("#message").html(spinner.el);

					$.post($(this).attr('action'), $(this).find(':not(:hidden)').serialize(), function(res) {
						spinner.stop();

						$("#message").html(function() {
							if(res.error === true) {
								return "<div class='alert alert-danger'>" +
									"<b>Error:</b>&nbsp;" + res.message +
								"</div>"
							} else {
								return "<div class='alert alert-success'>" +
									"<b>Message:</b>&nbsp;" + res.message + "<br/>" +
									"<button onclick='location.reload();' class='btn btn-success'>Ga naar de website</button>" +
								"</div>"
							}
						}());
					}, "JSON");
				});
			</script>
		</div>
	</body>
</html>