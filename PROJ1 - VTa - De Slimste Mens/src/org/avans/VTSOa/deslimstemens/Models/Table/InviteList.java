package org.avans.VTSOa.deslimstemens.Models.Table;

import com.jfoenix.controls.JFXButton;

/**
 * Created by rogier on 18-12-2015.
 */
public class InviteList {
    private String name;
    private JFXButton button;

    public InviteList(String name, JFXButton button) {
        this.name = name;
        this.button = button;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public JFXButton getButton() {
        return button;
    }

    public void setButton(JFXButton button) {
        this.button = button;
    }
}
