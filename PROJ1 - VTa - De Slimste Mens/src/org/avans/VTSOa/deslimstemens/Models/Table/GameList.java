package org.avans.VTSOa.deslimstemens.Models.Table;

import com.jfoenix.controls.JFXButton;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;

/**
 * Created by rogier on 18-12-2015.
 */
public class GameList {
    private String opponent;
    private String gameStatus;
    private JFXButton button;

    public GameList(String opponent, String gameStatus, JFXButton button) {
        this.opponent = opponent;
        this.gameStatus = gameStatus;
        this.button = button;
    }

    public String getOpponent() {
        return opponent;
    }

    public void setOpponent(String opponent) {
        this.opponent = opponent;
    }

    public String getGameStatus() {
        return gameStatus;
    }

    public void setGameStatus(String gameStatus) {
        this.gameStatus = gameStatus;
    }

    public JFXButton getButton() {
        return button;
    }

    public void setButton(JFXButton button) {
        this.button = button;
    }
}
