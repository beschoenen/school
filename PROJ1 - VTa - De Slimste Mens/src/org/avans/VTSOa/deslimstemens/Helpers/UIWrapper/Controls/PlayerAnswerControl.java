package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import com.jfoenix.controls.JFXButton;
import com.jfoenix.controls.JFXTextField;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.input.KeyCode;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;

import java.util.ArrayList;

/**
 * Created by Mike on 4-1-2016.
 */
public class PlayerAnswerControl extends StackPane {

	private ArrayList<AnswerListener> _answerListeners = new ArrayList<>();
	private JFXTextField tfAnwser;
	private Label explainText = new Label();

	public PlayerAnswerControl(){
		showAnswerControl();
	}

	private void showAnswerControl(){
		JFXButton btnPass = UI.Button("Pas");
		btnPass.setStyle("-fx-font-size: 15px;");
		btnPass.setTextFill(Color.WHITE);
		btnPass.setOnMouseClicked(event -> firePassEvent());

		tfAnwser = UI.TextField("Antwoord...");
		tfAnwser.setMinWidth(300);
		tfAnwser.setMinHeight(30);
		tfAnwser.setStyle("-fx-font-size: 15px;-fx-text-fill: white;-fx-prompt-text-fill: gray;");
		tfAnwser.setOnKeyReleased(event -> {
			if(event.getCode() == KeyCode.ENTER) fireAnswerEvent();
		});

		JFXButton btnSend = UI.Button(">");
		btnSend.setStyle("-fx-font-size: 15px;-fx-text-fill: white;");
		btnSend.setOnMouseClicked(event -> fireAnswerEvent());

		this.getChildren().clear();
		this.getChildren().addAll(btnPass, tfAnwser, btnSend);
		this.applyCss();
		this.setBackground(new Background(new BackgroundFill(Color.rgb(89, 8, 8).hsb(0, 0.91, 0.35), null, null)));
		StackPane.setAlignment(btnPass, Pos.CENTER_LEFT);
		StackPane.setAlignment(tfAnwser, Pos.CENTER);
		StackPane.setMargin(tfAnwser, new Insets(0, 40, 0, 40));
		StackPane.setAlignment(btnSend, Pos.CENTER_RIGHT);

		tfAnwser.requestFocus();
	}

	private void showAfterAnswerPage(){
		explainText.setStyle("-fx-font-size: 15px");
		explainText.setTextFill(Color.WHITE);

		this.getChildren().clear();
		this.getChildren().addAll(explainText);
		StackPane.setAlignment(explainText, Pos.CENTER);
	}

	public boolean setAnswerTurnStatus(TurnStatus status, boolean isEndRound){
		if(status == null){
			showAnswerControl();
			return false;
		}

		String text = getAnswerPageText(status, isEndRound);
		this.explainText.setText(text);

		showAfterAnswerPage();
		return true;
	}

	public boolean setAnswerMessage(String msg, boolean isEndRound){
		if(msg == null){
			showAnswerControl();
			return false;
		}

		if(isEndRound){
			msg += getEndRoundText();
		}
		this.explainText.setText(msg);

		showAfterAnswerPage();
		return true;
	}

	private String getAnswerPageText(TurnStatus status, boolean isEndRound){
		String text = "";

		switch (status){
			case Wrong:
				text = "Het gegeven antwoord is FOUT!";
				break;
			case Correct:
				text = "Het gegeven antwoord is CORRECT!";
				break;
			case Timeout:
				text = "Je tijd om te antwoorden is verlopen!";
				break;
			case Passed:
				text = "Je hebt de vraag gepast";
				break;
		}

		if(isEndRound){
			text += getEndRoundText();
		}
		return text;
	}

	private String getEndRoundText(){
		return ", dit is het einde van de ronde";
	}

	private void fireAnswerEvent() {
		String answer = purifyAnswer();

		if(answer != null){
			for(AnswerListener listener : _answerListeners){
				listener.answerEvent(answer);
			}
		}
		this.tfAnwser.setText("");
	}

	private void firePassEvent() {
		_answerListeners.forEach(AnswerListener::passEvent);
	}

	private String purifyAnswer() {
		String content = tfAnwser.getText().trim();
		if(content.length() >= 1){
			return content;
		}
		return null;
	}

	public void addAnswerListener(AnswerListener listener){
		_answerListeners.add(listener);
	}

	public void removeAnswerListener(AnswerListener listener){
		_answerListeners.remove(listener);
	}
}
