using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TipoPalabra{
	public string palabraElegida;

	public enum Modo{Tapado, Destapado}
	public Modo[] caracteres;

	public void Inicializa(){
		caracteres = new Modo[palabraElegida.Length];
		for (int i = 0; i < palabraElegida.Length; i++)
			caracteres [i] = Modo.Tapado;
	}
}

public class Palabra : MonoBehaviour {

	StreamReader entrada;
	Text text;
	[HideInInspector]
	public TipoPalabra palabraElegida;

	void Start () {
		palabraElegida = new TipoPalabra();
		text = GetComponent<Text> ();
		EligePalabra ();
		ActualizaPalabra ();
	}
	

	void EligePalabra(){
		entrada = new StreamReader ("Palabras.txt");
		string [] palabras = entrada.ReadToEnd ().Split ('.');
		palabraElegida.palabraElegida = palabras [Random.Range (0, palabras.Length)].ToUpper();
		palabraElegida.Inicializa ();
	}

	public void ActualizaPalabra(){
		text.text = " ";
		for (int i = 0; i < palabraElegida.palabraElegida.Length; i++) {
			if (palabraElegida.caracteres [i] == TipoPalabra.Modo.Tapado)
				text.text += "_ ";
			else
				text.text += palabraElegida.palabraElegida [i] + " ";
		}
	}
	public void Destapa(char letra){
		for (int i = 0; i < palabraElegida.palabraElegida.Length; i++)
			if(palabraElegida.palabraElegida[i].ToString() == letra.ToString().ToUpper())
				palabraElegida.caracteres [i] = TipoPalabra.Modo.Destapado;
		ActualizaPalabra ();
	}
}
