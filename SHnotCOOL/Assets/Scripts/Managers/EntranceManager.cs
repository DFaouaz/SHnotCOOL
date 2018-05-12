using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
			if (isPasillosTime ()) {
				GameManager.instance.lastEntrancePasillos = entrance.pos;
				GameManager.instance.lastEntranceName = entrance.entranceName;
				GameManager.instance.lastPosEntrance = entrance.entranceConnection.transform.position;
				SceneManager.LoadScene ("Pasillos");
			}else{
				//Movemos al jugador
				entrance.MoveToConnection(player);
				//Movemos al negro
				Invoke("MoveNegro",0.25f);
			}
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

	bool isPasillosTime(){
		if (entrance.isClass) {
			int num = Random.Range (0, 10);
			return num < 3;
		} else
			return false;
	}
}
