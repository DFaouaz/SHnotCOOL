﻿using System.Collections;
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
		if (SceneManager.GetActiveScene ().name == "MenuPrincipal") {
			PersistantCanvas.instance = null;
			DialogueManager.instance = null;
			MissionManager.instance = null;
			MessageManager.instance = null;
			EntranceManager.instance = null;
			Destroy (this.gameObject);
		} else {
			UpdateCanvasObjects ();
			try {
				GameManager.instance.UpdateExamsRender ();
			} catch {
				Debug.Log ("HUDMaganer gameobject desactivado en la jeranquía");
			}
			MessageManager.instance.CloseMessage ();
		}
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
