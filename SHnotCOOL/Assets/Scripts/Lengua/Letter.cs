using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour {

	[HideInInspector]
	public char letra;
	TextMesh caracter;

	void Start(){
		caracter = GetComponent<TextMesh>();
		caracter.text = letra.ToString();
	}

	void Update(){
		CheckLetter ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "DeathZone") {
			Destroy (this.gameObject);
			GameManager.instance.BajaVidaLengua ();
		}
	}

	void CheckLetter(){
		if (Input.inputString.ToLower() == letra.ToString ()) {
			Destroy (this.gameObject);
			GameManager.instance.SubePuntosLengua ();
		}
	}

}
