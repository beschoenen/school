package org.avans.VTSOa.deslimstemens.Enums;

public enum PlayerRole {
    Player("player"),
    Observer("observer");
    public String value;

    PlayerRole(String value) {
        this.value = value;
    }

    public static PlayerRole fromString(String text) {
        if (text != null) {
            for (PlayerRole value : PlayerRole.values()) {
                if (text.equalsIgnoreCase(value.value)) {
                    return value;
                }
            }
        }
        return null;
    }
}
