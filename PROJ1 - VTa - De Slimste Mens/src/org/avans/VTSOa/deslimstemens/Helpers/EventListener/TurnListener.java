package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Helpers.BaseTimer;
import org.avans.VTSOa.deslimstemens.Models.Database.PlayerTurn;

public abstract class TurnListener extends BaseTimer {
	public abstract void newTurn(PlayerTurn turn);

	public TurnListener(int gameId) {
		super();
		this.timer.schedule(new TurnTask(gameId, this), 0, 2000);
	}
}
