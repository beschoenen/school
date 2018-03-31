package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Models.Database.PlayerTurn;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;

import java.sql.SQLException;
import java.util.TimerTask;

public class TurnTask extends TimerTask {
	private TurnListener turnListener;
	private Integer gameId;

	private PlayerTurn lastTurn = null;

	public TurnTask(int gameId, TurnListener turnListener) {
		this.gameId = gameId;
		this.turnListener = turnListener;
	}

	@Override
	public void run() {
		try {
			if(lastTurn != null) {
				PlayerTurn playerTurn = TurnRepository.getLastTurn(this.gameId);
				if(playerTurn != null) {
					handleTurn(playerTurn);
				}
			} else {
				TurnRepository.getTurnsByGame(gameId).forEach(this::handleTurn);
			}
		} catch(SQLException e) {
			System.out.println("Couldn't get turn");
			e.printStackTrace();
		}
	}

	private void handleTurn(PlayerTurn playerTurn) {
		boolean sameTurn = this.lastTurn != null &&
				this.lastTurn.getPlayer().equals(playerTurn.getPlayer()) &&
				this.lastTurn.getId().equals(playerTurn.getId()) &&
				this.lastTurn.getGameRound().equals(playerTurn.getGameRound());

		if(!sameTurn) {
			this.lastTurn = playerTurn;
			this.turnListener.newTurn(playerTurn);
		}
	}
}
