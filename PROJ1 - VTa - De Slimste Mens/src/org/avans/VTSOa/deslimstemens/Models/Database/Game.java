package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.GameStatus;

public class Game {
	private Integer id;
	private String player1;
	private String player2;
	private GameStatus gameStatus;

	public Game(Integer id, String player1, String player2, GameStatus gameStatus) {
		this.id = id;
		this.player1 = player1;
		this.player2 = player2;
		this.gameStatus = gameStatus;
	}

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String getPlayer1() {
		return player1;
	}

	public void setPlayer1(String player1) {
		this.player1 = player1;
	}

	public String getPlayer2() {
		return player2;
	}

	public void setPlayer2(String player2) {
		this.player2 = player2;
	}

	public GameStatus getGameStatus() {
		return gameStatus;
	}

	public void setGameStatus(GameStatus gameStatus) {
		this.gameStatus = gameStatus;
	}
}
