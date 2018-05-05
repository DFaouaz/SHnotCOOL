using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntranceManager : MonoBehaviour {


	public Entrance entrance;
	[HideInInspector]
	public Entrance lastMovedEntrance;
	public Text mensajeEscena;

	void Update () {
		CheckInteraction ();
	}

	void CheckInteraction(){
		if (Input.GetKeyDown (GameManager.instance.botonInteractuar) && entrance != null) {
			//Movemos al jugador
			entrance.MoveToConnection(GameObject.FindGameObjectWithTag("Player"));
			//Movemos al negro
			if(GameManager.instance.habladoNegro)
				entrance.MoveToConnection(GameObject.FindGameObjectWithTag("Negro"));
		}
	}

}
