using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour {
    GameObject player, respawn;
    Text textVidas;
    public int vidas;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        textVidas = GameObject.FindGameObjectWithTag("Vidas").GetComponent<Text>();
        UpdateVidas();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Dead();
    }

    void UpdateVidas()
    {
        textVidas.text = "Vidas: " + vidas;
    }

    void Dead()
    {
        if (vidas > 0)
        {
            player.transform.position = respawn.transform.position;
            vidas--;
            UpdateVidas();
            if (vidas == 0)
                GameManager.instance.FinExamenGeografia();
        }
    }
}