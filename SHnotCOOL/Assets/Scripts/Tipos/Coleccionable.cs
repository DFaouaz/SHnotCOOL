using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Coleccionable : MonoBehaviour {

	public string currentScene;
	public string NombreColeccionable;
	public Sprite imagenRepresentacion;
	HUDManager im;

	void Awake(){
		im = FindObjectOfType<HUDManager> ();
		//Visibilidad
		if (SceneManager.GetActiveScene ().name == currentScene)
			this.gameObject.SetActive (true);
		else
			this.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag =="Player")
			//Da referencia del objeto en el InventoryManager
			im.objeto = this;		
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag=="Player")
			im.objeto = null;
	}
}
