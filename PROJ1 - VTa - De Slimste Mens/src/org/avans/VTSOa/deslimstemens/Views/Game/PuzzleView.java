package org.avans.VTSOa.deslimstemens.Views.Game;

import javafx.geometry.Insets;
import javafx.scene.Parent;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Controllers.Game.PuzzleController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.OpenAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerAnswerControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PlayerScoreControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.PuzzleTileControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Mike on 3-1-2016.
 */
public class PuzzleView extends SceneView {
	private BorderPane parent = new BorderPane();

	private VBox answerBox = new VBox();

	public PlayerScoreControl playerScoreControl = new PlayerScoreControl();
	public PlayerAnswerControl playerAnswerControl = new PlayerAnswerControl();
	private List<OpenAnswerControl> listAnswerControls = new ArrayList<>();

	private PuzzleTileControl tileControl = new PuzzleTileControl();

	private PuzzleController controller;

	public PuzzleView(PuzzleController controller) {
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

		this.answerBox.setPadding(new Insets(0, 10, 0, 10));
		this.answerBox.setSpacing(5);

		VBox container = new VBox();
		container.getChildren().addAll(tileControl, this.answerBox);
		container.setSpacing(5);
		container.setMinHeight(240);

		this.parent.setTop(this.playerScoreControl);
		this.parent.setCenter(container);
		this.parent.setBottom(this.playerAnswerControl);
		this.parent.setBackground(new Background(new BackgroundFill(Color.RED, null, null)));
		this.parent.applyCss();
		BorderPane.setMargin(container, new Insets(10));
	}

	@Override
	public double getWidth() {
		return 700;
	}

	@Override
	public double getHeight() {
		return 540;
	}

	@Override
	public Parent getRoot() {
		return this.parent;
	}

	public void setRanking(GameRanking gameRanking) {
		this.playerScoreControl.setPlayerInfo(gameRanking);
	}

	public void setQuestions(List<Question> questions) {
		this.listAnswerControls.clear();
		this.answerBox.getChildren().clear();

		for(int i = 0; i < questions.size(); i++){
			OpenAnswerControl answer = new OpenAnswerControl(questions.get(i).getQuestion(), "60");
			listAnswerControls.add(answer);
			this.answerBox.getChildren().add(answer);
		}
		this.tileControl.create(questions);
	}

	public void revealAnswer(String answer) {
		this.listAnswerControls.forEach(openAnswerControl -> {
			if(openAnswerControl.getAnswer().equals(answer)) {
				openAnswerControl.answer();
			}
		});
	}
}
