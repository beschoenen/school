package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import com.jfoenix.controls.JFXProgressBar;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.beans.property.ReadOnlyDoubleProperty;
import javafx.beans.property.ReadOnlyDoubleWrapper;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.event.EventType;
import javafx.geometry.Insets;
import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.shape.Circle;
import javafx.scene.text.Text;
import javafx.scene.text.TextAlignment;
import javafx.util.Duration;
import javafx.util.Pair;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;

import javax.swing.event.DocumentEvent;

/**
 * Created by Mike on 3-1-2016.
 */
public class PlayerScoreControl extends BorderPane {

	private VBox playerLeft;
	private VBox playerRight;
	private VBox timer = null;

	private Label timerText;
	private JFXProgressBar timerBar;

	private int totalTime;
	private int currentTime = 0;

	private boolean showTimer = true;

	private GameRanking ranking;

	public PlayerScoreControl(int totalTime){
		this.totalTime = totalTime;
		this.setShowTimer(true);
		init();
	}

	public PlayerScoreControl(){
		init();
	}

	private void init(){
		this.setBackground(new Background(new BackgroundFill(Color.rgb(89, 8, 8).hsb(0, 0.91, 0.35), null, null)));
	}

	public void setShowTimer(boolean isEnabled){
		this.showTimer = isEnabled;
		if(isEnabled){
			setTimer();
		}
		else if (timer != null){
			this.timer = new VBox();
			this.setCenter(this.timer);
		}
	}

	public void addScore(int player, int scoreToAdd){
		this.ranking.addScore(player, scoreToAdd);
		setPlayerInfo(this.ranking);
	}

	public void setTime(int time){
		this.currentTime = time;
		updateTime();
	}

	private void updateTime(){
		this.timerText.setText(String.valueOf(totalTime - currentTime));
		this.timerBar.setProgress(((double) currentTime) / ((double) totalTime));
	}

	private void setTimer(){
		this.timer = createTimer();
		this.setCenter(this.timer);
		BorderPane.setAlignment(this.timer, Pos.TOP_CENTER);
	}

	private void setLeftPlayerInfo(Pair<String, Integer> info){
		playerLeft = createCircle(info.getKey(), info.getValue());
		this.setLeft(playerLeft);
		BorderPane.setMargin(playerLeft, new Insets(10));
	}

	private void setRightPlayerInfo(Pair<String, Integer> info){
		playerRight = createCircle(info.getKey(), info.getValue());
		this.setRight(playerRight);
		BorderPane.setMargin(playerRight, new Insets(10));
	}

	public void setPlayerInfo(GameRanking ranking){
		this.ranking = ranking;
		setLeftPlayerInfo(ranking.getPlayer(1));
		setRightPlayerInfo(ranking.getPlayer(2));
	}

	private VBox createTimer() {
		VBox timer = new VBox();
		timer.setMaxSize(Double.MAX_VALUE, Double.MAX_VALUE);

		this.timerText = new Label();
		this.timerText.setTextFill(Color.WHITE);
		this.timerText.setText(String.valueOf(totalTime));

		this.timerBar = UI.ProgressBar();
		this.timerBar.setMaxWidth(Double.MAX_VALUE);
		this.timerBar.setProgress(0);

		timer.getChildren().addAll(timerText, timerBar);
		VBox.setMargin(timerText, new Insets(0, 0, 0, 135));
		return timer;
	}

	private VBox createCircle(String name, int score){
		VBox main = new VBox();

		StackPane scorePane = new StackPane();
		Circle scoreCircle = new Circle(50, Color.RED);
		scoreCircle.setStroke(Color.DARKRED);
		scoreCircle.setStrokeWidth(3);
		Text scoreText = new Text(String.valueOf(score));
		scoreText.setStyle("-fx-font-size: 35px");
		scoreText.setStroke(Color.BLACK);
		scoreText.setFill(Color.YELLOW);
		scorePane.getChildren().addAll(scoreCircle, scoreText);

		Label playerText = new Label(name);
		playerText.setMaxWidth(100);
		playerText.setTextAlignment(TextAlignment.CENTER);
		playerText.setAlignment(Pos.CENTER);
		playerText.setTextFill(Color.WHITE);
		playerText.setStyle("-fx-font-size: 24px");

		main.getChildren().addAll(scorePane, playerText);

		return main;
	}
}
