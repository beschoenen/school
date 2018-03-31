package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import javafx.scene.control.Label;
import javafx.scene.effect.BoxBlur;
import javafx.scene.layout.HBox;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Circle;

/**
 * Created by Mike on 4-1-2016.
 */
public class OpenAnswerControl extends HBox {

	private boolean _answered = false;
	private String _answerText;
	private String _pointsText;

	public OpenAnswerControl(String answerText, String points){
		_answerText = answerText;
		_pointsText = points;
		redraw();
		this.setSpacing(10);
	}

	private StackPane createCircle()
	{
		StackPane main = new StackPane();

		Circle circle = new Circle(10);
		circle.setFill(Color.rgb(176, 9, 9).hsb(0, 0.95, 0.69));
		circle.setStroke(Color.WHITE);
		main.getChildren().add(circle);

		if(isAnswered()){
			Label score = new Label(_pointsText);
			score.setTextFill(Color.WHITE);
			main.getChildren().add(score);
		}
		return main;
	}

	private Label createLabel(){
		Label anwserText = new Label(_answerText);
		if(!isAnswered()){
			BoxBlur blur = new BoxBlur();
			blur.setHeight(5);
			blur.setWidth(5);
			blur.setIterations(10);
			anwserText.setEffect(blur);
			anwserText.setTextFill(Color.BLACK);
		}
		else{
			anwserText.setTextFill(Color.WHITE);
		}
		return anwserText;
	}

	public boolean isAnswered(){
		return _answered;
	}

	public void answer(){
		_answered = !_answered;
		redraw();
	}

	public String getAnswer() {
		return _answerText;
	}

	private void redraw() {
		StackPane score = createCircle();
		Label text = createLabel();
		this.getChildren().clear();
		this.getChildren().addAll(score, text);
	}
}
