package org.avans.VTSOa.deslimstemens.Models.Table;

import com.jfoenix.controls.JFXButton;

/**
 * Created by rogier on 28-12-2015.
 */
public class GameListObserver {

    private String challenger;
    private String opponent;
    private JFXButton button;

    public GameListObserver(String challenger, String opponent, JFXButton button) {
        this.challenger = challenger;
        this.opponent = opponent;
        this.button = button;
    }

    public String getChallenger() {
        return challenger;
    }

    public void setChallenger(String challenger) {
        this.challenger = challenger;
    }

    public String getOpponent() {
        return opponent;
    }

    public void setOpponent(String opponent) {
        this.opponent = opponent;
    }

    public JFXButton getButton() {
        return button;
    }

    public void setButton(JFXButton button) {
        this.button = button;
    }
}
