package org.avans.VTSOa.deslimstemens.Models.Database;

import java.util.ArrayList;
import java.util.List;

public class Answer {
	private String answer;
	private List<String> alternatives = new ArrayList<>();

	private boolean answered = false;

	public Answer(String answer, List<String> alternatives) {
		this.answer = answer;
		this.alternatives = alternatives;
	}

	public Answer(String answer) {
		this.answer = answer;
	}

	public String getAnswer() {
		return answer;
	}

	public void setAnswer(String answer) {
		this.answer = answer;
	}

	public void addAlterantive(String alternative) {
		if(alternative != null) {
			boolean add = true;
			for(String ignored : this.alternatives) {
				if(alternative.equals(ignored)) {
					add = false;
					break;
				}
			}
			if(add) this.alternatives.add(alternative);
		}
	}

	public List<String> getAlternatives() {
		return alternatives;
	}

	public void setAlternatives(List<String> alternatives) {
		this.alternatives = alternatives;
	}

	public void setAnswered(boolean answered) {
		this.answered = answered;
	}

	public boolean isAnswered() {
		return answered;
	}
}
