package org.avans.VTSOa.deslimstemens.Views;

import com.jfoenix.controls.JFXButton;
import com.jfoenix.controls.JFXPasswordField;
import com.jfoenix.controls.JFXTextField;
import javafx.geometry.Insets;
import javafx.scene.Parent;
import javafx.scene.control.Label;
import javafx.scene.input.InputMethodEvent;
import javafx.scene.layout.StackPane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.FontWeight;
import org.avans.VTSOa.deslimstemens.Controllers.AccountController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;

public class AccountView extends SceneView{

	StackPane parent = new StackPane();
	public Label infoText = new Label();
	public JFXPasswordField passNewField;
	public JFXPasswordField passOldField;
	AccountController controller;

	public AccountView(AccountController accountController) {
		this.controller = accountController;
		this.setMenuBarTitle("Account van "+RunTimeSettings.user.getName());
	}

	@Override
	public Parent getRoot() {
		return this.parent;
	}

	@Override
	public void initialize() {
		VBox container = new VBox();
		container.setMinSize(200, 200);
		container.setPrefSize(200, 200);

		this.infoText.setFont(Font.font("Arial", FontWeight.BOLD, 15));
		this.infoText.setTextFill(Color.RED);
		VBox.setMargin(this.infoText, new Insets(0, 0, 15, 0));

		Label changePass = new Label("Wachtwoord aanpassen");
		changePass.setFont(Font.font("Arial", FontWeight.BOLD, 18));

		Label changePassNew = new Label("Nieuw wachtwoord:");
		changePassNew.setFont(Font.font("Arial", FontWeight.NORMAL, 15));

		this.passNewField = UI.PasswordField("Wachtwoord");
		passNewField.setOnInputMethodTextChanged(this::onTextChanged);

		Label changePassOld = new Label("Oud wachtwoord:");
		changePassOld.setFont(Font.font("Arial", FontWeight.NORMAL, 15));

		this.passOldField = UI.PasswordField("Wachtwoord");
		passOldField.setOnInputMethodTextChanged(this::onTextChanged);

		JFXButton updateButton = UI.Button("Aanpassen");
		updateButton.setOnAction(event -> {
			this.controller.checkUpdatePassword(passNewField.getText(), passOldField.getText());
		});

		container.getChildren().addAll(this.infoText, changePassNew, passNewField, changePassOld, passOldField, updateButton);
		StackPane.setMargin(container, new Insets(15));
		this.parent.getChildren().addAll(container);
		this.parent.setMinSize(200, 200);
		this.parent.setPrefSize(200, 200);
	}

	private void onTextChanged(InputMethodEvent event){
		this.infoText.setText("");
	}

	@Override
	public void goBack() {
		super.goBack();
	}

	@Override
	public double getHeight() {
		return 350;
	}

	@Override
	public double getWidth() {
		return super.getWidth();
	}

	public void clearText(boolean clearAllFields) {
		if(clearAllFields) {
			this.passNewField.clear();
			this.passOldField.clear();
		}else{
			this.passOldField.clear();
		}
	}
}
