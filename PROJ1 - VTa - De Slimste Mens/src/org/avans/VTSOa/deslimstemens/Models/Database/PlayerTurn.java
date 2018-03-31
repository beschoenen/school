package org.avans.VTSOa.deslimstemens.Models.Database;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

public class PlayerTurn {
	private Integer id;
	private Integer gameId;
	private GameRound gameRound;
	private String player;
	private TurnStatus turnStatus;
	private Integer secondsRewarded;
	private Integer secondsTaken;

	public PlayerTurn(Integer id, Integer gameId, GameRound gameRound, String player, TurnStatus turnStatus, Integer secondsRewarded, Integer secondsTaken) {
		this.id = id;
		this.gameId = gameId;
		this.gameRound = gameRound;
		this.player = player;
		this.turnStatus = turnStatus;
		this.secondsRewarded = secondsRewarded;
		this.secondsTaken = secondsTaken;
	}

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public Integer getGameId() {
		return gameId;
	}

	public void setGameId(Integer gameId) {
		this.gameId = gameId;
	}

	public GameRound getGameRound() {
		return gameRound;
	}

	public void setGameRound(GameRound gameRound) {
		this.gameRound = gameRound;
	}

	public String getPlayer() {
		return player;
	}

	public void setPlayer(String player) {
		this.player = player;
	}

	public TurnStatus getTurnStatus() {
		return turnStatus;
	}

	public void setTurnStatus(TurnStatus turnStatus) {
		this.turnStatus = turnStatus;
	}

	public Integer getSecondsRewarded() {
		return secondsRewarded;
	}

	public void setSecondsRewarded(Integer secondsRewarded) {
		this.secondsRewarded = secondsRewarded;
	}

	public Integer getSecondsTaken() {
		return secondsTaken;
	}

	public void setSecondsTaken(Integer secondsTaken) {
		this.secondsTaken = secondsTaken;
	}
}
