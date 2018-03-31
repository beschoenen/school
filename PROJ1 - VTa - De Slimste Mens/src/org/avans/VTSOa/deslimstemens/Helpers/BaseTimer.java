package org.avans.VTSOa.deslimstemens.Helpers;

import java.util.Timer;

public abstract class BaseTimer {
	public final Timer timer = new Timer(false);

	public final void stop() {
		this.timer.cancel();
		this.timer.purge();
	}
}
