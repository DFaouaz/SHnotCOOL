using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoping : MonoBehaviour {
	[SerializeField]
	ObjetoEnVenta [] objetos_en_venta;
	public CanvasRenderer tienda_;


	public Text dialogueMensaje;
	public KeyCode botonInteraccion;
	bool collisionn;
	// Use this for initialization
	void Start () {
		dialogueMensaje.gameObject.SetActive (false);
		dialogueMensaje.text = "Pulsar " + botonInteraccion.ToString () + " para interactuar.";
		tienda_.gameObject.SetActive (false);

	}
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
		    dialogueMensaje.gameObject.SetActive (true);
			collisionn = true;
		}
	}
	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			dialogueMensaje.gameObject.SetActive (false);
			collisionn = false;
		}
	}
	void CheckInputDialogue(){
		if (Input.GetKeyDown (botonInteraccion) && collisionn) {
			tienda_.gameObject.SetActive (true);
			dialogueMensaje.gameObject.SetActive (false);
			GameManager.instance.ventanaAbierta = true;
		} else if(!collisionn)
			tienda_.gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		CheckInputDialogue ();
	}
	public void Exit()
	{
		tienda_.gameObject.SetActive (false);
		GameManager.instance.ventanaAbierta = false;
	}

}
