package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;

import java.sql.SQLException;
import java.util.TimerTask;

public class GameIdTask extends TimerTask {
	private GameListener gameListener;
	private Integer gameId;
	private String user;

	private GameInfo game;

	public GameIdTask(int gameId, String user, GameListener gameListener) {
		this.gameId = gameId;
		this.user = user;
		this.gameListener = gameListener;
	}

	@Override
	public void run() {
		try {
			GameInfo newGame = GameRepository.findById(this.gameId, this.user);
			if(this.game == null) this.game = newGame;

			boolean sameGame = this.game.getGameStatus() == newGame.getGameStatus();
			if(this.game.getCurrentPlayer() != null) {
				sameGame = sameGame && this.game.getCurrentPlayer().equals(newGame.getCurrentPlayer());
			}
			if(this.game.getGameRound() != null) {
				sameGame = sameGame && this.game.getGameRound() == newGame.getGameRound();
			}

			if(!sameGame) {
				this.game = newGame;
				this.gameListener.gameChanged(newGame);
			}
		} catch(SQLException e) {
			System.out.println("Couldn't get game");
			e.printStackTrace();
		}
	}
}
