using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    Text textoVidas;
    SpriteRenderer playerRender;
    public int vidas;
    int repeticiones = 16;
    float tiempo = 0.1f;
    bool golpeado = false, parpadea = false;

	void Start () {
        textoVidas = GameObject.FindGameObjectWithTag("Vidas").GetComponent<Text>();
        playerRender = GetComponent<SpriteRenderer>();
		ActualizaVidas ();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            Damage();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemigo"))
            Damage();
    }

    void Damage()
    {
        if (vidas > 1 && !golpeado)
        {
            golpeado = true;
            vidas--;
            ActualizaVidas();
            InvokeRepeating("Invulnerable", 0, tiempo);
            Invoke("FinInvulnerable", tiempo * repeticiones);
        }
        else if (vidas == 1)
        {
            vidas--;
            ActualizaVidas();
            GameManager.instance.FinExamenMatematicas();
        }
    }

    void Invulnerable()
    {
        playerRender.enabled = parpadea;
        parpadea = !parpadea;
    }

    void FinInvulnerable()
    {
        CancelInvoke();
        golpeado = false;
        playerRender.enabled = true;
    }

	void ActualizaVidas()
    {
		textoVidas.text = "Vidas: " + vidas;
	}
}