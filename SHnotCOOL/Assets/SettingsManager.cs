using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public AudioMixer mainMixer;
	public Dropdown listaCalidad, listaResoluciones;

	Resolution[] resoliciones;
	void Start(){
		resoliciones = Screen.resolutions;
		listaResoluciones.ClearOptions ();
		List<string> opciones = new List<string>();
		int indiceResolucionActual = 0;
		for (int i = 0; i < resoliciones.Length; i++) {
			string option = resoliciones[i].width + " x " + resoliciones[i].height;
			opciones.Add(option);

			if (resoliciones [i].width == Screen.currentResolution.width &&
			   resoliciones [i].height == Screen.currentResolution.height)
				indiceResolucionActual = i;
		}

		listaResoluciones.AddOptions (opciones);
		listaResoluciones.value = indiceResolucionActual;
		listaResoluciones.RefreshShownValue ();
		CambiaGraficos (QualitySettings.GetQualityLevel ());

	}

	//Métodos para el apartado de opciones
	public void CambiaVolumen(float volume){
		mainMixer.SetFloat ("volume", volume);
	}

	public void CambiaGraficos(int index){
		listaCalidad.value = index;
		listaCalidad.RefreshShownValue ();
		QualitySettings.SetQualityLevel (index);
	}

	public void CambiaAPantallaCompleta(bool pantCompleta){
		Screen.fullScreen = pantCompleta;
	}

	public void ActualizaResolucion(int indice){
		Resolution res = resoliciones [indice];
		Screen.SetResolution (res.width, res.height, Screen.fullScreen);
	}
}
