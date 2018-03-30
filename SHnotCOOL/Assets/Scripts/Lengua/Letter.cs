using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour {

	public char letra;
	TextMesh caracter;

	void Start(){
		caracter = GetComponent<TextMesh>();
		caracter.text = letra.ToString();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "DeathZone")
			Destroy (this.gameObject);
	}

}
