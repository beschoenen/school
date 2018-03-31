package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Enums.GameRound;

public class GameInfo {
	private int gameId;
	private String opponent;
	private GameStatus gameStatus;
	private GameRound gameRound; // Can be null
	private int turns;
	private String currentPlayer; // Can be null

	public GameInfo(int gameId, String opponent, GameStatus gameStatus, GameRound gameRound, int turns, String currentPlayer) {
		this.gameId = gameId;
		this.opponent = opponent;
		this.gameStatus = gameStatus;
		this.gameRound = gameRound;
		this.turns = turns;
		this.currentPlayer = currentPlayer;
	}

	public int getGameId() {
		return gameId;
	}

	public void setGameId(int gameId) {
		this.gameId = gameId;
	}

	public String getOpponent() {
		return opponent;
	}

	public void setOpponent(String opponent) {
		this.opponent = opponent;
	}

	public GameStatus getGameStatus() {
		return gameStatus;
	}

	public void setGameStatus(GameStatus gameStatus) {
		this.gameStatus = gameStatus;
	}

	public GameRound getGameRound() {
		return gameRound;
	}

	public void setGameRound(GameRound gameRound) {
		this.gameRound = gameRound;
	}

	public int getTurns() {
		return turns;
	}

	public void setTurns(int turns) {
		this.turns = turns;
	}

	public String getCurrentPlayer() {
		return currentPlayer;
	}

	public void setCurrentPlayer(String currentPlayer) {
		this.currentPlayer = currentPlayer;
	}
}
