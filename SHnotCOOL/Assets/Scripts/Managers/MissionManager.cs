﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

	//Variables
	public KeyCode botonAbrirYCerrar;
	public GameObject missionBox;
	public Button izquierda, derecha;
	public List<MissionSlot> huecos;
	int paginaCont = 1;
	List<Mission> misiones = new List<Mission> ();




	public static MissionManager instance = null;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy (this);
	}

	void Start(){
		missionBox.SetActive (true);
		//Inicializacion
		foreach (MissionSlot i in huecos) {
			i.VaciaSlot ();
		}
		missionBox.SetActive (false);
	}

	void Update(){
		CheckInput ();
	}

	void CheckInput(){
		if (Input.GetKeyDown (botonAbrirYCerrar)) {
			if (!missionBox.activeInHierarchy && !GameManager.instance.ventanaAbierta && !GameManager.instance.pauseMode) {
				transform.SetAsLastSibling ();
				missionBox.SetActive (true);
				GameManager.instance.ventanaAbierta = true;
				derecha.Select ();
			} else if(missionBox.activeInHierarchy && !GameManager.instance.pauseMode){
				GameManager.instance.ventanaAbierta = false;
				InputConfiguration.DeselectButton ();
				missionBox.SetActive (false);
				paginaCont = 1;
			}
		}
	}

	public void AñadirMision(NPC mision){
		Mission m = (Mission)mision;
		misiones.Add (m);
		AsignaMisiones ();
	}

	public void PasaPaginaDerecha(){
		if (misiones.Count > huecos.Count && (float)misiones.Count / (float)huecos.Count > paginaCont) {
			paginaCont++;
			VaciaTodo ();
			AsignaMisiones ();
		}
	}

	public void PasaPaginaIzquierda(){
		if (paginaCont - 1 > 0) {
			paginaCont--;
			VaciaTodo ();
			AsignaMisiones ();
		}
	}

	void AsignaMisiones(){
		for (int i = (huecos.Count * paginaCont) - huecos.Count; i < huecos.Count * paginaCont && i < misiones.Count; i++) {
			huecos [i % huecos.Count].mision = misiones [i];
			huecos [i % huecos.Count].UpdateLooking ();
		}
	}

	public void EliminaMision(Mission mision){
		misiones.Remove (mision);
		VaciaTodo ();
		AsignaMisiones ();
	}

	void VaciaTodo(){
		for (int i = 0; i < huecos.Count; i++) {
			huecos [i].VaciaSlot ();
			huecos [i].mision = null;
		}
	}

	public void ActualizaPasos(Mission mision){
		for (int i = (huecos.Count * paginaCont) - huecos.Count; i < huecos.Count * paginaCont && i < misiones.Count; i++) {
			if (huecos [i].mision == mision) {
				huecos [i].mision = misiones [i];
				huecos [i].UpdateLooking ();
			}
		}
	}
}
