using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerPers : MonoBehaviour {

	public string scene;

	public void UpdateTrigger(){
		if (SceneManager.GetActiveScene ().name == scene)
			gameObject.SetActive (true);
		else
			gameObject.SetActive (false);
	}
}
