package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

import java.util.ArrayList;
import java.util.List;

public class FramedOpenDoor extends SaveTurn {
	private int questionId;
	private List<PlayerAnswer> answers = new ArrayList<>(); // Framed: 10 / OpenDoor: 4

	public FramedOpenDoor(
			boolean isFramed,
			int gameId,
			int turnId,
			int questionId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded
	) {
		super.gameId = gameId;
		super.gameRound = isFramed ? GameRound.Framed : GameRound.OpenDoor;
		super.turnId = turnId;
		this.questionId = questionId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
	}

	public FramedOpenDoor(
			boolean isFramed,
			int gameId,
			int turnId,
			int questionId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded,
			List<PlayerAnswer> answers
	) {
		super.gameId = gameId;
		super.gameRound = isFramed ? GameRound.Framed : GameRound.OpenDoor;
		super.turnId = turnId;
		this.questionId = questionId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
		this.answers = answers;
	}

	public int getQuestionId() {
		return questionId;
	}

	public void setQuestionId(int questionId) {
		this.questionId = questionId;
	}

	public List<PlayerAnswer> getAnswers() {
		return answers;
	}

	public void setAnswers(List<PlayerAnswer> answers) {
		this.answers = answers;
	}

	public void addAnswer(PlayerAnswer answer) {
		this.answers.add(answer);
	}

	public int getAnswerId() {
		if(this.answers.size() < 1) {
			return 1;
		}
		return this.answers.size() +1;
	}
}
