package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

public class PuzzlePartQuestion {
	private int followNumber;
	private int questionId;

	public PuzzlePartQuestion(int followNumber, int questionId) {
		this.followNumber = followNumber;
		this.questionId = questionId;
	}

	public int getFollowNumber() {
		return followNumber;
	}

	public void setFollowNumber(int followNumber) {
		this.followNumber = followNumber;
	}

	public int getQuestionId() {
		return questionId;
	}

	public void setQuestionId(int questionId) {
		this.questionId = questionId;
	}
}
