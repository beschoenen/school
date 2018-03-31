package org.avans.VTSOa.deslimstemens.Helpers;

import javafx.application.Application;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.text.Font;
import javafx.scene.text.Text;
import javafx.stage.Stage;

/**
 * Created by Mike on 27-1-2016.
 */
public class MeasureText extends Application {

	private String measureText;
	private Font font;
	private Text textObject;

	public MeasureText(String text, Font font){
		this.measureText = text;
		this.font = font;
		try {
			start(new Stage());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void start(Stage primaryStage) throws Exception {
		this.textObject = new Text(measureText);
		this.textObject.setFont(this.font);
		Scene scene = new Scene(new Group(this.textObject));
		primaryStage.setScene(scene);
		this.textObject.applyCss();
	}

	public double getTextWidth(){
		return this.textObject.getLayoutBounds().getWidth();
	}
}
