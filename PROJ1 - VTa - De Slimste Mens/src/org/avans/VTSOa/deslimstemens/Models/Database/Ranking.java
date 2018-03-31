package org.avans.VTSOa.deslimstemens.Models.Database;

public class Ranking {
	private String player;
	private int playedGames;
	private int wonGames;
	private int lostGames;
	private int averageSecondsLeft;

	public Ranking(String player, int playedGames, int wonGames, int lostGames, int averageSecondsLeft) {
		this.player = player;
		this.playedGames = playedGames;
		this.wonGames = wonGames;
		this.lostGames = lostGames;
		this.averageSecondsLeft = averageSecondsLeft;
	}

	public String getPlayer() {
		return player;
	}

	public void setPlayer(String player) {
		this.player = player;
	}

	public int getPlayedGames() {
		return playedGames;
	}

	public void setPlayedGames(int playedGames) {
		this.playedGames = playedGames;
	}

	public int getWonGames() {
		return wonGames;
	}

	public void setWonGames(int wonGames) {
		this.wonGames = wonGames;
	}

	public int getLostGames() {
		return lostGames;
	}

	public void setLostGames(int lostGames) {
		this.lostGames = lostGames;
	}

	public int getAverageSecondsLeft() {
		return averageSecondsLeft;
	}

	public void setAverageSecondsLeft(int averageSecondsLeft) {
		this.averageSecondsLeft = averageSecondsLeft;
	}
}
