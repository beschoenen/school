package org.avans.VTSOa.deslimstemens.Views.Game;

import javafx.geometry.Insets;
import javafx.scene.Parent;
import javafx.scene.control.Label;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Controllers.Game.R369Controller;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerScoreControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

public class R369View extends SceneView {
	private BorderPane parent = new BorderPane();

	private Label questionText = new Label();
	private Label questionNumber = new Label();

	public PlayerAnswerControl playerAnswerControl = new PlayerAnswerControl();
	public PlayerScoreControl playerScoreControl = new PlayerScoreControl(25);

	private R369Controller controller;

	public R369View(R369Controller controller) {
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

		this.questionNumber.setTextFill(Color.YELLOW);
		this.questionNumber.setMinSize(24, 24);
		this.questionNumber.setStyle("-fx-font-size: 20px");

		this.questionText.setTextFill(Color.WHITE);
		this.questionText.setMinSize(250, 28);
		this.questionText.setWrapText(true);
		this.questionText.setStyle("-fx-font-size: 18px");

		VBox questionBox = new VBox();
		questionBox.setMinHeight(100);
		questionBox.getChildren().addAll(questionNumber, this.questionText);
		BorderPane.setMargin(questionBox, new Insets(10));

		this.parent.setTop(this.playerScoreControl);
		this.parent.setCenter(questionBox);
		this.parent.setBottom(playerAnswerControl);
		this.parent.setBackground(new Background(new BackgroundFill(Color.RED, null, null)));
		this.parent.applyCss();
	}

	@Override
	public double getWidth() {
		return 545;
	}

	@Override
	public Parent getRoot() {
		return this.parent;
	}

	public void setRanking(GameRanking gameRanking) {
		this.playerScoreControl.setPlayerInfo(gameRanking);
	}

	public void setQuestion(Question question) {
		this.questionNumber.setText(String.format("Vraag %d:", this.controller.getQuestionNumber()));
		this.questionText.setText(question.getQuestion());
	}
}

