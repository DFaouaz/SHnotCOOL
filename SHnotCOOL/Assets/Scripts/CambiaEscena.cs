using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("Entrada");
        if (col.tag == "Player" && GameManager.instance.matematicasScore == 0)
        {
			GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
			SceneManager.LoadScene (escenaACambiar);
		}
	}
    
}
