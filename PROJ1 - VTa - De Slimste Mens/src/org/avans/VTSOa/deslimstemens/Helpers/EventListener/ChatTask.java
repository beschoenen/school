package org.avans.VTSOa.deslimstemens.Helpers.EventListener;

import org.avans.VTSOa.deslimstemens.Models.Database.ChatMessage;
import org.avans.VTSOa.deslimstemens.Repositories.ChatRepository;

import java.sql.SQLException;
import java.util.List;
import java.util.TimerTask;

public class ChatTask extends TimerTask {
	private Integer gameId;
	private ChatListener chatListener;

	private ChatMessage lastMessage = null;

	public ChatTask(Integer gameId, ChatListener chatListener) {
		this.gameId = gameId;
		this.chatListener = chatListener;
	}

	@Override
	public void run() {
		try {
			List<ChatMessage> chatMessages;
			if(lastMessage == null) {
				chatMessages = ChatRepository.findByGame(this.gameId);
			} else {
				chatMessages = ChatRepository.getMessagesFromTimeStamp(this.gameId, this.lastMessage.getTimestamp());
			}

			for(ChatMessage chatMessage : chatMessages) {
				boolean sameMessage = false;
				if(this.lastMessage != null) {
					sameMessage = chatMessage.getName().equals(lastMessage.getName()) &&
							chatMessage.getMessage().equals(lastMessage.getMessage()) &&
							chatMessage.getTimestamp().equals(lastMessage.getTimestamp());
				}

				if(!sameMessage) {
					this.lastMessage = chatMessage;
					this.chatListener.newMessage(chatMessage);
				}
			}
		} catch(SQLException e) {
			System.out.println("Failed to get message");
			e.printStackTrace();
		}
	}

}
