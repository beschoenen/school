package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

import java.util.ArrayList;
import java.util.List;

public class R369 extends SaveTurn {
	List<R369PartQuestion> questions = new ArrayList<>();

	public R369(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.R369;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
	}

	public R369(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded,
			List<R369PartQuestion> questions
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.R369;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
		this.questions = questions;
	}

	public List<R369PartQuestion> getQuestions() {
		return questions;
	}

	public void setQuestions(List<R369PartQuestion> questions) {
		this.questions = questions;
	}

	public void addQuestion(R369PartQuestion question) {
		this.questions.add(question);
	}

	public List<Integer> getQuestionIdList() {
		List<Integer> IDs = new ArrayList<>();
		this.questions.forEach(question -> IDs.add(question.getQuestionId()));
		return IDs;
	}

	public int getCurrentFollowNumber() {
		if(this.getQuestions().size() > 0) {
			return this.questions.get(this.questions.size() -1).getFollowNumber();
		}
		return 0;
	}
}
