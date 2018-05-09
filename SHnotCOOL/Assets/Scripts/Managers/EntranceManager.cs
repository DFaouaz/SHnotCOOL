using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntranceManager : MonoBehaviour {


	public Entrance entrance;
	[HideInInspector]
	public GameObject player,negro;

	public static EntranceManager instance = null;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy (this);
	}

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		negro = GameObject.FindGameObjectWithTag ("Negro");
	}

	void Update () {
		CheckInteraction ();
	}

	void CheckInteraction(){
		if (Input.GetKeyDown (GameManager.instance.botonInteractuar) && entrance != null) {			
			//Movemos al jugador
			entrance.MoveToConnection(player);
			//Movemos al negro
			Invoke("MoveNegro",0.25f);
		}
	}

	void MoveNegro(){
		if (player == null || negro == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			negro = GameObject.FindGameObjectWithTag ("Negro");
		}
		if(GameManager.instance.habladoNegro)
		negro.transform.position = player.transform.position;
	}
}
