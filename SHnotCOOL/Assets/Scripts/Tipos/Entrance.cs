using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	CambiaEscena ce;
	public string escenaACambiar;// Aula si es un examen
	public string examen;

	void Awake(){
		ce = GetComponentInParent<CambiaEscena> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			MuestraMensaje ();
			ce.entrada = this;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			ce.mensajeEscena.gameObject.SetActive (false);
			ce.entrada = null;
		}
	}

	void MuestraMensaje(){
		ce.mensajeEscena.gameObject.SetActive(true);
		if (escenaACambiar != "Aula")
			ce.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para acceder al\n" + escenaACambiar;
		else
			ce.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para acceder al\nAula de " + examen;
	}
}
