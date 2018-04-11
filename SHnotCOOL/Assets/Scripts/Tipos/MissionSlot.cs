using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSlot : MonoBehaviour {

	public Text nombre, titulo, paso;
	[HideInInspector]
	public Mission mision;

	void Start(){
		VaciaSlot ();
	}

	public void UpdateLooking(){
		if (mision != null) {
			nombre.text = mision.nombrePersonaje;
			titulo.text = mision.tituloDeLaMision;
			paso.text = mision.pasos.ToArray () [0].lineaPaso;
		}else{
			VaciaSlot();
		}
	}
	
	public void VaciaSlot(){
		nombre.text = "";
		titulo.text = "";
		paso.text = "";
	}
		
}
