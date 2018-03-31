package org.avans.VTSOa.deslimstemens.Controllers.Game;

import javafx.application.Platform;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Navigator;
import org.avans.VTSOa.deslimstemens.Models.Database.GameInfo;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;

import java.sql.SQLException;

public class GameController {
	private GameInfo gameInfo;

	public GameController(int gameId) {
		// fetch the current GameInfo
	    try {
		    this.gameInfo = GameRepository.findById(gameId, RunTimeSettings.user.getName());
	    } catch (SQLException e) {
		    e.printStackTrace();
	    }
    }

	public void playRound() throws Exception {
		BaseRoundController round = getRoundController();

		// show the view
		RunTimeSettings.mainStager.addSceneController(round);
		try {
			Navigator.show("MainStage", round.getName());
		} catch (Exception e) {
			e.printStackTrace();
		}
			// TODO show loading
		new Thread(() -> {
			round.getData();
			Platform.runLater(round::render);
		}).start();
	}

	private BaseRoundController getRoundController() throws Exception {
		BaseRoundController controller;
		switch (this.gameInfo.getGameRound()) {
			case R369:
				controller = new R369Controller();
				break;
			case OpenDoor:
				controller = new OpenDoorController();
				break;
			case Puzzle:
				controller = new PuzzleController();
				break;
			case Framed:
				controller = new FramedController();
				break;
			case Finale:
				controller = new FinaleController();
				break;
			default:
				throw new Exception("Incorrect round");
		}
		return controller;
	}
}

