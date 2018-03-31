package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

public class R369PartQuestion {
	private int followNumber;
	private int questionId;
	private String answer;

	public R369PartQuestion(int followNumber, int questionId, String answer) {
		this.followNumber = followNumber;
		this.questionId = questionId;
		this.answer = answer;
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

	public String getAnswer() {
		return answer;
	}

	public void setAnswer(String answer) {
		this.answer = answer;
	}
}
