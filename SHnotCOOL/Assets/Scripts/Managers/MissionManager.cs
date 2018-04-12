using System.Collections;
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

	public void AñadirMision(NPC mision){
		Mission m = (Mission)mision;
		misiones.Add (m);
		AsignaMisiones ();
	}

	public void PasaPaginaDerecha(){
		if (misiones.Count > huecos.Count && misiones.Count / huecos.Count > paginaCont) {
			paginaCont++;
			AsignaMisiones ();
		}
	}

	public void PasaPaginaIzquierda(){
		if (paginaCont - 1 > 0) {
			paginaCont--;
			AsignaMisiones ();
		}
	}

	void AsignaMisiones(){
		for (int i = (huecos.Count * paginaCont) - huecos.Count; i < huecos.Count * paginaCont && i < misiones.Count; i++) {
			huecos [i].mision = misiones [i];
			huecos [i].UpdateLooking ();
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
