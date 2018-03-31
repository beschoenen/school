package org.avans.VTSOa.deslimstemens.Views;

import javafx.geometry.Insets;
import javafx.scene.Parent;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.VBox;
import javafx.scene.text.Font;
import javafx.util.Callback;
import org.avans.VTSOa.deslimstemens.Controllers.RankingController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Models.Database.Ranking;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class RankingView extends SceneView {
    private RankingController controller;

    private SplitPane spMain = new SplitPane();
    private VBox vbOwn = new VBox();
    public TableView tvRanking = new TableView();

    public RankingView(RankingController controller){
        this.controller = controller;
        this.setMenuBarTitle("Ranking");
    }

    @Override
    public void initialize() {
        spMain.setDividerPosition(0, 0.22);
        vbOwn.maxWidthProperty().bind(spMain.widthProperty().multiply(0.22));
        vbOwn.minWidthProperty().bind(spMain.widthProperty().multiply(0.22));

        tvRanking.setEditable(false);
        tvRanking.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);

        TableColumn rank = new TableColumn("#");
        rank.setCellFactory( new Callback<TableColumn, TableCell>() {
            @Override
            public TableCell call( TableColumn p ) {
                return new TableCell() {
                    @Override
                    public void updateItem(Object item, boolean empty) {
                        super.updateItem(item, empty);
                        setGraphic(null);
                        setText(empty ? null : getIndex() + 1 + "");
                    }
                };
            }
        });

        TableColumn playerName = new TableColumn("Speler");
        playerName.setCellValueFactory(new PropertyValueFactory<>("player"));

        TableColumn totalGames = new TableColumn("Aantal gespeelde spellen");
        totalGames.setCellValueFactory(new PropertyValueFactory<>("playedGames"));

        TableColumn won = new TableColumn("Aantal gewonnen");
        won.setCellValueFactory(new PropertyValueFactory<>("wonGames"));

        TableColumn lost = new TableColumn("Aantal verloren");
        lost.setCellValueFactory(new PropertyValueFactory<>("lostGames"));

        TableColumn seconds = new TableColumn("Aantal seconden over");
        seconds.setCellValueFactory(new PropertyValueFactory<>("averageSecondsLeft"));

        tvRanking.getColumns().addAll(rank, playerName, totalGames, won, lost, seconds);

        spMain.getItems().addAll(vbOwn, tvRanking);
    }

    public void renderOwnRanking(Ranking ranking) {
        List<Label> labels = new ArrayList<>();

        Label heading = new Label("Eigen statistieken");
        heading.setFont(new Font("Arial", 24));
        labels.add(heading);

        Arrays.asList(
                ranking.getPlayer(),
                "Aantal gespeeld: " + ranking.getPlayedGames(),
                "Aantal gewonnen: " + ranking.getWonGames(),
                "Aantal verloren: " + ranking.getLostGames(),
                "Aantal seconden over: " + ranking.getAverageSecondsLeft()
        ).forEach(s -> {
            Label label = new Label(s);
            label.setFont(new Font("Arial", 16));
            label.setMaxWidth(300);
            labels.add(label);
        });
        vbOwn.setPadding(new Insets(10));
        vbOwn.getChildren().addAll(labels);
    }

    @Override
    public Parent getRoot() {
        return this.spMain;
    }
}
