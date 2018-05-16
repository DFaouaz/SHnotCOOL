using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameManager : MonoBehaviour {


	void Start () {
		if (GameManager.instance != null) {			
			Destroy (GameManager.instance.gameObject);
			GameManager.instance = null;
		}
		Destroy (this.gameObject);
	}
}
