package org.avans.VTSOa.deslimstemens.Controllers;

import com.mysql.jdbc.exceptions.jdbc4.MySQLIntegrityConstraintViolationException;
import javafx.application.Platform;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Enums.PlayerRole;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.MissingException;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Navigator;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Account;
import org.avans.VTSOa.deslimstemens.Repositories.AccountRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;
import org.avans.VTSOa.deslimstemens.Views.LoginView;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class LoginController extends SceneController {

	LoginView view;

	public boolean isLoginMode = false;

	public LoginController(){
		this.view = new LoginView(this);
		this.view.setMenuBar(false);
		switchMode();
	}

	public void switchMode() {
		paintUsernameField(false);
		paintPasswordField(false);
		this.isLoginMode = !this.isLoginMode;
		view.submitButton.setDisable(false);
		view.usernameField.selectAll();
		view.passwordField.setText("");
		if(this.isLoginMode) { // Login
			view.submitButton.setText("Login");
			view.modeSwitchButton.setText("Naar registratie");
			view.alignBox.getChildren().remove(view.roleCheckboxBox);
		} else { // Register
			view.submitButton.setText("Registreer");
			view.submitButton.setDisable(true);
			view.modeSwitchButton.setText("Naar login");
			view.usernameField.setText("");
			view.passwordField.setText("");
			view.alignBox.getChildren().remove(view.roleSelectorBox);
			view.alignBox.getChildren().add(4, view.roleCheckboxBox);
		}
		view.usernameField.requestFocus();
		view.messageLabel.setText("");
		view.playerCheckBox.setSelected(false);
		view.observerCheckBox.setSelected(false);
	}

	public void submit() {
		paintPasswordField(false);
		paintPasswordField(false);
		if(this.isLoginMode) {
			tryLogin();
		} else {
			tryRegister();
		}
	}

	private void tryLogin() {
		String username = view.usernameField.getText();
		String password = view.passwordField.getText();

		new Thread(() -> {
			Account user = null;
			try {
				user = AccountRepository.find(username);
			} catch (SQLException e) {
				e.printStackTrace();
			}
			handleLogin(user, username, password);
		}).start();
	}

	private void handleLogin(Account user, String username, String password) {
		if(user != null && user.getName().equals(username)) {
			if(user.getPassword().equals(password)) {
				if(user.getRoles().size() == 2 && view.roleSelectorGroup.getSelectedToggle() == null) {
					Platform.runLater(this::showRoleSelector);
				} else {
					PlayerRole playerRole = PlayerRole.Player;
					if(user.getRoles().size() == 1) {
						playerRole = user.getRoles().get(0);
					} else if(view.roleSelectorGroup.getSelectedToggle() != null) {
						if(view.roleSelectorGroup.getSelectedToggle().equals(view.observerRadioButton)) {
							playerRole = PlayerRole.Observer;
						}
					}
					login(user, playerRole);
				}
			} else {
				Platform.runLater(this::wrongPassword);
			}
		} else {
			Platform.runLater(this::userNotFound);
		}
	}

	private void showRoleSelector() {
		view.messageLabel.setText("Selecteer aub een rol");
		view.submitButton.setDisable(true);
		view.alignBox.getChildren().add(4, view.roleSelectorBox);
	}

	private void userNotFound() {
		paintUsernameField(true);
		view.usernameField.requestFocus();
		view.messageLabel.setText("Gebruikersnaam niet gevonden!");
	}

	private void wrongPassword() {
		paintPasswordField(true);
		view.passwordField.requestFocus();
		view.messageLabel.setText("Verkeerd wachtwoord!");
	}

	private void login(Account account, PlayerRole playerRole) {
		RunTimeSettings.user = account;
		RunTimeSettings.role = playerRole;

		Platform.runLater(() -> {
			if(playerRole == PlayerRole.Observer) {
				try {
					ObserverController observerController = new ObserverController();
					RunTimeSettings.mainStager.addSceneController(observerController);
					Navigator.show("MainStage", observerController.getName());
				} catch (MissingException e) {
					e.printStackTrace();
				}
			} else {
				StartController startController = new StartController();
				RunTimeSettings.mainStager.addSceneController(startController);
				try {
					Navigator.show("MainStage", startController.getName());
				} catch (MissingException e) {
					e.printStackTrace();
				}
			}
		});
	}

	private void tryRegister() {
		List<PlayerRole> roles = new ArrayList<>();

		if(view.playerCheckBox.isSelected()) roles.add(PlayerRole.Player);
		if(view.observerCheckBox.isSelected()) roles.add(PlayerRole.Observer);

		String username = view.usernameField.getText();
		String password = view.passwordField.getText();

		if(roles.size() == 0) return;

		if(username.equals("")) {
			paintUsernameField(true);
			view.messageLabel.setText("Geef aub een gebruikersnaam!");
			return;
		} else if(username.length() < 3) {
			paintUsernameField(true);
			view.messageLabel.setText("Gebruikersnaam te kort.");
			return;
//		} else if(username.matches("[^0-9a-zA-Z ]+")) {
//			paintUsernameField(true);
//			view.usernameField.setText(username.replaceAll("[^0-9a-zA-Z ]+", ""));
//			view.usernameField.requestFocus();
//			view.messageLabel.setText("Gebruikersnaam bevatte illegale characters");
//			return;
		} else {
			paintUsernameField(false);
		}

		if(password.equals("")) {
			paintPasswordField(true);
			view.messageLabel.setText("Geef aub een wachtwoord!");
			return;
		} else if(password.length() < 3) {
			paintPasswordField(true);
			view.messageLabel.setText("Wachtwoord te kort.");
			return;
//		} else if(password.matches("")) { // TODO
//			paintPasswordField(true);
//			view.usernameField.setText("");
//			view.usernameField.requestFocus();
//			view.messageLabel.setText("Wachtwoord bevatte illegale characters");
//			return;
		} else {
			paintPasswordField(false);
		}

		register(new Account(username, password, roles));
	}

	private void register(Account account) {
		new Thread(() -> {
			try {
				AccountRepository.create(account);
				Platform.runLater(() -> afterRegister(account.getName()));
			} catch (SQLException e) {
				if (e.getClass() == MySQLIntegrityConstraintViolationException.class) {
					Platform.runLater(this::userAlreadyExists);
				} else {
					e.printStackTrace();
				}
			}
		}).start();
	}

	private void afterRegister(String name) {
		switchMode();
		view.messageLabel.setText(String.format("Je bent geregistreerd als %s", name));
		view.usernameField.setText(name);
		view.passwordField.requestFocus();
		view.submitButton.setDisable(false);
	}

	private void userAlreadyExists() {
		paintUsernameField(true);
		view.usernameField.requestFocus();
		view.messageLabel.setText("Deze naam is al in gebruik!");
	}

	private void paintUsernameField(boolean red) {
		if(red) {
			view.usernameField.setUnFocusColor(Color.RED);
			view.usernameField.setFocusColor(Color.DARKRED);
		} else {
			view.usernameField.setUnFocusColor(Color.rgb(77, 77, 77));
			view.usernameField.setFocusColor(Color.valueOf("#4059A9"));
		}
	}

	private void paintPasswordField(boolean red) {
		if(red) {
			view.passwordField.setUnFocusColor(Color.RED);
			view.passwordField.setFocusColor(Color.DARKRED);
		} else {
			view.passwordField.setUnFocusColor(Color.rgb(77, 77, 77));
			view.passwordField.setFocusColor(Color.valueOf("#4059A9"));
		}
	}

	@Override
	public void onLeave() {
		this.view.usernameField.setText("");
		this.view.passwordField.setText("");
	}

	@Override
	public void onEnter() {
		this.view.usernameField.requestFocus();
	}

	@Override
	public SceneView getView() {
		return view;
	}

	@Override
	public String getName() {
		return "Login";
	}
}
