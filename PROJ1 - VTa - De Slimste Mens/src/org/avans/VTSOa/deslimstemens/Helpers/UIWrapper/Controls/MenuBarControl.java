package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls;

import com.jfoenix.controls.JFXButton;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Node;
import javafx.scene.control.Label;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.MissingException;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Navigator;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.UI;

/**
 * Created by Mike on 4-1-2016.
 */
public class MenuBarControl extends BorderPane {

	private Label _menuBarTitle;
	private JFXButton _menuBarBack;
	private StackPane menu = new StackPane();
	private boolean goBackEnabled = true;

	public MenuBarControl(EventHandler<MouseEvent> backButtonEvent){
		init(backButtonEvent);
	}

	public MenuBarControl(Node content, EventHandler<MouseEvent> backButtonEvent){
		init(content, backButtonEvent);
	}

	private void init(Node content, EventHandler<MouseEvent> backEvent){
		init(backEvent);
		setContent(content);
	}

	private void init(EventHandler<MouseEvent> backEvent){
		StackPane menuBar = createMenuBar();
		setOnBackButtonClicked(backEvent);
		this.setTop(menuBar);
	}

	private StackPane createMenuBar(){
		this.menu.setPrefHeight(64);
		this.menu.setBackground(new Background(new BackgroundFill(Color.DARKGRAY, null, null)));

		_menuBarTitle = new Label();
		_menuBarTitle.setStyle("-fx-font-size: 24px");
		_menuBarTitle.setTextFill(Color.WHITE);

		redrawBackButton();

		this.menu.getChildren().addAll(_menuBarTitle);
		StackPane.setAlignment(_menuBarTitle, Pos.CENTER_LEFT);
		StackPane.setMargin(_menuBarTitle, new Insets(0, 0, 0, 92));

		return menu;
	}

	private void redrawBackButton(){
		if(this.goBackEnabled){
			_menuBarBack = UI.Button("<-");
			_menuBarBack.setTextFill(Color.WHITE);
			_menuBarBack.setStyle("-fx-font-size: 24px");
			this.menu.getChildren().add(_menuBarBack);
			StackPane.setAlignment(_menuBarBack, Pos.CENTER_LEFT);
			StackPane.setMargin(_menuBarBack, new Insets(0, 0, 0, 16));
		}
		else{
			this.menu.getChildren().remove(_menuBarBack);
		}
	}

	private void setOnBackButtonClicked(EventHandler<MouseEvent> event){
		_menuBarBack.setOnMouseClicked(event);
	}

	public void setTitle(String title){
		_menuBarTitle.setText("De Slimste Mens - " + title);
	}

	public void setContent(Node content){
		this.setCenter(content);
	}

	public void setGoBackButton(boolean isEnabled){
		this.goBackEnabled = isEnabled;
		redrawBackButton();
	}

}