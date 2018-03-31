package org.avans.VTSOa.deslimstemens.Interfaces;

import org.avans.VTSOa.deslimstemens.Models.Database.Question;

/**
 * Created by tyil on 12/30/15.
 */
public interface PlayableRound {
    boolean isTurnComplete();
    boolean isTurnCorrect();

    void prepareTurn();
    void render();
}
