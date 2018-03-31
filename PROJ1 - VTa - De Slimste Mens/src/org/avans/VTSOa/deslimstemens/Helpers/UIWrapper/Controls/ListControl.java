package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import com.jfoenix.controls.JFXListView;
import com.jfoenix.controls.JFXSpinner;
import javafx.scene.Node;
import javafx.scene.layout.StackPane;

public class ListControl extends StackPane {
    private JFXSpinner spinner = new JFXSpinner();
    private JFXListView listView = new JFXListView();

    public ListControl() {
        this.getChildren().addAll(this.listView, this.spinner);
    }

    public void addItem(String item) {
        this.listView.getItems().add(item);
        this.getChildren().remove(this.spinner);
    }

    public void addItems(String[] items) {
        this.listView.getItems().addAll(items);
        this.getChildren().remove(this.spinner);
    }

    public void removeItem(Node item) {
        this.listView.getItems().remove(item);
    }
}
