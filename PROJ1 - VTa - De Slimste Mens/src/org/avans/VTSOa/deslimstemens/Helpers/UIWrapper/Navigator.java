package org.avans.VTSOa.deslimstemens.Helpers.UIWrapper;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by Mike on 5-12-2015.
 */
public class Navigator {

	//collection with stages(windows)
	private static HashMap<String, Stager> _stageCollection = new HashMap<>();

	//collection holding the names of what controller was used. Navigator can go back to previous scene this way
	private static ArrayList<String> _sceneHistory = new ArrayList<String>();

	//Current stage and scene active
	private static Stager _currentStage = null;
	private static String _currentScene = null;

	/**
	 * Notify the Navigator of passed UIWrapper.Stager object
	 * Needs to be called only once!
	 * @param stager a UIWrapper.Stager object
	 */
	public static void registerStage(Stager stager) {
		_stageCollection.put(stager.getName(), stager);
	}

	/**
	 * Show a certain stage with a scene
	 * @param stageName given name of the registered UIWrapper.Stager object
	 * @param sceneName given name of the controller, controlling the view
	 * @throws MissingException
	 */
	public static void show(String stageName, String sceneName) throws MissingException {
		setHistory(sceneName);
		Stager stage = getStage(stageName);
		stage.setTitle("De Slimste Mens - VTSOa");
		stage.showScene(sceneName);
	}

	private static void setHistory(String name) {
		if(_currentScene == null){
			_currentScene = name;
		}
		else if(!_currentScene.equals(name)){
			_sceneHistory.add(_currentScene);
			_currentScene = name;
		}
	}

	private static Stager getStage(String stageName) throws MissingException {
		if(_currentStage == null){
			_currentStage = getStoredStager(stageName);
			return _currentStage;
		}
		else{
			if(_currentStage.getName().equals(stageName)){
				return _currentStage;
			}
			return getStoredStager(stageName);
		}
	}

	private static Stager getStoredStager(String stageName) throws MissingException {
		Stager stage = _stageCollection.get(stageName);
		if(stage != null){
			return stage;
		}
		else{
			throw new MissingException("Stage '"+stageName+"' not found");
		}
	}

	/**
	 * Open previously used scene
	 * @throws MissingException
	 */
	public static void previous() throws MissingException {
		int index = _sceneHistory.size() - 1;
		_currentScene = _sceneHistory.get(index);
		_sceneHistory.remove(index);
		_currentStage.showScene(_currentScene);
	}
}
