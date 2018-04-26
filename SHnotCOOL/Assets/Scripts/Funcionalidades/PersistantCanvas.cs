using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantCanvas : MonoBehaviour {

	static PersistantCanvas instance = null;
	public CanvasObject [] objs;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		} 
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		Debug.Log (scene.name);
		UpdateCanvasObjects ();
	}
	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void UpdateCanvasObjects(){
		foreach (CanvasObject i in objs) {
			i.UpdateCanvasObject ();
		}
	}
}
