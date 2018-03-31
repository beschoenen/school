package org.avans.VTSOa.deslimstemens.Controllers;

import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.SceneView;
import org.avans.VTSOa.deslimstemens.Views.ControlTestView;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by Mike on 6-1-2016.
 */
public class TestController extends SceneController {

	//Zorg dat je controller deze       ^^^^^^^^   klasse extend

	String time = null;
	ControlTestView view;

	public TestController(){
		Date date = new Date();
		DateFormat format = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		time = format.format(date);

		view = new ControlTestView(time);

		//deze zet je de titel
		view.setMenuBarTitle(getName());

		//deze methode kun je de menubar uitzetten
		view.setMenuBar(true);
	}

	@Override
	public SceneView getView() {
		return view;
	}

	@Override
	public String getName() {
		return "Test";
	}
}
