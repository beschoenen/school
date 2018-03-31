package org.avans.VTSOa.deslimstemens.Models.Database.Turn;

import org.avans.VTSOa.deslimstemens.Enums.GameRound;
import org.avans.VTSOa.deslimstemens.Enums.TurnStatus;

public abstract class SaveTurn {
	protected int gameId;
	protected GameRound gameRound;
	protected int turnId;
	protected String player;
	protected TurnStatus turnStatus;
	protected int secondsRewarded;

	public int getGameId() {
		return gameId;
	}

	public void setGameId(int gameId) {
		this.gameId = gameId;
	}

	public GameRound getGameRound() {
		return gameRound;
	}

	public void setGameRound(GameRound gameRound) {
		this.gameRound = gameRound;
	}

	public int getTurnId() {
		return turnId;
	}

	public void setTurnId(int turnId) {
		this.turnId = turnId;
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

	public int getSecondsRewarded() {
		return secondsRewarded;
	}

	public void setSecondsRewarded(int secondsRewarded) {
		this.secondsRewarded = secondsRewarded;
	}
}
