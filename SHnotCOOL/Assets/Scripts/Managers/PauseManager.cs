using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public GameObject pausaMenu;
	public string escenaMenuPrincipal;


	void Start(){
		pausaMenu.SetActive (false);
	}

	void Update(){
		CheckInputPause ();
	}

	void CheckInputPause(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (pausaMenu.activeInHierarchy) {
				pausaMenu.SetActive (false);
				GameManager.instance.pauseMode = false;
				Time.timeScale = 1;
			}
			else {
				pausaMenu.SetActive (true);
				GameManager.instance.pauseMode = true;
				Time.timeScale = 0;
			}
		}
	}

	//Devuelve el tiempo al juego y quita el menu de pausa
	public void Continuar(){
		pausaMenu.SetActive (false);
		Time.timeScale = 1;
	}

	//Sale al menu principal
	public void SalirMenuPrincipal(){
		SceneManager.LoadScene (escenaMenuPrincipal);
	}




}
