package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper;

/**
 * Created by Mike on 6-1-2016.
 */
public abstract class SceneController {

	/**
	 * Return the view this controller is controlling
	 * @return SceneView view
	 */
	public abstract SceneView getView();

	/**
	 * Return a name used to identify this controller and view
 	 * @return String name
	 */
	public abstract String getName();

	public void onEnter() {}

	public void onLeave() {}
}
