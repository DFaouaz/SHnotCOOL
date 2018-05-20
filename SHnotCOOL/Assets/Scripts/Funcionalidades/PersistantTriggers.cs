using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantTriggers : MonoBehaviour {

	public static PersistantTriggers instance;
	TriggerPers[] triggers;

	void Awake() {
		if (instance == null) {
			triggers = GetComponentsInChildren<TriggerPers> ();
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
			PersistantTriggers.instance = null;
			Destroy (this.gameObject);
		}
		else 
			UpdateTriggers ();
	}
	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void UpdateTriggers(){
		foreach (TriggerPers i in triggers) {
			if (i != null)
				i.UpdateTrigger ();
		}
	}


}
