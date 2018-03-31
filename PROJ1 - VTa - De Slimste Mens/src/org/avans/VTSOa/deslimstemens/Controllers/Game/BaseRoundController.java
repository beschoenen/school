package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.AnswerListener;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.MissingException;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Navigator;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Models.Database.GameRanking;
import org.avans.VTSOa.deslimstemens.Models.Database.Question;
import org.avans.VTSOa.deslimstemens.Models.Database.Turn.SaveTurn;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Repositories.RankingRepository;
import org.avans.VTSOa.deslimstemens.Repositories.TurnRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;

import java.sql.SQLException;
import java.util.Timer;
import java.util.TimerTask;

public abstract class BaseRoundController extends SceneController implements AnswerListener {
    protected Timer timer = new Timer(false);

    protected GameInfo game;
    protected GameRanking gameRanking;

    protected Question question;

    private int time = 0;

    public BaseRoundController() {
        try {
            this.game = GameRepository.findById(RunTimeSettings.gameId, RunTimeSettings.user.getName());
            createTurn();
            this.gameRanking = RankingRepository.getGameRanking(RunTimeSettings.gameId);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    protected abstract void createTurn();

    /**
     * Play a turn of the current round
     */
    public abstract void getData();

    /**
     * All logic required to render the current turn
     */
    public void render() {
        this.timer.schedule(new TimerTask() {
            @Override
            public void run() {
                time++;
                tick(time);
            }
        }, 0, 1000);
    }

    /**
     * Allow this round to end cleanly, this generally means returning the player to the lobby
     */
    public abstract void endRound();

	/**
     * End the current turn
     */
    public abstract void endTurn();

	/**
     * Get a question for the turn
     */
    protected abstract void getQuestion();

    /**
     * Called when the timer ticks
     */
    protected abstract void tick(final int time);

	/**
     * Get the current turn time
     */
    protected int getTime() {
        return this.time;
    }

	/**
	 * Get the current question number
     */
    public abstract int getQuestionNumber();

    /**
     * Return player to the LobbyController
     */
    public final void returnToLobby() {
        Platform.runLater(() -> {
	        try {
		        Navigator.previous();
	        } catch (MissingException e) {
		        e.printStackTrace();
	        }
        });
    }

    protected void endGame(SaveTurn saveTurn){
        try {
            GameRepository.updateStatus(this.game.getGameId(), GameStatus.Ended);
            TurnRepository.updateTurn(saveTurn);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onLeave() {
        this.timer.cancel();
        this.timer.purge();
    }
}