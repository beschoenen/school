package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Helpers.BaseTimer;
import org.avans.VTSOa.deslimstemens.Models.Database.ChatMessage;

public abstract class ChatListener extends BaseTimer {
	public abstract void newMessage(ChatMessage message);

	public ChatListener(Integer gameId) {
		super();
		this.timer.schedule(new ChatTask(gameId, this), 0, 2000);
	}
}
