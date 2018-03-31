package org.avans.VTSOa.deslimstemens.Models.Database;

import java.sql.Timestamp;

public class ChatMessage {
	private Timestamp timestamp;
	private String name;
	private String message;

	public ChatMessage(Timestamp timestamp, String name, String message) {
		this.timestamp = timestamp;
		this.name = name;
		this.message = message;
	}

	public Timestamp getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(Timestamp timestamp) {
		this.timestamp = timestamp;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(String message) {
		this.message = message;
	}
}
