package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;

import java.sql.SQLException;
import java.util.List;
import java.util.TimerTask;

public class GameUserTask extends TimerTask {
	private GameListener gameListener;
	private String user;

	private List<GameInfo> games = null;

	public GameUserTask(String user, GameListener gameListener) {
		this.user = user;
		this.gameListener = gameListener;
	}

	@Override
	public void run() {
		try {
			List<GameInfo> newGames = GameRepository.findByUser(this.user);
			if(this.games == null) {
				this.games = newGames;
			} else {
				for(GameInfo game : newGames) {
					GameInfo sameGame = this.containsGame(game);
					if(sameGame != null) { // if contains
						if(hasChanged(game, sameGame)) {
							this.changeGame(game);
							this.gameListener.gameChanged(game);
						}
					} else {
						this.games.add(game);
						this.gameListener.gameChanged(game);
					}
				}
			}
		} catch(SQLException e) {
			System.out.println("Couldn't get game");
			e.printStackTrace();
		}
	}

	private void changeGame(GameInfo game) {
		for(int i = 0; i < this.games.size(); i++) {
			GameInfo oldGame = this.games.get(i);
			if(oldGame.getGameId() == game.getGameId()) {
				this.games.remove(i);
				this.games.add(game);
			}
		}
	}

	public GameInfo containsGame(GameInfo gameInfo) {
		for(GameInfo game : this.games) {
			if(game.getGameId() == gameInfo.getGameId()) {
				return game;
			}
		}
		return null;
	}

	public boolean hasChanged(GameInfo game1, GameInfo game2) {
		boolean sameGame = game1.getGameStatus() == game2.getGameStatus();
		if(game1.getCurrentPlayer() != null) {
			sameGame = sameGame && game1.getCurrentPlayer().equals(game2.getCurrentPlayer());
		}
		if(game1.getGameRound() != null) {
			sameGame = sameGame && game1.getGameRound() == game2.getGameRound();
		}
		return !sameGame;
	}
}
