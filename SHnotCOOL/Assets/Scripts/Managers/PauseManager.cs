﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public GameObject pausaMenu;
	public string escenaMenuPrincipal;
	public GameObject botones;
	public Button [] botonesDelMenu;
	public CanvasRenderer panelOpciones;


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
				panelOpciones.gameObject.SetActive (false);
				botones.gameObject.SetActive (false);
				GameManager.instance.pauseMode = false;
				//Time.timeScale = 1;
				InputConfiguration.SelectFirstFoundButton ();
			}else {
				transform.SetAsLastSibling ();
				pausaMenu.SetActive (true);
				botones.SetActive (true);
				InputConfiguration.DeselectButton ();
				botonesDelMenu [0].Select ();
				GameManager.instance.pauseMode = true;
				//Time.timeScale = 0;
			}
		}
	}

	//Devuelve el tiempo al juego y quita el menu de pausa
	public void Continuar(){
		pausaMenu.SetActive (false);
		GameManager.instance.pauseMode = false;
		//Time.timeScale = 1;
	}

	//Sale al menu principal
	public void SalirMenuPrincipal(){
		SceneManager.LoadScene (escenaMenuPrincipal);
	}

	//Métodos para el panel de opciones
	public void AbreOpciones(){
		botones.SetActive (false);
		panelOpciones.gameObject.SetActive (true);
	}
	public void CierraOpciones(){
		botones.SetActive (true);
		botonesDelMenu [1].Select ();
		panelOpciones.gameObject.SetActive (false);
	}
}
