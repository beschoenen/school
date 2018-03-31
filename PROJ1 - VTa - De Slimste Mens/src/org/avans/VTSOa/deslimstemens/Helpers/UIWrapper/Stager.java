package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper;

import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.util.HashMap;

/**
 * Created by Mike on 5-12-2015.
 */
public class Stager {

	private Stage _stage;
	private HashMap<String, SceneController> _controllerCollection = new HashMap<>();
	private String _name;
	private String _currentScene = null;

	public Stager(Stage stage, String name) {
		_stage = stage;
		_name = name;
	}

	/**
	 * Add a controller to this stager to be shown
	 * @param controller controller extending UIWrapper.SceneController
	 */
	public void addSceneController(SceneController controller){
		_controllerCollection.put(controller.getName(), controller);
		controller.getView().initialize();
	}

	/**
	 * Show scene by given name
	 * @param sceneName controller and scene name
	 * @throws MissingException
	 */
	protected void showScene(String sceneName) throws MissingException {
		showScene(sceneName, false);
	}

	/**
	 * Show scene by given name as dialog. Needs to be closed before using any other windows/Stagers
	 * @param sceneName
	 * @param isDialog
	 * @throws MissingException
	 */
	protected void showScene(String sceneName, boolean isDialog) throws MissingException {
		if(this._currentScene != null) { getStoredController(this._currentScene).onLeave(); }
		Scene scene = getScene(sceneName);
		_stage.setScene(scene);
		show(isDialog);
	}

	private void show(boolean isDialog) throws MissingException {
		if(isDialog){
			_stage.showAndWait();
		}
		else{
			_stage.show();
		}
	}

	private Scene getScene(String sceneName) throws MissingException {
		this._currentScene = sceneName;
		SceneController controller = getStoredController(sceneName);
		controller.onEnter();
		SceneView view = controller.getView();
		Parent root = getRoot(view);
		Scene scene;
		if(root.getScene() != null){
			scene = root.getScene();
		}
		else{
			scene = new Scene(root, view.getWidth(), view.getHeight());
		}
		return scene;
	}

	private SceneController getStoredController(String name) throws MissingException {
		SceneController controller = _controllerCollection.get(name);
		if(controller != null){
			return controller;
		}
		else{
			throw new MissingException("Controller '"+name+"' not found");
		}
	}

	private Parent getRoot(SceneView view){
		Parent root;
		if(view.getMenuBar()){
			root = view.getMenuRoot();
		}
		else{
			root = view.getRoot();
		}
		return root;
	}

	/**
	 * Returns given name to the Stager
	 * @return
	 */
	public String getName() {
		return _name;
	}

	/**
	 * Set the title of the window
	 * @param title
	 */
	public void setTitle(String title) {
		_stage.setTitle(title);
	}
}