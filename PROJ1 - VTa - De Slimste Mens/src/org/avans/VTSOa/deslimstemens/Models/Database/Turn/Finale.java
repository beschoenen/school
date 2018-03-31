package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

import java.util.ArrayList;
import java.util.List;

public class Finale extends SaveTurn {
	private int questionId;
	private int secondsLost;
	private List<PlayerAnswer> answers = new ArrayList<>(); // 5

	public Finale(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded,
			int questionId,
			int secondsLost
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.Finale;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
		this.questionId = questionId;
		this.secondsLost = secondsLost;
	}

	public Finale(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded,
			int questionId,
			int secondsLost,
	        List<PlayerAnswer> answers
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.Finale;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
		this.questionId = questionId;
		this.secondsLost = secondsLost;
		this.answers = answers;
	}

	public int getQuestionId() {
		return questionId;
	}

	public void setQuestionId(int questionId) {
		this.questionId = questionId;
	}

	public int getSecondsLost() {
		return secondsLost;
	}

	public void setSecondsLost(int secondsLost) {
		this.secondsLost = secondsLost;
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
