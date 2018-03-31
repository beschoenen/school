package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.GameStatus;

public class PlayerGameStatus {
	private String name;
	private Integer gameId;
	private GameStatus gameStatus;

	public PlayerGameStatus(String name, Integer gameId, GameStatus gameStatus) {
		this.name = name;
		this.gameId = gameId;
		this.gameStatus = gameStatus;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public Integer getGameId() {
		return gameId;
	}

	public void setGameId(Integer gameId) {
		this.gameId = gameId;
	}

	public GameStatus getGameStatus() {
		return gameStatus;
	}

	public void setGameStatus(GameStatus gameStatus) {
		this.gameStatus = gameStatus;
	}
}
