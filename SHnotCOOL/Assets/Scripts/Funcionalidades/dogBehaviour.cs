using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameManager.instance.perroComido)
			gameObject.SetActive (false);
	}
	

}
