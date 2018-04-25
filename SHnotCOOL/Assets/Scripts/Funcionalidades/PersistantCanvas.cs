using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantCanvas : MonoBehaviour {

	static PersistantCanvas instance = null;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		} 
	}
}
