package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

import java.util.ArrayList;
import java.util.List;

public class Puzzle extends SaveTurn {
	private List<PuzzlePartQuestion> questions = new ArrayList<>();
	private List<PlayerAnswer> answers = new ArrayList<>();

	public Puzzle(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.Puzzle;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
	}

	public Puzzle(
			int gameId,
			int turnId,
			String player,
			TurnStatus turnStatus,
			int secondsRewarded,
			List<PuzzlePartQuestion> questions,
			List<PlayerAnswer> answers
	) {
		super.gameId = gameId;
		super.gameRound = GameRound.Puzzle;
		super.turnId = turnId;
		super.player = player;
		super.turnStatus = turnStatus;
		super.secondsRewarded = secondsRewarded;
		this.questions = questions;
		this.answers = answers;
	}

	public List<PuzzlePartQuestion> getQuestions() {
		return questions;
	}

	public void setQuestions(List<PuzzlePartQuestion> questions) {
		this.questions = questions;
	}

	public void addQuestion(PuzzlePartQuestion question) {
		this.questions.add(question);
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

	public int getAnswerId() {
		if(this.answers.size() < 1) {
			return 1;
		}
		return this.answers.size() +1;
	}
}
