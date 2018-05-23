using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour {

	Text texto;
	public int puntosParaAprobar;
	public int puntosPorLetra;
	int puntosLlevados;


	void Start() {

		texto = GetComponent<Text> ();
		ActualizaPuntos ();
		puntosLlevados = 0;
	
	}

    public void SubePuntos()
    {
        puntosLlevados += puntosPorLetra;
        PuntosPorVida();
        ActualizaPuntos();
    }

	void ActualizaPuntos()
    {
		if (FindObjectOfType<VidasLengua> ().vidas >= 5)
			GameManager.instance.lenguaScore = 10;
		else
			GameManager.instance.lenguaScore = FindObjectOfType<VidasLengua> ().vidas * 2;
		texto.text = "Puntos: " + GameManager.instance.lenguaScore;
	}

	void PuntosPorVida()
    {
		if (puntosLlevados % 10 == 0)
			FindObjectOfType<VidasLengua> ().SubeVida ();
	}
}