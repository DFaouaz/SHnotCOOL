using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasLengua : MonoBehaviour {

	public int vidas;
	Text texto;
	Puntos p;

	void Start(){

		texto = GetComponent<Text> ();
		p = FindObjectOfType<Puntos> ();
		ActualizaVidas ();
	}
		

	public void BajaVida()
    {
		if (vidas > 0)
        {
			vidas--;
			ActualizaVidas ();
		}
        else if (vidas == 0)
        {
			GameManager.instance.FinExamenLengua ();
		}
	}
	public void SubeVida()
    {
		vidas++;
		ActualizaVidas ();
	}

	void ActualizaVidas()
    {
		texto.text = "Vidas: " + vidas;
		p.ActualizaPuntos ();
	}
}