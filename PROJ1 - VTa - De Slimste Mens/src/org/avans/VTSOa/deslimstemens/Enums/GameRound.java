package org.avans.VTSOa.deslimstemens.Enums;

public enum GameRound {
    OpenDoor("opendeur"),
    Finale("finale"),
    Puzzle("puzzel"),
    Framed("ingelijst"),
    R369("369");

    public String value;

    GameRound(String value) {
        this.value = value;
    }

    public static GameRound fromString(String text) {
        if (text != null) {
            for (GameRound value : GameRound.values()) {
                if (text.equalsIgnoreCase(value.value)) {
                    return value;
                }
            }
        }
        return null;
    }
}
