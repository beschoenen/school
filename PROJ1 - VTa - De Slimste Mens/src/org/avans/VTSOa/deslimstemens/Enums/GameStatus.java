package org.avans.VTSOa.deslimstemens.Enums;

public enum GameStatus {
    Ended("afgelopen"),
    Passed("verworpen"),
    Challenged("uitgedaagd"),
    ChallengeReceived("uitdaging_ontvangen"),
    Going("bezig"),
    NoGame("geen_spel");

    public String value;

    GameStatus(String value) {
        this.value = value;
    }

	public static GameStatus fromString(String text) {
		if (text != null) {
			for (GameStatus value : GameStatus.values()) {
				if (text.equalsIgnoreCase(value.value)) {
					return value;
				}
			}
		}
		return null;
	}
}
