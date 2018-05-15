﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public struct opcion{
	public string frase;
	public int valor;
}

public class AnswerManager : MonoBehaviour {

	public static AnswerManager instance = null;
    public Text nombreEnemigo;

    float danio=0;
	opcion[] respuestas;
	List<opcion> opciones;
	opcion [] Buenas;
	opcion [] Malas;
	bool pulsado;
    bool quitadaVida=false;
    

	void Start () {
		if (instance == null)
			instance = this;

        string fraseBuena;
        string[] lineasBuenas;
        string fraseMala;
        string[] lineasMalas;
        StreamReader texto2= new StreamReader("MalasJisus.txt", Encoding.Default);
        StreamReader texto= new StreamReader("BuenasJisus.txt", Encoding.Default); ;
        pulsado = false;
        switch(GameManager.instance.trimestre)
        {
            case (1):
                texto = new StreamReader("BuenasTrump.txt", Encoding.Default);
                texto2 = new StreamReader("MalasTrump.txt", Encoding.Default);
                nombreEnemigo.text = "Trump";
                break;
            case (2):
                texto = new StreamReader("buena.txt", Encoding.Default);
                texto2 = new StreamReader("mala.txt", Encoding.Default);
                nombreEnemigo.text = "Hitler";
                break;
            case (3):
                texto = new StreamReader("BuenasJisus.txt", Encoding.Default);
                texto2 = new StreamReader("MalasJisus.txt", Encoding.Default);
                nombreEnemigo.text = "Jesus";
                break;
        }

		opciones = new List<opcion> ();
		Buenas = new opcion[10];
		Malas = new opcion[30];
		respuestas = new opcion[4];
		int i = 0;
        while (i < Buenas.Length)
        {
            fraseBuena = texto.ReadLine();
            lineasBuenas = fraseBuena.Split(' ');
            Buenas[i].valor = int.Parse(lineasBuenas[0]);
            for (int k = 1; k < lineasBuenas.Length; k++)
            {
                Buenas[i].frase += ' ';
                Buenas[i].frase += lineasBuenas[k];
            }
            i++;
        }
        int l = 0;
        while (l<Malas.Length) { 
            fraseMala = texto2.ReadLine();
            lineasMalas = fraseMala.Split(' ');
            Malas[l].valor = int.Parse(lineasMalas[0]);
            for (int j = 1; j < lineasMalas.Length; j++)
            {
                Malas[l].frase += ' ';
                Malas[l].frase += lineasMalas[j];               
            }
			l++;
		}
		InitButtons ();
	}
	

	public void InitButtons()
	{

		int x, y, w, z;
		x = Random.Range (0, Buenas.Length);
		y = Random.Range (0, Malas.Length);
		w = Random.Range (0, Malas.Length);
		z = Random.Range (0,Malas.Length);

		while(w==y)
			w = Random.Range (0, Buenas.Length);
		while(w==z||z==y)
			z = Random.Range (0,Malas.Length);

		opciones.Add (Buenas [x]);
		opciones.Add (Malas [y]);
		opciones.Add (Malas [w]);
		opciones.Add (Malas [z]);
		int contador = 0;
		while (opciones.Count > 0) {
			int posicion = Random.Range (0, opciones.Count);
			respuestas[contador]=opciones [posicion];
			opciones.RemoveAt (posicion);
			contador++;
		}
        quitadaVida = false;

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
    public void setDanio(int damage)
    {
        danio = damage;
    }
    public float getDanio()
    {
        return danio;
    }
    public bool getQuitadaVida()
    {
        return quitadaVida;
    }
    public void setQuitadaVida(bool ok)
    {
        quitadaVida = ok;
    }
}
