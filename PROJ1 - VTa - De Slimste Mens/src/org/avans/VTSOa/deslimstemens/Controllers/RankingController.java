package org.avans.VTSOa.deslimstemens.Controllers;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Ranking;
import org.avans.VTSOa.deslimstemens.Repositories.RankingRepository;
import org.avans.VTSOa.deslimstemens.RunTimeSettings;
import org.avans.VTSOa.deslimstemens.Views.RankingView;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class RankingController extends SceneController {

    private RankingView view;

    public RankingController(){
        view = new RankingView(this);

        new Thread(() -> {
            Ranking ranking = this.findOwn();
            Platform.runLater(() -> this.view.renderOwnRanking(ranking));

            this.view.tvRanking.setItems(getData());
            Platform.runLater(() -> this.view.tvRanking.refresh());
        }).start();
    }

    private List<Ranking> findAllUsers() {
        try {
            return RankingRepository.getAllRankings();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return new ArrayList<>();
    }

    private Ranking findOwn() {
        try {
           return RankingRepository.getRanking(RunTimeSettings.user.getName());
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    private ObservableList<Ranking> getData() {
        ObservableList<Ranking> data = FXCollections.observableArrayList();
        findAllUsers().forEach(data::addAll);
        return data;
    }

    @Override
    public SceneView getView() {
        return view;
    }

    @Override
    public String getName() {
        return "Ranking";
    }
}
