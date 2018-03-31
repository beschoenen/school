package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;

import java.util.List;

public class Question {
	private Integer id;
	private GameRound gameRound;
	private String question;
	private List<Answer> answers;

	private int puzzleNumber;

	private boolean fromPreviousPlayer = false;

	public Question(Integer id, GameRound gameRound, String question, List<Answer> answers) {
		this.id = id;
		this.gameRound = gameRound;
		this.question = question;
		this.answers = answers;
	}

	public Question(Integer id, GameRound gameRound, String question) {
		this.id = id;
		this.gameRound = gameRound;
		this.question = question;
	}

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public GameRound getGameRound() {
		return gameRound;
	}

	public void setGameRound(GameRound gameRound) {
		this.gameRound = gameRound;
	}

	public String getQuestion() {
		return question;
	}

	public void setQuestion(String question) {
		this.question = question;
	}

	public List<Answer> getAnswers() {
		return answers;
	}

	public void setAnswers(List<Answer> answers) {
		this.answers = answers;
	}

	public boolean isFromPreviousPlayer() {
		return fromPreviousPlayer;
	}

	public void setFromPreviousPlayer(boolean fromPreviousPlayer) {
		this.fromPreviousPlayer = fromPreviousPlayer;
	}

	public boolean checkGivenAnswer(String givenAnswer) {
		for(Answer answer : this.answers) {
			if(checkAnswer(givenAnswer.toLowerCase(), answer.getAnswer().toLowerCase())) {
				return true;
			} else {
				for(String alternative : answer.getAlternatives()) {
					if(checkAnswer(givenAnswer.toLowerCase(), alternative.toLowerCase())) return true;
				}
			}
		}
		return false;
	}

	public String checkPuzzleAnswer(String givenAnswer){
		if(checkAnswer(givenAnswer.toLowerCase(), this.getQuestion().toLowerCase())){
			return this.getQuestion();
		}
		return null;
	}

	public String getGivenAnswer(String givenAnswer) {
		for(Answer answer : this.answers) {
			if(checkAnswer(givenAnswer.toLowerCase(), answer.getAnswer().toLowerCase()) && !answer.isAnswered()) {
				answer.setAnswered(true);
				return answer.getAnswer();
			} else {
				for(String alternative : answer.getAlternatives()) {
					if(checkAnswer(givenAnswer.toLowerCase(), alternative.toLowerCase()) && !answer.isAnswered()) {
						answer.setAnswered(true);
						return answer.getAnswer();
					}
				}
			}
		}
		return null;
	}

	private boolean checkAnswer(String givenAnswer, String correctAnswer) {
		int distance = levenshteinDistance(new CharSequence() {
			@Override
			public int length() {
				return givenAnswer.length();
			}

			@Override
			public char charAt(int index) {
				return givenAnswer.charAt(index);
			}

			@Override
			public CharSequence subSequence(int start, int end) {
				return givenAnswer.subSequence(start, end);
			}
		}, new CharSequence() {
			@Override
			public int length() {
				return correctAnswer.length();
			}

			@Override
			public char charAt(int index) {
				return correctAnswer.charAt(index);
			}

			@Override
			public CharSequence subSequence(int start, int end) {
				return correctAnswer.subSequence(start, end);
			}
		});
		return ((double) distance / correctAnswer.length()) * 100 < 20;
	}

	private int levenshteinDistance (CharSequence lhs, CharSequence rhs) {
		int len0 = lhs.length() + 1;
		int len1 = rhs.length() + 1;

		// the array of distances
		int[] cost = new int[len0];
		int[] newcost = new int[len0];

		// initial cost of skipping prefix in String s0
		for (int i = 0; i < len0; i++) cost[i] = i;

		// dynamically computing the array of distances

		// transformation cost for each letter in s1
		for (int j = 1; j < len1; j++) {
			// initial cost of skipping prefix in String s1
			newcost[0] = j;

			// transformation cost for each letter in s0
			for(int i = 1; i < len0; i++) {
				// matching current letters in both strings
				int match = (lhs.charAt(i - 1) == rhs.charAt(j - 1)) ? 0 : 1;

				// computing cost for each transformation
				int cost_replace = cost[i - 1] + match;
				int cost_insert  = cost[i] + 1;
				int cost_delete  = newcost[i - 1] + 1;

				// keep minimum cost
				newcost[i] = Math.min(Math.min(cost_insert, cost_delete), cost_replace);
			}

			// swap cost/newcost arrays
			int[] swap = cost; cost = newcost; newcost = swap;
		}

		// the distance is the cost for transforming all letters in both strings
		return cost[len0 - 1];
	}

	public int getPuzzleNumber() {
		return puzzleNumber;
	}

	public void setPuzzleNumber(int puzzleNumber) {
		this.puzzleNumber = puzzleNumber;
	}
}
