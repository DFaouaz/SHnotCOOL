using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    GameObject text, go, respawn;
    Text textoVidas;
    public int vidas = 5;

    void Start()
    {
        text = GameObject.FindGameObjectWithTag("Vidas");
        go = GameObject.FindGameObjectWithTag("Player");
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        textoVidas = text.GetComponent<Text>();
        ActualizaVidas();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
            go.transform.position = respawn.transform.position;
        }
    }

    void Death()
    {
        if (vidas > 0)
        {
            vidas--;
            ActualizaVidas();
        }
        if (vidas == 0)
            GameManager.instance.FinExamenGeografia();
    }

    void ActualizaVidas()
    {
        textoVidas.text = "Vidas: " + vidas;
    }
}