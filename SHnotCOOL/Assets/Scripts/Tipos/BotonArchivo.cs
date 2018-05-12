using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BotonArchivo : MonoBehaviour {

	[HideInInspector]
	public Sprite imagen;
	[HideInInspector]
	public string nombre, fecha, hora, ruta;

	public void ReferenciaEnMenu(){
		//FindObjectOfType<MenuManager> ().botonRef = this;
	}

	public void ActualizaDatos(){
		Text [] textos = GetComponentsInChildren<Text> ();
		textos [0].text = nombre;
		textos [1].text = fecha;
		textos [2].text = hora;
	}

	public void BorrarArchivo(){
		File.Delete (ruta);
		FindObjectOfType<MenuManager> ().ReseteaLista ();
	}
}
