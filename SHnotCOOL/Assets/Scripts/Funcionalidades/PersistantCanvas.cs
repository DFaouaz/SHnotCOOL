using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
