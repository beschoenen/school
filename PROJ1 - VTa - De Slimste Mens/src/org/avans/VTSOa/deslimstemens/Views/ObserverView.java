package org.avans.VTSOa.deslimstemens.Views;

import javafx.scene.Parent;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import org.avans.VTSOa.deslimstemens.Controllers.ObserverController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;

public class ObserverView extends SceneView {

    ObserverController ObserverController;

    public TableView tvObserver = new TableView();

    public ObserverView(ObserverController controller){
        this.ObserverController = controller;
        this.setMenuBarTitle("Hoofdmenu");
    }

    @Override
    public void goBack() {
        super.goBack();
    }

    @Override
    public void initialize() {
        tvObserver.setEditable(false);
        tvObserver.setColumnResizePolicy(TableView.CONSTRAINED_RESIZE_POLICY);

        TableColumn tcChallenger = new TableColumn("Uitdager");
        tcChallenger.setCellValueFactory(new PropertyValueFactory<>("challenger"));

        TableColumn tcOpponent = new TableColumn("Tegenspeler");
        tcOpponent.setCellValueFactory(new PropertyValueFactory<>("opponent"));

        TableColumn tcButton = new TableColumn("Observe");
        tcButton.setCellValueFactory(new PropertyValueFactory<>("button"));

        tvObserver.getColumns().addAll(tcChallenger, tcOpponent, tcButton);
    }

    @Override
    public Parent getRoot() {
        return this.tvObserver;
    }

    @Override
    public double getWidth(){
        return 450;
    }
}
