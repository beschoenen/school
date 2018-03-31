package org.avans.VTSOa.deslimstemens.Models.Database;

public class Column { // Used for update statements
	private String column;
	private String value;

	public Column(String column, String value) {
		this.column = column;
		this.value = value;
	}

	public Column(String column) {
		this.column = column;
	}

	public String getColumn() {
		return column;
	}

	public Object getValue() {
		return value;
	}

	public void setValue(String value) {
		this.value = value;
	}
}
