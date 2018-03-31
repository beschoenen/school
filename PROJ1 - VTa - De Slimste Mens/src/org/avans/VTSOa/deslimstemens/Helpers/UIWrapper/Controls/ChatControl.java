package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import com.jfoenix.controls.JFXButton;
import com.jfoenix.controls.JFXTextField;
import javafx.geometry.Pos;
import javafx.scene.input.KeyCode;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Priority;

public abstract class ChatControl {
	private HBox hBox = new HBox();

	private JFXTextField tfNewMessage = new JFXTextField();
	private JFXButton btSendNewMessage = new JFXButton();

	public ChatControl() {
		btSendNewMessage.setText("Verstuur");
		tfNewMessage.setPromptText("Type een bericht...");

		HBox.setHgrow(tfNewMessage, Priority.ALWAYS);
		btSendNewMessage.setAlignment(Pos.BASELINE_RIGHT);

		hBox.getChildren().addAll(tfNewMessage, btSendNewMessage);

		btSendNewMessage.setOnAction(event -> {
			onSend(tfNewMessage.getText());
			tfNewMessage.setText("");
		});
		tfNewMessage.setOnKeyReleased(event -> {
			if(event.getCode() == KeyCode.ENTER && !tfNewMessage.getText().equals("")) {
				onSend(tfNewMessage.getText());
				tfNewMessage.setText("");
			}
		});
	}

	public abstract void onSend(String text);

	public final HBox getView() {
		return this.hBox;
	}
}
