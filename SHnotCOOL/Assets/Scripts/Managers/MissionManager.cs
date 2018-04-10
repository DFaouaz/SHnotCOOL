using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

	//Variables
	public KeyCode botonAbrirYCerrar;
	public GameObject missionBox;
	int paginaCont = 1;
	List<MissionSlot> misiones = new List<MissionSlot> ();




	public static MissionManager instance = null;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy (this);
	}

	void Start(){
		missionBox.SetActive (false);
	}

	void Update(){
		CheckInput ();
	}

	void CheckInput(){
		if (Input.GetKeyDown (botonAbrirYCerrar)) {
			if (!missionBox.activeInHierarchy && !GameManager.instance.ventanaAbierta) {
				missionBox.SetActive (true);
				GameManager.instance.ventanaAbierta = true;
			} else {
				GameManager.instance.ventanaAbierta = false;
				missionBox.SetActive (false);
			}
		}
	}

	void AñadirMision(){
		
	}
	void ActualizaVista (){
		//Dependiendo de la pagina
		
	}

	public void PasaPaginaDerecha(){
		int n = 8;	//Numero de misiones en una pagina
		if (misiones.Count > n && misiones.Count / 8 > paginaCont)
			paginaCont++;
	}
	public void PasaPaginaIzquierda(){
		if (paginaCont - 1 > 0)
			paginaCont--;
	}
		
}
