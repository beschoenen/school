package org.avans.VTSOa.deslimstemens.Views;

import com.jfoenix.controls.JFXButton;
import javafx.scene.Parent;
import javafx.scene.control.ListView;
import javafx.scene.control.SplitPane;
import javafx.scene.layout.Priority;
import javafx.scene.layout.VBox;
import org.avans.VTSOa.deslimstemens.Controllers.LobbyController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;

public class LobbyView extends SceneView {
    VBox vbMain;
    SplitPane spMain;
    VBox vbChat;

    public ListView listViewGame = new ListView();
    public ListView listViewChat = new ListView();

    public JFXButton btGame = new JFXButton();

    LobbyController controller;

    public LobbyView(LobbyController controller){
        this.controller = controller;
        this.setMenuBarTitle("Lobby");

        this.btGame.setText("Loading...");
    }

    @Override
    public void goBack() {
        super.goBack();
    }

    @Override
    public void initialize() {
        vbChat = new VBox(listViewChat, controller.getChatControl().getView());
        vbChat.setMinWidth(250);

        spMain = new SplitPane(listViewGame, vbChat);
        spMain.setDividerPosition(0, 0.65);

        vbMain = new VBox(spMain, btGame);

        VBox.setVgrow(spMain, Priority.ALWAYS);
        VBox.setVgrow(listViewChat, Priority.ALWAYS);
    }

    @Override
    public Parent getRoot() {
        return this.vbMain;
    }

    @Override
    public double getWidth(){
        return 800;
    }
}
