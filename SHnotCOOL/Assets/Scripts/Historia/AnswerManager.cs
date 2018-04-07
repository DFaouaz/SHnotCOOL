using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public struct opcion{
	public string frase;
	public int valor;
}

public class AnswerManager : MonoBehaviour {

	public static AnswerManager instance = null;

	opcion[] respuestas;
	List<opcion> opciones;
	opcion [] Buenas;
	opcion [] Malas;
	bool pulsado;

	void Start () {
		if (instance == null)
			instance = this;
	
	     
		pulsado = false;
		StreamReader texto = new StreamReader("buena.txt", Encoding.Default);
		StreamReader texto2 = new StreamReader("mala.txt", Encoding.Default);
		opciones = new List<opcion> ();
		Buenas = new opcion[8];
		Malas = new opcion[8];
		respuestas = new opcion[4];
		int i = 0;
		while (i < Buenas.Length) {
			Buenas [i].valor = texto.Read ();
			Buenas [i].frase = texto.ReadLine ();
			Malas [i].valor = texto2.Read ();
			Malas [i].frase = texto2.ReadLine ();
			i++;
		}
		InitButtons ();
	}
	

	public void InitButtons()
	{

		int x, y, w, z;
		x = Random.Range (0, Buenas.Length);
		y = Random.Range (0, Buenas.Length);
		w = Random.Range (0, Malas.Length);
		z = Random.Range (0,Malas.Length);

		while(x==y)
			y = Random.Range (0, Buenas.Length);
		while(w==z)
			z = Random.Range (0,Malas.Length);

		opciones.Add (Buenas [x]);
		opciones.Add (Buenas [y]);
		opciones.Add (Malas [w]);
		opciones.Add (Malas [z]);
		int contador = 0;
		while (opciones.Count > 0) {
			int posicion = Random.Range (0, opciones.Count);
			respuestas[contador]=opciones [posicion];
			opciones.RemoveAt (posicion);
			contador++;
		}

	}

	public opcion getOpcion(int index)
	{
		return respuestas [index];
	}

	public bool getPulsado(){
		return pulsado;
	}
	public void setPulsado(bool ok)
	{
		pulsado = ok;
	}
}
