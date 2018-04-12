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

	void Start () {
        text = GameObject.FindGameObjectWithTag("Vidas");
        textoVidas = text.GetComponent<Text>();
        
        render = GetComponent<SpriteRenderer>();
		ActualizaVidas ();
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bala") {
            Damage();
            Destroy(collision.gameObject);
        }
    }

    void Damage() {
		if (vidas > 0 && !golpeado) {
			vidas--;
			ActualizaVidas ();
			golpeado = true;
			InvokeRepeating ("Invulnerable", 0, tiempo);
			Invoke ("CancelInvoke", tiempo * repeticiones);
			golpeado = false;
		} else if (vidas-- == 0)
			GameManager.instance.FinExamenMatematicas ();
    }

    void Invulnerable() {
        render.enabled = parpadea;
        parpadea = !parpadea;
    }

	void ActualizaVidas(){
		textoVidas.text = "Vidas: " + vidas;
	}
}
