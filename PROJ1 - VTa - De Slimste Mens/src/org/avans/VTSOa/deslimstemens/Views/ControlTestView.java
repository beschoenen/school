package org.avans.VTSOa.deslimstemens.Views;

import com.jfoenix.controls.*;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.fxml.FXML;
import javafx.geometry.Insets;
import javafx.geometry.Orientation;
import javafx.scene.control.Label;
import javafx.scene.control.Toggle;
import javafx.scene.control.ToggleGroup;
import javafx.scene.paint.Color;
import javafx.util.Duration;
import org.avans.VTSOa.deslimstemens.Enums.FAB;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.ListControl;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;

/**
 * Created by Mike on 7-12-2015.
 */
public class ControlTestView extends SceneView {

	String _time;

	public ControlTestView(String timestamp){
		_time = timestamp;
	}

	@Override
	public void goBack() {
		//override deze methode om de functionaliteit aan de back button in het menu toe te voegen
		//ALTIJD super.goBack(); callen anders gaat ie niet terug naar de vorige view
		super.goBack();
	}

	@Override
	public double getWidth() {
		return 1000;
	}

	@Override
	public void initialize() {
		JFXButton one = UI.Button(_time);
		JFXButton two = UI.FAB(FAB.EDIT);

		JFXCheckBox checkBox = UI.CheckBox("Does it work?", Color.AQUAMARINE);

		JFXComboBox comboBox = UI.ComboBox("Select a project member", new String[]{ "Mike", "Bas", "Patrick", "Rogier", "Kevin" });

		JFXHamburger menuButton = UI.MenuButton(Color.LIMEGREEN);

		ListControl emptyListView = UI.ListView();
		ListControl listView = UI.ListView(new String[] { "Mike", "Bas", "Patrick", "Rogier", "Kevin" });

		JFXTextField textField = UI.TextField("Type something here");

		JFXProgressBar progressBar = UI.ProgressBar();
		Timeline timeLine = new Timeline(
				new KeyFrame(Duration.ZERO, new KeyValue(progressBar.progressProperty(), 0)),
				new KeyFrame(Duration.seconds(2), new KeyValue(progressBar.progressProperty(), 1))
		);
		timeLine.setCycleCount(Timeline.INDEFINITE);
		timeLine.play();
		JFXProgressBar infProgressBar = UI.ProgressBar(true);

		Label radioButtonLabel = new Label();
		JFXRadioButton radioButton1 = UI.RadioButton("Radio Button");
		JFXRadioButton radioButton2 = UI.RadioButton("Click me!");
		ToggleGroup radioGroup = UI.RadioButtonGroup(
				radioButton1,
				radioButton2
		);
		radioGroup.selectedToggleProperty().addListener(new ChangeListener<Toggle>() {
			@FXML
			@Override
			public void changed(ObservableValue<? extends Toggle> observable, Toggle oldValue, Toggle newValue) {
				radioButtonLabel.setText(((JFXRadioButton)newValue).getText());
			}
		});

		JFXSlider sliderDefault = UI.Slider();
		JFXSlider slider = UI.Slider(Orientation.VERTICAL, JFXSlider.IndicatorPosition.RIGHT);

		JFXSpinner spinner = UI.Spinner();

		JFXTabPane tabPane = UI.TabPanel(
				UI.Tab("Nummer 1"),
				UI.Tab("Nummer 2")
		);

		JFXToggleButton toggleButton = UI.ToggleButton();

		JFXToggleNode toggleNode = UI.ToggleImage("/com/sun/javafx/scene/control/skin/modena/dialog-error.png");

		_root.setPadding(new Insets(20));
		_root.setOrientation(Orientation.VERTICAL);
		_root.setVgap(10);
		_root.setHgap(10);
		_root.getChildren().addAll(
				one,
				two,
				checkBox,
				comboBox,
				menuButton,
				emptyListView,
				listView,
				textField,
				progressBar,
				infProgressBar,
				radioButtonLabel,
				radioButton1,
				radioButton2,
				sliderDefault,
				slider,
				spinner,
				tabPane,
				toggleButton,
				toggleNode
		);
	}
}
