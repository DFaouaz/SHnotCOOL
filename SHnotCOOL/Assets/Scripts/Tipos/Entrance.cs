using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public string entranceName;
	public Entrance entranceConnection;
	[Header("Introduce el nombre de la escena del examen")]
	public string examSceneName;
	EntranceManager em;
	[Header("Marcar si el una salida")]
	public bool isExit;

	void Awake(){
		em = GetComponentInParent<EntranceManager>();
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" && !GameManager.instance.thereIsAnInteractiveEvent) {
			MuestraMensaje ();
			em.entrance = this;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			GameManager.instance.thereIsAnInteractiveEvent = false;
			em.mensajeEscena.gameObject.SetActive (false);
			em.entrance = null;
		}
	}

	void MuestraMensaje(){
		if (em.mensajeEscena != null) {
			em.mensajeEscena.gameObject.SetActive (true);
			GameManager.instance.thereIsAnInteractiveEvent = true;
			if (!isExit)
				em.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para acceder al\n" + entranceName;
			else
				em.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para salir de\n" + em.lastMovedEntrance.entranceName;
		}
	}

	public void MoveToConnection(GameObject character){
		if (isExit) 
			character.transform.position = em.lastMovedEntrance.gameObject.transform.position;
		else
			character.transform.position = entranceConnection.gameObject.transform.position; 
		em.lastMovedEntrance = this;
	}



}