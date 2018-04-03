using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour {

	[HideInInspector]
	public char letra;
	TextMesh caracter;
	public Color colorCorrecto;
	bool enPalabra = false;

	void Start(){
		caracter = GetComponent<TextMesh>();
		caracter.text = letra.ToString();
		if (BuscaLetra ()) {
			caracter.color = colorCorrecto;
			enPalabra = true;
		}
	}

	void Update(){
		CheckLetter ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "DeathZone") {
			if (enPalabra) {
				Destroy (this.gameObject);
				GameManager.instance.Destapa(letra);
			}
			else{			
				Destroy (this.gameObject);
				GameManager.instance.BajaVidaLengua ();
			}
		}
	}

	void CheckLetter(){
		if (Input.inputString.ToLower() == letra.ToString ()) {
			Destroy (this.gameObject);
			GameManager.instance.SubePuntosLengua ();
		}
	}
	public bool BuscaLetra(){
		int i = 0;
		Palabra pal = FindObjectOfType<Palabra> ();
		while (i < pal.palabraElegida.Length && letra.ToString() != pal.palabraElegida[i].ToString().ToLower())
			i++;
		return i < pal.palabraElegida.Length;
	}

}
