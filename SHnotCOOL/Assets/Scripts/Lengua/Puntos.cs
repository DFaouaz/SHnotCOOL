using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour {

	Text texto;
	public int puntosPorLetra;

	void Start(){
		texto = GetComponent<Text> ();
		ActualizaPuntos ();
	}
	public void SubePuntos(){
		GameManager.instance.lenguaScore += puntosPorLetra;
		ActualizaPuntos ();
	}
	void ActualizaPuntos(){
		texto.text = "Puntos: " + GameManager.instance.lenguaScore;
	}
}
