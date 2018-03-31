package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Helpers.BaseTimer;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;

public abstract class GameListener extends BaseTimer {
	public abstract void gameChanged(GameInfo game);

	public GameListener(String username) {
		super();
		this.timer.schedule(new GameUserTask(username, this), 0, 2000);
	}

	public GameListener(int gameId, String user) {
		super();
		this.timer.schedule(new GameIdTask(gameId, user, this), 0, 2000);
	}
}
