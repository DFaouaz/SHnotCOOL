using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantCharacter : MonoBehaviour {

	public static PersistantCharacter instance = null;
	public Shoping[] shops;
	public NegroBehaviour negro;
	NPC [] npcs;

	void Awake() {
		if (instance == null) {
			instance = this;
			npcs = GetComponentsInChildren<NPC> ();
			DontDestroyOnLoad(this.gameObject);
			UpdateShops ();
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
		UpdateShops ();
		UpdateNPCs ();
	}
	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void UpdateShops(){
		foreach (Shoping i in shops) {
			i.ActualizaPersonaje ();
		}
	}
	void UpdateNPCs(){
		foreach (NPC i in npcs) {
			if (i.nombrePersonaje == "Negro")
				negro.UpdateNegro ();
			else
				i.UpdateNPCs ();
		}
	}
}
