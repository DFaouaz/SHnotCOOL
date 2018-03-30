using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasLengua : MonoBehaviour {

	public int vidas;
	Text texto;

	void Start(){
		texto = GetComponent<Text> ();
		ActualizaVidas ();
	}
		

	public void BajaVida(){
		vidas--;
		if (vidas <= 0) {
			GameManager.instance.FinExamenLengua ();
			vidas = 0;
		}
		ActualizaVidas ();
	}
	public void SubeVida(){
		vidas++;
		ActualizaVidas ();
	}

	void ActualizaVidas(){
		texto.text = "Vidas: " + vidas;
	}
}
