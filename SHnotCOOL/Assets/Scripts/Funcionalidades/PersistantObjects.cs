using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjects : MonoBehaviour {

	public static PersistantObjects instance = null;
	public bool updateObjs;
	Coleccionable[] objs;

	void Awake() {
		updateObjs = false;
		objs = GetComponentsInChildren<Coleccionable> ();
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		} 
	}

	void Update(){
		if (updateObjs)
			ActualizaObjetos ();
	}


	void ActualizaObjetos(){
		foreach (Coleccionable i in objs) {
			i.ActualizaObjeto ();
		}
		updateObjs = false;
	}
}
