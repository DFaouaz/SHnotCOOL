using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour {

	Text texto;
	public int puntosParaAprobar;
	public int puntosPorLetra;

	void Start(){
		texto = GetComponent<Text> ();
		ActualizaPuntos ();
	}
	public void SubePuntos(){
		if (GameManager.instance.lenguaScore < puntosParaAprobar) {
			GameManager.instance.lenguaScore += puntosPorLetra;
			PuntosPorVida ();
			ActualizaPuntos ();
			if (CompruebaAprobado ())
				GameManager.instance.FinExamenLengua ();
		}
	}
	void ActualizaPuntos(){
		texto.text = "Puntos: " + GameManager.instance.lenguaScore;
	}
	bool CompruebaAprobado(){
		return (GameManager.instance.lenguaScore >= puntosParaAprobar);
	}
	void PuntosPorVida(){
		if (GameManager.instance.lenguaScore % 5 == 0)
			FindObjectOfType<VidasLengua> ().SubeVida ();
	}
}
