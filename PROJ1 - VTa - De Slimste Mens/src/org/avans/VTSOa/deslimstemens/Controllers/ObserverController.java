package org.avans.VTSOa.deslimstemens.Controllers;

import com.jfoenix.controls.JFXButton;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import org.avans.VTSOa.deslimstemens.Enums.GameStatus;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;
import org.avans.VTSOa.deslimstemens.Models.Database.Game;
import org.avans.VTSOa.deslimstemens.Models.Table.GameListObserver;
import org.avans.VTSOa.deslimstemens.Repositories.GameRepository;
import org.avans.VTSOa.deslimstemens.Views.ObserverView;

import java.sql.SQLException;
import java.util.List;

public class ObserverController extends SceneController {

    ObserverView view;

    public ObserverController(){
        this.view = new ObserverView(this);

        new Thread(() -> {
            ObservableList<GameListObserver> games = getDataList();
            this.view.tvObserver.setItems(games);
        }).start();
    }

    public List<Game> getData() {
        try {
            return GameRepository.getGamesforObserver();
        } catch (SQLException e) {
            System.out.println("Error retrieving games");
            e.printStackTrace();
        }
        return null;
    }

    public ObservableList<GameListObserver> getDataList(){
        List<Game> list = getData();

        ObservableList<GameListObserver> data = FXCollections.observableArrayList();

        list.stream().filter(game -> game.getGameStatus().equals(GameStatus.Ended)).forEach(g -> {
            JFXButton button = UI.Button(String.format("Bekijk spel %s", g.getId()));
            button.setOnAction(event -> System.out.println("Clicked")); // TODO go in observer mode

            data.add(new GameListObserver(g.getPlayer1(), g.getPlayer2(), button));
        });
        return data;
    }

    @Override
    public SceneView getView() {
        return view;
    }

    @Override
    public String getName() {
        return "Observer";
    }
}
