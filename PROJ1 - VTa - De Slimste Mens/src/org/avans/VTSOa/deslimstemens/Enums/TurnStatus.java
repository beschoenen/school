package org.avans.VTSOa.deslimstemens.Enums;

public enum TurnStatus {
	Going("bezig"),
	Timeout("timeout"),
	Passed("pas"),
	Correct("goed"),
	Wrong("fout"),
	Bonus("bonus");
	public String value;

	TurnStatus(String value) {
		this.value = value;
	}

	public static TurnStatus fromString(String text) {
		if (text != null) {
			for (TurnStatus value : TurnStatus.values()) {
				if (text.equalsIgnoreCase(value.value)) {
					return value;
				}
			}
		}
		return null;
	}
}
