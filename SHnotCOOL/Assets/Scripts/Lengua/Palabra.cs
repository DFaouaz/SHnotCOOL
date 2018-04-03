using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TipoPalabra{
	


}

public class Palabra : MonoBehaviour {

	StreamReader entrada;
	Text text;
	[HideInInspector]
	public string palabraElegida;
	[HideInInspector]
	public enum Modo{Tapado, Destapado}
	[HideInInspector]
	public Modo[] caracteres;

	void Start () {
		text = GetComponent<Text> ();
		EligePalabra ();
		ActualizaPalabra ();
	}

	public void Inicializa(){
		caracteres = new Modo[palabraElegida.Length];
		for (int i = 0; i < palabraElegida.Length; i++)
			caracteres [i] = Modo.Tapado;
	}

	void EligePalabra(){
		entrada = new StreamReader ("Palabras.txt");
		string [] palabras = entrada.ReadToEnd ().Split ('.');
		palabraElegida = palabras [Random.Range (0, palabras.Length)].ToUpper();
		Inicializa ();
	}

	public void ActualizaPalabra(){
		text.text = " ";
		for (int i = 0; i < palabraElegida.Length; i++) {
			if (caracteres [i] == Modo.Tapado)
				text.text += "_ ";
			else
				text.text += palabraElegida [i] + " ";
		}
	}
	public void Destapa(char letra){
		for (int i = 0; i < palabraElegida.Length; i++)
			if(palabraElegida[i].ToString() == letra.ToString().ToUpper())
				caracteres [i] = Modo.Destapado;
		ActualizaPalabra ();
		if (IsWordComplete ())
			GameManager.instance.FinExamenLengua ();
	}

	bool IsWordComplete(){
		int i = 0;
		while (i < text.text.Length && text.text [i] != '_')
			i++;
		return i < text.text.Length;
	}
}
