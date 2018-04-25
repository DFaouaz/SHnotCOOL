﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantObjects : MonoBehaviour {

	public static PersistantObjects instance = null;
	public bool updateObjs;
	Coleccionable[] objs;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			updateObjs = false;
			objs = GetComponentsInChildren<Coleccionable> ();
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
		foreach (Coleccionable i in objs) {
			i.ActualizaObjeto ();
		}
	}
}