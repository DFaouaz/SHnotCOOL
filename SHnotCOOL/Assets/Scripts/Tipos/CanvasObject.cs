using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasObject : MonoBehaviour {

	public string [] scenes;

	public void UpdateCanvasObject(){
		if (inScene ())
			this.gameObject.SetActive (true);
		else
			this.gameObject.SetActive (false);
	}

	bool inScene(){
		int i = 0;
		string actualScene = SceneManager.GetActiveScene ().name;
		while (i < scenes.Length && scenes [i] != actualScene)
			i++;
		return i < scenes.Length;
	}
}
