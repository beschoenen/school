package org.avans.VTSOa.deslimstemens.Views.Game;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Parent;
import javafx.scene.control.Label;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Controllers.Game.FramedController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.OpenAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerScoreControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Answer;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Mike on 3-1-2016.
 */
public class FramedView extends SceneView {
	private BorderPane parent = new BorderPane();

	private Label questionText = new Label();

	private VBox answerRow1 = new VBox();
	private VBox answerRow2 = new VBox();

	private PlayerScoreControl playerScoreControl = new PlayerScoreControl();
	public PlayerAnswerControl playerAnswerControl = new PlayerAnswerControl();
	private List<OpenAnswerControl> listAnswerControls = new ArrayList<>();

	private FramedController controller;

	public FramedView(FramedController controller) {
		this.setGoBackButton(false);
		this.controller = controller;
	}

	@Override
	public void goBack() {
		this.controller.returnToLobby();
	}

	@Override
	public void initialize() {
		this.playerAnswerControl.addAnswerListener(controller);

		this.questionText.setTextFill(Color.WHITE);

		VBox question = new VBox(this.questionText);
		question.setAlignment(Pos.CENTER);
		question.setSpacing(5);

		this.answerRow1.setSpacing(5);
		this.answerRow2.setSpacing(5);

		HBox answers = new HBox(answerRow1, answerRow2);
		answers.setAlignment(Pos.CENTER);
		answers.setSpacing(15);

		VBox center = new VBox(question, answers);
		center.setSpacing(10);
		center.setAlignment(Pos.CENTER);
		center.setPadding(new Insets(10));
		center.setMinHeight(320);

		this.parent.setTop(this.playerScoreControl);
		this.parent.setCenter(center);
		this.parent.setBottom(this.playerAnswerControl);
		this.parent.setBackground(new Background(new BackgroundFill(Color.RED, null, null)));
		this.parent.applyCss();
	}

	@Override
	public double getWidth() {
		return 545;
	}

	@Override
	public double getHeight() {
		return 580;
	}

	@Override
	public Parent getRoot() {
		return this.parent;
	}

	public void setRanking(GameRanking gameRanking) {
		this.playerScoreControl.setPlayerInfo(gameRanking);
	}

	public void setQuestion(Question question) {
		this.listAnswerControls.clear();
		this.answerRow1.getChildren().clear();
		this.answerRow2.getChildren().clear();

		this.questionText.setText(question.getQuestion());

		List<Answer> answers = question.getAnswers();
		int totalSize = answers.size();
		int firstRowSize = totalSize / 2;

		for (int i = 0; i < firstRowSize; i++){
			OpenAnswerControl answer = new OpenAnswerControl(answers.get(i).getAnswer(), "20");
			this.listAnswerControls.add(answer);
			this.answerRow1.getChildren().add(answer);
		}

		for (int i = firstRowSize; i < totalSize; i++){
			OpenAnswerControl answer = new OpenAnswerControl(answers.get(i).getAnswer(), "20");
			this.listAnswerControls.add(answer);
			this.answerRow2.getChildren().add(answer);
		}
	}

	public void revealAnswer(String answer) {
		this.listAnswerControls.forEach(openAnswerControl -> {
			if(openAnswerControl.getAnswer().equals(answer)) {
				openAnswerControl.answer();
			}
		});
	}
}
