package org.avans.VTSOa.deslimstemens.Views.Game;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Parent;
import javafx.scene.control.Label;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Controllers.Game.OpenDoorController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.OpenAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerScoreControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Mike on 3-1-2016.
 */
public class OpenDoorView extends SceneView {
	private BorderPane parent = new BorderPane();

	private Label questionLabel = new Label();
	private VBox answerBox = new VBox();

	private PlayerScoreControl playerScoreControl = new PlayerScoreControl();
	public PlayerAnswerControl playerAnswerControl = new PlayerAnswerControl();
	private List<OpenAnswerControl> listAnswerControls = new ArrayList<>();

	private OpenDoorController controller;

	public OpenDoorView(OpenDoorController controller) {
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

		this.questionLabel.setTextFill(Color.WHITE);
		this.questionLabel.setMinSize(250, 28);
		this.questionLabel.setWrapText(true);
		this.questionLabel.setStyle("-fx-font-size: 18px");

		this.answerBox.setPadding(new Insets(0, 10, 0, 10));
		this.answerBox.setSpacing(5);
		this.answerBox.setAlignment(Pos.CENTER);

		VBox center = new VBox(this.questionLabel, this.answerBox);
		center.setMinHeight(200);
		BorderPane.setMargin(center, new Insets(10));

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
	public Parent getRoot() {
		return this.parent;
	}

	public void setRanking(GameRanking gameRanking) {
		this.playerScoreControl.setPlayerInfo(gameRanking);
	}

	public void setQuestion(Question question) {
		this.questionLabel.setText(question.getQuestion());

		this.listAnswerControls.clear();
		this.answerBox.getChildren().clear();

		question.getAnswers().forEach(answer -> {
			OpenAnswerControl answerControl = new OpenAnswerControl(answer.getAnswer(), "40");
			listAnswerControls.add(answerControl);
			answerBox.getChildren().addAll(answerControl);
		});
	}

	public void revealAnswer(String answer) {
		this.listAnswerControls.forEach(openAnswerControl -> {
			if(openAnswerControl.getAnswer().equals(answer)) {
				openAnswerControl.answer();
			}
		});
	}
}
