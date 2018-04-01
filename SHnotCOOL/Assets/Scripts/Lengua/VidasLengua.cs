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
		if (vidas > 0) {
			vidas--;
			ActualizaVidas ();
		} else if (vidas == 0) {
			vidas--;
			GameManager.instance.FinExamenLengua ();
		}
	}
	public void SubeVida(){
		vidas++;
		ActualizaVidas ();
	}

	void ActualizaVidas(){
		texto.text = "Vidas: " + vidas;
	}
}
