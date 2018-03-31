package org.avans.VTSOa.deslimstemens.Views;

import com.jfoenix.controls.JFXButton;
import javafx.geometry.Pos;
import javafx.scene.Parent;
import javafx.scene.control.SplitPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Priority;
import javafx.scene.layout.StackPane;
import javafx.scene.layout.VBox;
import org.avans.VTSOa.deslimstemens.Controllers.StartController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;

public class StartView extends SceneView {
	VBox vbMain;
	StackPane spButtons;

	SplitPane spMain;

    public TableView tvGames = new TableView();
    public TableView tvPlayers = new TableView();

	public JFXButton btRankingView = UI.Button("Naar de statistieken");
	public JFXButton btAccount = UI.Button("Naar account");

    StartController controller;

	public StartView(StartController controller){
		this.controller = controller;
        this.setMenuBarTitle("Hoofdmenu");
	}

	@Override
	public void goBack() {
		super.goBack();
	}

	@Override
    public void initialize() {
		tvGames.setEditable(false);
		tvGames.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);

        TableColumn tcGameOpponent = new TableColumn("Tegenstander");
        tcGameOpponent.setCellValueFactory(new PropertyValueFactory<>("opponent"));
		tcGameOpponent.setSortable(false);

        TableColumn tcGameStatus = new TableColumn("Status");
        tcGameStatus.setCellValueFactory(new PropertyValueFactory<>("gameStatus"));
		tcGameStatus.setSortable(false);

        TableColumn tcGameTurn = new TableColumn("Beurt");
        tcGameTurn.setCellValueFactory(new PropertyValueFactory<>("button"));
		tcGameTurn.setSortable(false);

		tvGames.getColumns().addAll(tcGameOpponent, tcGameStatus, tcGameTurn);


		tvPlayers.setEditable(false);
		tvPlayers.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);

        TableColumn tcPlayersName = new TableColumn("Speler");
        tcPlayersName.setCellValueFactory(new PropertyValueFactory<>("name"));
		tcPlayersName.setSortable(false);

        TableColumn tcPlayersStatus = new TableColumn("Uitnodigen");
        tcPlayersStatus.setCellValueFactory(new PropertyValueFactory<>("button"));
		tcGameStatus.setSortable(false);

        tvPlayers.getColumns().addAll(tcPlayersName, tcPlayersStatus);


        spMain = new SplitPane(new StackPane(tvGames), new StackPane(tvPlayers));
		spMain.setDividerPosition(0, 0.65);

		VBox.setVgrow(spMain, Priority.ALWAYS);


		StackPane.setAlignment(btAccount, Pos.CENTER_RIGHT);
		StackPane.setAlignment(btRankingView, Pos.CENTER_LEFT);

		spButtons = new StackPane(btRankingView, btAccount);

        vbMain = new VBox(spMain, spButtons);
    }

	@Override
	public Parent getRoot() {
		return this.vbMain;
	}
}
