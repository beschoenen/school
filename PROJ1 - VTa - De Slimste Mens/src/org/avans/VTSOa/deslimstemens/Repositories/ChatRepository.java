package org.avans.VTSOa.deslimstemens.Repositories;

import org.avans.VTSOa.deslimstemens.Helpers.Database.QueryBuilder;
import org.avans.VTSOa.deslimstemens.Helpers.Helper;
import org.avans.VTSOa.deslimstemens.Models.Database.ChatMessage;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ChatRepository {

	public static List<ChatMessage> findByGame(int gameId) throws SQLException {
		List<ChatMessage> messages = new ArrayList<>();
		ResultSet chatMessages = new QueryBuilder("chatregel")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.get();
		while (chatMessages.next()) {
			Timestamp timestamp = new Timestamp(chatMessages.getTimestamp("tijdstip").getTime());
			timestamp.setNanos(chatMessages.getInt("millisec"));
			messages.add(new ChatMessage(
					timestamp,
					chatMessages.getString("account_naam_zender"),
					chatMessages.getString("bericht")
			));
		}
		return messages;
	}

	public static void postMessage(int game_id, String user, String message, Date date) throws SQLException {
		Timestamp timestamp = new Timestamp(date.getTime());
		new QueryBuilder("chatregel").create()
				.setColumns("spel_id", "tijdstip", "millisec", "account_naam_zender", "bericht")
				.addValues(
						String.valueOf(game_id),
						Helper.stringWithQuotes(timestamp.toString().replaceAll(".[0-9]{3}$", "")),
						String.format("%03d", timestamp.getNanos()),
						Helper.stringWithQuotes(user),
						Helper.stringWithQuotes(message)
				).run();
	}

	public static ChatMessage getLatestMessage(int gameId) throws SQLException {
		ChatMessage chatMessage = null;

		ResultSet messageResult = new QueryBuilder("chatregel")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.orderBy("tijdstip").orderBy("millisec").first();
		if(messageResult.next()) {
			Timestamp timestamp = new Timestamp(messageResult.getTimestamp("tijdstip").getTime());
			timestamp.setNanos(messageResult.getInt("millisec"));
			chatMessage = new ChatMessage(
					timestamp,
					messageResult.getString("account_naam_zender"),
					messageResult.getString("bericht")
			);
		}
		return chatMessage;
	}

	public static List<ChatMessage> getMessagesFromTimeStamp(int gameId, Timestamp timestampLatest) throws SQLException {
		List<ChatMessage> chatMessages = new ArrayList<>();

		ResultSet messageResult = new QueryBuilder("chatregel")
				.selectAll()
				.where("spel_id").equals(String.valueOf(gameId))
				.andWhere("tijdstip").greaterThan(Helper.stringWithQuotes(timestampLatest.toString().replaceAll(".[0-9]{3}$", "")))
				.orderBy("tijdstip", "ASC").orderBy("millisec", "ASC").get();

		while(messageResult.next()) {
			Timestamp timestamp = new Timestamp(messageResult.getTimestamp("tijdstip").getTime());
			timestamp.setNanos(messageResult.getInt("millisec"));
			chatMessages.add(new ChatMessage(
					timestamp,
					messageResult.getString("account_naam_zender"),
					messageResult.getString("bericht")
			));
		}

		return chatMessages;
	}
}
