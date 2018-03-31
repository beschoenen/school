package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper;

import javafx.event.EventHandler;
import javafx.scene.Parent;
import javafx.scene.control.Button;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.FlowPane;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Controls.MenuBarControl;

/**
 * Created by Mike on 6-12-2015.
 */
public abstract class SceneView {

	private EventHandler<MouseEvent> _goBackEvent = event -> goBack();

	private boolean _hasMenuBar = true;
	private MenuBarControl _menuBar = new MenuBarControl(_goBackEvent);

	/**
	 * Basic root to add controls to
	 */
	public FlowPane _root = new FlowPane();

	/**
	 * Return a Double used to set the size of the windows width
	 * @return Double
	 */
	public double getWidth(){
		return 1000;
	}

	/**
	 * Return a Double used to set the size of the windows height
	 * @return Double
	 */
	public double getHeight(){
		return 500;
	}

	/**
	 * Enable or disable MenuBar from being shown
	 * @param isEnabled true for shown, false for hidden
	 */
	public final void setMenuBar(boolean isEnabled){
		_hasMenuBar = isEnabled;
	}

	/**
	 * Get if the MenuBar is shown or not
	 * @return Boolean true for shown, false for hidden
	 */
	public final boolean getMenuBar(){
		return _hasMenuBar;
	}

	/**
	 * Set title used in the MenuBar
	 * @param title title is auto-prefixed with 'De slimste Mens - '
	 */
	public final void setMenuBarTitle(String title){
		_menuBar.setTitle(title);
	}

	/**
	 * Switch visibility goBackButton
	 * @param isEnabled
	 */
	public final void setGoBackButton(boolean isEnabled){
		_menuBar.setGoBackButton(isEnabled);
	}

	private void goBackDefault(){
		try {
			Navigator.previous();
		} catch (MissingException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Override this method to add extra functionality to the back button for this view
	 * Always call super.goBack()
	 */
	public void goBack(){
		goBackDefault();
	}

	/**
	 * Method called to initialize the view given to the navigator
	 * Init controls for the views here
	 */
	public void initialize(){
		_root.getChildren().addAll(new Button("Default"));
	}

	/**
	 * Get the view with the MenuBar surrounding it
	 * @return MenuBarControl
	 */
	public final Parent getMenuRoot(){
		_menuBar.setContent(getRoot());
		return _menuBar;
	}

	/**
	 * Get the view without the MenuBar surrounding it
	 * @return Parent any form of Pane
	 */
	public Parent getRoot(){
		return _root;
	}
}
