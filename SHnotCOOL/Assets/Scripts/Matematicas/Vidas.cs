using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    GameObject text;
    
    Text textoVidas;
    SpriteRenderer render;
    public int vidas = 5;
    int repeticiones = 8;
    float tiempo = 0.1f;
    bool golpeado = false, parpadea = false;
	// Use this for initialization
	void Start () {
        text = GameObject.FindGameObjectWithTag("Vidas");
        textoVidas = text.GetComponent<Text>();
        
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        textoVidas.text = "Vidas: " + vidas;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            Damage();
            Destroy(collision.gameObject);
        }
    }

    void Damage()
    {
        if (vidas > 0 && !golpeado)
        {
            vidas--;
            golpeado = true;
            InvokeRepeating("Invulnerable", 0, tiempo);
            Invoke("CancelInvoke", tiempo * repeticiones);
            golpeado = false;
        }
        else
            GameManager.instance.FinExamenMatematicas();
    }

    void Invulnerable()
    {
        render.enabled = parpadea;
        parpadea = !parpadea;
    }
}
