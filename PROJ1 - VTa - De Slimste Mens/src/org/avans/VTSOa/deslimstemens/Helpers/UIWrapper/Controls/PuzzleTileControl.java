package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.FontWeight;
import javafx.util.Pair;
import org.avans.VTSOa.deslimstemens.Helpers.MeasureText;
import org.avans.VTSOa.deslimstemens.Models.Database.Answer;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 * Created by Mike on 4-1-2016.
 */
public class PuzzleTileControl extends GridPane {

	private List<Question> questions;
	private List<Label> labels = new ArrayList<>();
	private List<Pair<Color, String>> allAnswers = new ArrayList<>();
	private Color[] colors = new Color[]{Color.LIMEGREEN, Color.AQUAMARINE, Color.DARKCYAN};
	private int colorIndex = 0;
	private double maxWidth = 0;
	private Font font = Font.font("Arial", FontWeight.BOLD, 12);

	public PuzzleTileControl() {}

	public void create(List<Question> questions){
		this.questions = new ArrayList<>();
		this.questions = questions;
		prepare();
		createLabels();
		createGrid();
	}

	private void prepare(){
		allAnswers.clear();
		this.colorIndex = 0;
		this.questions.forEach(question -> {
			List<Answer> answers = question.getAnswers();
			answers.forEach(answer -> {
				allAnswers.add(new Pair<Color, String>(this.colors[this.colorIndex], answer.getAnswer()));
			});
			this.colorIndex++;
		});
	}

	private void createGrid(){
		this.getChildren().clear();
		int index = 0;
		for (int y = 0; y < 3; y ++) {
			for (int x = 0; x < 4; x++) {
				Label label = this.labels.get(index);
				label.setMinWidth(this.maxWidth);
				this.add(label, x, y);
				index++;
			}
		}
		this.setVgap(5);
		this.setHgap(5);
	}

	private void createLabels(){
		this.maxWidth = 0;
		this.labels.clear();
		for (int i = 0; i < 12; i++){
			Label label = createLabel(getRandomAnswer());
			MeasureText measure = new MeasureText(label.getText(), this.font);
			double width = Math.floor(measure.getTextWidth() + 34);
			if(width >= this.maxWidth){
				this.maxWidth = width;
			}
			this.labels.add(label);
		}
	}

	private Label createLabel(Pair<Color, String> combo){
		Label label = new Label(combo.getValue());
		label.setPadding(new Insets(15));
		label.setBackground(new Background(new BackgroundFill(Color.rgb(204, 0, 0).hsb(0, 1, 0.8), null, null)));
		label.setTextFill(combo.getKey());
		label.setFont(this.font);
		label.setAlignment(Pos.CENTER);
		label.setStyle("-fx-border-color: black");
		label.applyCss();
		return label;
	}

	private Pair<Color, String> getRandomAnswer() {
		Random r = new Random();
		int index = r.nextInt(allAnswers.size());
		Pair<Color, String> answer = allAnswers.get(index);
		allAnswers.remove(index);

		return answer;
	}
}
