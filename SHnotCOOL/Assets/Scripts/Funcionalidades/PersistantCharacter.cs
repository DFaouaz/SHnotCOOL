using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantCharacter : MonoBehaviour {

	public static PersistantCharacter instance = null;
	Shoping[] objs;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			objs = GetComponentsInChildren<Shoping> ();
			ActualizaObjetos ();
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
		ActualizaObjetos ();
	}
	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void ActualizaObjetos(){
		foreach (Shoping i in objs) {
			i.ActualizaPersonaje ();
		}
	}
}
