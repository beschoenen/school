package org.avans.VTSOa.deslimstemens.Views;

import com.jfoenix.controls.*;
import javafx.beans.value.ObservableValue;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Parent;
import javafx.scene.control.ButtonBar;
import javafx.scene.control.Label;
import javafx.scene.control.ToggleGroup;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.KeyCode;
import javafx.scene.layout.StackPane;
import javafx.scene.layout.VBox;
import org.avans.VTSOa.deslimstemens.Controllers.LoginController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;

public class LoginView extends SceneView {

	public StackPane stackPane = new StackPane();

	public VBox alignBox = new VBox();
	public VBox roleSelectorBox = new VBox();
	public VBox roleCheckboxBox = new VBox();
	public ButtonBar buttonBar = new ButtonBar();

	public Label messageLabel = new Label();

	public Label roleSelectorLabel = new Label("Je hebt meerdere rollen.\nKies een van de rollen en druk op Login.");
	public JFXRadioButton playerRadioButton = UI.RadioButton("Speler");
	public JFXRadioButton observerRadioButton = UI.RadioButton("Observator");
	public ToggleGroup roleSelectorGroup = UI.RadioButtonGroup(playerRadioButton, observerRadioButton);

	public JFXCheckBox playerCheckBox = UI.CheckBox("Speler");
	public JFXCheckBox observerCheckBox = UI.CheckBox("Observator");
	Integer countSelectedRoles = 0;

	public JFXTextField usernameField = UI.TextField("Gebruikersnaam");
	public JFXPasswordField passwordField = UI.PasswordField("Wachtwoord");

	public JFXButton submitButton = UI.Button("");
	public JFXButton modeSwitchButton = UI.Button("");

	ImageView imageView = new ImageView(new Image(this.getClass().getResource("/org/avans/VTSOa/deslimstemens/Assets/Images/Logo_slimste_mens.png").toExternalForm()));

	LoginController controller;

	public LoginView(LoginController controller){
		this.controller = controller;
	}

	@Override
	public void initialize() {
		// Events
		submitButton.setOnAction(event -> controller.submit());
		modeSwitchButton.setOnAction(event -> controller.switchMode());

		usernameField.setOnKeyReleased(event -> {
			if(event.getCode() == KeyCode.ENTER) {
				passwordField.requestFocus();
			}
		});

		passwordField.setOnKeyReleased(event -> {
			if(event.getCode() == KeyCode.ENTER && !submitButton.isDisable()) {
				controller.submit();
			}
		});

		roleSelectorGroup.selectedToggleProperty().addListener((observable, oldValue, newValue) -> {
			if(newValue != null) {
				submitButton.setDisable(false);
				this.messageLabel.setText("");
			}
		});

		playerCheckBox.selectedProperty().addListener((observable, oldValue, newValue) -> countRoles(observable));
		observerCheckBox.selectedProperty().addListener((observable, oldValue, newValue) -> countRoles(observable));

		// Alignment stuff
		roleSelectorBox.setAlignment(Pos.TOP_LEFT);
		roleCheckboxBox.setAlignment(Pos.TOP_LEFT);

		alignBox.setAlignment(Pos.TOP_CENTER);
		alignBox.setMaxWidth(300);

		ButtonBar.setButtonData(submitButton, ButtonBar.ButtonData.LEFT);
		ButtonBar.setButtonData(modeSwitchButton, ButtonBar.ButtonData.RIGHT);
		buttonBar.setPadding(new Insets(4, 0, 0, 0));
		buttonBar.getButtons().addAll(submitButton, modeSwitchButton);

		roleCheckboxBox.setMargin(playerCheckBox, new Insets(7, 0, 7, 0));
		roleCheckboxBox.setMargin(observerCheckBox, new Insets(7, 0, 7, 0));

		VBox.setMargin(roleSelectorLabel, new Insets(5, 0, 0, 10));

		// Fill stuff
		roleSelectorBox.getChildren().addAll(roleSelectorLabel, playerRadioButton, observerRadioButton);
		roleCheckboxBox.getChildren().addAll(playerCheckBox, observerCheckBox);

		alignBox.getChildren().addAll(imageView, messageLabel, usernameField, passwordField, buttonBar);

		stackPane.setAlignment(Pos.CENTER);
		stackPane.getChildren().add(alignBox);
	}

	private void countRoles(ObservableValue<? extends Boolean> observable) {
		if(observable.getValue()) {
			countSelectedRoles += 1;
		} else {
			countSelectedRoles -= 1;
		}
		this.submitButton.setDisable(countSelectedRoles <= 0);
	}

	@Override
	public Parent getRoot() {
		return this.stackPane;
	}

	@Override
	public double getWidth(){
		return 400;
	}

	@Override
	public double getHeight(){
		return 450;
	}
}
