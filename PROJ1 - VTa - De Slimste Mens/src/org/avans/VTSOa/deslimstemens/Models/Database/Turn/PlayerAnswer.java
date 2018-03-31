package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

public class PlayerAnswer {
	private int id; // antwoord_id
	private String answer;
	private int moment; // Aantal seconden binnen de beurt

	public PlayerAnswer(int id, String answer, int moment) {
		this.id = id;
		this.answer = answer;
		this.moment = moment;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getAnswer() {
		return answer;
	}

	public void setAnswer(String answer) {
		this.answer = answer;
	}

	public int getMoment() {
		return moment;
	}

	public void setMoment(int moment) {
		this.moment = moment;
	}
}
