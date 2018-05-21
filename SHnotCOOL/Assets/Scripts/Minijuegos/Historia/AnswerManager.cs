using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public struct Opcion
{
    public string frase;
    public int valor;
}

public class AnswerManager : MonoBehaviour {

    public static AnswerManager instance = null;
    public Text nombreEnemigo;

    float damage = 0;
    List<Opcion> opciones;
    Opcion[] buenas, malas, respuestas;
    bool pulsado, vidaRestada = false;

    void Start() {

        if (instance == null)
            instance = this;

        string fraseBuena, fraseMala;
        string[] lineasBuenas, lineasMalas;

        StreamReader textoMalas = new StreamReader("MalasTrump.txt", Encoding.UTF8);
        StreamReader textoBuenas = new StreamReader("BuenasTrump.txt", Encoding.UTF8);
        pulsado = false;

        switch (GameManager.instance.trimestre)
        {
            case (1):
                textoMalas = new StreamReader("MalasTrump.txt", Encoding.UTF8);
                textoBuenas = new StreamReader("BuenasTrump.txt", Encoding.UTF8);
                nombreEnemigo.text = "Trump";
                break;
            case (2):
                textoMalas = new StreamReader("MalasHitler.txt", Encoding.UTF8);
                textoBuenas = new StreamReader("BuenasHitler.txt", Encoding.UTF8);
                nombreEnemigo.text = "Hitler";
                break;
            case (3):
                textoMalas = new StreamReader("MalasJesus.txt", Encoding.UTF8);
                textoBuenas = new StreamReader("BuenasJesus.txt", Encoding.UTF8);
                nombreEnemigo.text = "Jesus";
                break;
        }

        opciones = new List<Opcion>();
        buenas = new Opcion[10];
        malas = new Opcion[30];
        respuestas = new Opcion[4];

        int i = 0;
        while (i < buenas.Length)
        {
            fraseBuena = textoBuenas.ReadLine();
            lineasBuenas = fraseBuena.Split(' ');
            buenas[i].valor = int.Parse(lineasBuenas[0]);

            for (int k = 1; k < lineasBuenas.Length; k++)
            {
                buenas[i].frase += ' ';
                buenas[i].frase += lineasBuenas[k];
            }
            i++;
        }

        int j = 0;
        while (j < malas.Length)
        {
            fraseMala = textoMalas.ReadLine();
            lineasMalas = fraseMala.Split(' ');
            malas[j].valor = int.Parse(lineasMalas[0]);

            for (int m = 1; m < lineasMalas.Length; m++)
            {
                malas[j].frase += ' ';
                malas[j].frase += lineasMalas[m];
            }
            j++;
        }
        InitButtons();
    }

    public void InitButtons()
    {
        int x, y, w, z;
        x = Random.Range(0, buenas.Length);
        y = Random.Range(0, malas.Length);
        w = Random.Range(0, malas.Length);
        z = Random.Range(0, malas.Length);

        while (w == y)
            w = Random.Range(0, buenas.Length);

        while (w == z || y == z)
            z = Random.Range(0, malas.Length);

        opciones.Add(buenas[x]);
        opciones.Add(malas[y]);
        opciones.Add(malas[w]);
        opciones.Add(malas[z]);

        int contador = 0;
        while (opciones.Count > 0)
        {
            int posicion = Random.Range(0, opciones.Count);

            respuestas[contador] = opciones[posicion];
            opciones.RemoveAt(posicion);
            contador++;
        }
        vidaRestada = false;
    }

    public Opcion GetOpcion(int index)
    {
        return respuestas[index];
    }

    public bool GetPulsado()
    {
        return pulsado;
    }

    public void SetPulsado(bool ok)
    {
        pulsado = ok;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(int damages)
    {
        damage = damages;
    }

    public bool GetVidaRestada()
    {
        return vidaRestada;
    }

    public void SetVidaRestada(bool ok)
    {
        vidaRestada = ok;
    }
}