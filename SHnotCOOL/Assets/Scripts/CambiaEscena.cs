using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("Entrada");
		if (col.tag == "Player" && (escenaACambiar == "Matematicas" && GameManager.instance.matematicasScore == 0)|| (escenaACambiar == "Lengua" && GameManager.instance.lenguaScore == 0) || (escenaACambiar == "Historia" && GameManager.instance.historiaScore == 0) ) {		//ES MA PROVISIONAL
			GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
			SceneManager.LoadScene (escenaACambiar);
		}
	}
    
}
