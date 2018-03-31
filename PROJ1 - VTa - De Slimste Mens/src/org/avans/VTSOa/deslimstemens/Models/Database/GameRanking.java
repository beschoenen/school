package org.avans.VTSOa.deslimstemens.Models.Database;

import javafx.util.Pair;

import java.util.HashMap;

public class GameRanking {
	private HashMap<Integer, Pair<String, Integer>> players = new HashMap<>();

	public GameRanking(String player1, Integer scorePlayer1, String player2, Integer scorePlayer2) {
		players.put(1, new Pair<>(player1, scorePlayer1));
		players.put(2, new Pair<>(player2, scorePlayer2));
	}

	public Pair<String, Integer> getPlayer(int player) {
		return this.players.get(player);
	}

	public int getPlayerNumber(String player) {
		for(int i = 0; i < this.players.size(); i++) {
			Pair<String, Integer> pair = this.players.get(i +1);
			if(pair.getKey().equals(player)) return pair.getValue();
		}
		return 0;
	}

	public void setScore(int player, int score) {
		Pair<String, Integer> pair = this.players.get(player);
		this.players.put(player, new Pair<>(pair.getKey(), score));
	}

	public void addScore(int player, int score) {
		Pair<String, Integer> pair = this.players.get(player);
		this.players.put(player, new Pair<>(pair.getKey(), pair.getValue() + score));
	}

	public void removeScore(int player, int score) {
		Pair<String, Integer> pair = this.players.get(player);
		this.players.put(player, new Pair<>(pair.getKey(), pair.getValue() - score));
	}

	public String getPlayerWithLowestScore() {
		Pair<String, Integer> player = null;

		for(int i = 0; i < this.players.size(); i++) {
			Pair<String, Integer> pPlayer = this.players.get(i+1);
			if(player == null) {
				player = pPlayer;
			} else if(player.getValue() > pPlayer.getValue()) {
				player = pPlayer;
			}
		}
		return player != null ? player.getKey() : null;
	}
}
