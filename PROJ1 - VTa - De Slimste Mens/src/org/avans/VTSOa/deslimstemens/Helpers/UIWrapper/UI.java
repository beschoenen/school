package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper;

import com.jfoenix.controls.*;
import com.jfoenix.transitions.hamburger.HamburgerBasicCloseTransition;
import javafx.geometry.Insets;
import javafx.geometry.Orientation;
import javafx.scene.Node;
import javafx.scene.control.Tab;
import javafx.scene.control.ToggleGroup;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Enums.FAB;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.ListControl;

public class UI {

    public static JFXButton Button(String text) {
        JFXButton button = new JFXButton(text);
        button.getStyleClass().add("default-button");
        return button;
    }

    public static JFXButton FAB(FAB type) {
        JFXButton button = new JFXButton();
        button.getStyleClass().addAll("raised", "fab");
	    switch (type){
		    case ADD:
			    button.getStyleClass().add("add-black");
			    break;
		    case EDIT:
			    button.getStyleClass().add("edit-black");
			    break;
	    }
        return button;
    }

    public static JFXCheckBox CheckBox(String text) {
        return CheckBox(text, Color.GREEN);
    }

    public static JFXCheckBox CheckBox(String text, Color color) {
        JFXCheckBox checkBox = new JFXCheckBox(text);
        checkBox.setCheckedColor(color);
        return checkBox;
    }

    public static JFXComboBox ComboBox(String[] selection) {
        return ComboBox("Please select an item from the list", selection);
    }

    public static JFXComboBox ComboBox(String promptText, String[] selection) {
        JFXComboBox comboBox = new JFXComboBox();
        comboBox.setPromptText(promptText);
        comboBox.getItems().addAll(selection);
        return comboBox;
    }

    public static JFXHamburger MenuButton() {
        JFXHamburger menu = new JFXHamburger();
        menu.getStyleClass().addAll("hamburger");
        HamburgerBasicCloseTransition burgerTask = new HamburgerBasicCloseTransition(menu);
        burgerTask.setRate(-1);
        menu.addEventHandler(MouseEvent.MOUSE_PRESSED, (e) -> {
            burgerTask.setRate(burgerTask.getRate() * -1);
            burgerTask.play();
        });
        return menu;
    }

    public static JFXHamburger MenuButton(Color color) {
        JFXHamburger menu = MenuButton();
        for (Node node : menu.getChildren()) {
            if (node instanceof StackPane) {
                ((StackPane) node).setBackground(new Background(new BackgroundFill(color, null, null)));
            }
        }
        return menu;
    }

    public static ListControl ListView() {
        return new ListControl();
    }

    public static ListControl ListView(String[] listItems) {
        ListControl list = new ListControl();
        list.addItems(listItems);
        return list;
    }

    public static JFXTextField TextField() {
        return TextField("");
    }

    public static JFXTextField TextField(String promptText) {
        JFXTextField textField = new JFXTextField();
        textField.setPromptText(promptText);
        return textField;
    }

    public static JFXPasswordField PasswordField() {
        return PasswordField("");
    }

    public static JFXPasswordField PasswordField(String promptText) {
        JFXPasswordField passwordField = new JFXPasswordField();
        passwordField.setPromptText(promptText);
        return passwordField;
    }

    public static JFXProgressBar ProgressBar() {
        JFXProgressBar progressBar = new JFXProgressBar();
        progressBar.setPrefWidth(300);
        return progressBar;
    }

    public static JFXProgressBar ProgressBar(boolean isIndeterminate) {
        JFXProgressBar progressBar = ProgressBar();
        progressBar.setProgress(-1.0f);
        return progressBar;
    }

    public static JFXProgressBar ProgressBar(double progress) {
        JFXProgressBar progressBar = ProgressBar();
        progressBar.setProgress(progress);
        return progressBar;
    }

    public static JFXRadioButton RadioButton(String text) {
        JFXRadioButton radioButton = new JFXRadioButton();
        radioButton.setPadding(new Insets(10));
        radioButton.setText(text);
        return radioButton;
    }

    public static ToggleGroup RadioButtonGroup(JFXRadioButton... radioButtons) {
        ToggleGroup group = new ToggleGroup();
        group.getToggles().addAll(radioButtons);
        return group;
    }

    public static ToggleGroup RadioButtonGroup(String... radioButtonTexts) {
        ToggleGroup group = new ToggleGroup();
        for(String radioButtonText : radioButtonTexts) {
            group.getToggles().add(RadioButton(radioButtonText));
        }
        return group;
    }

    public static JFXSlider Slider() {
        return Slider(Orientation.HORIZONTAL, JFXSlider.IndicatorPosition.LEFT);
    }

    public static JFXSlider Slider(Orientation orientation, JFXSlider.IndicatorPosition indicatorPosition) {
        JFXSlider slider = new JFXSlider();
        slider.setOrientation(orientation);
        slider.setIndicatorPosition(indicatorPosition);
        return slider;
    }

    public static JFXSpinner Spinner() {
        return new JFXSpinner();
    }

    public static Tab Tab(String text) {
        return new Tab(text);
    }

    public static JFXTabPane TabPanel(Tab... tabs) {
        JFXTabPane tabPane = new JFXTabPane();
        tabPane.getTabs().addAll(tabs);
        return tabPane;
    }

    public static JFXToggleButton ToggleButton() {
        return new JFXToggleButton();
    }

    public static JFXToggleNode ToggleImage(String sourceFilePath) {
        JFXToggleNode toggleButton = new JFXToggleNode();
        ImageView view = new ImageView();
        Image image = new Image(sourceFilePath);
        view.setImage(image);
        toggleButton.setGraphic(view);
        return toggleButton;
    }
}
