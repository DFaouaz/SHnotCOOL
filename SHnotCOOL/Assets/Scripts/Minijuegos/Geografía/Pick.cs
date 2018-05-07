using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick : MonoBehaviour {
    static int total;
    public AudioClip SonidoRecogida;    
    GameObject text;
    Text textoItems;
 
    void Start () {
        text = GameObject.FindGameObjectWithTag("Items");
        textoItems = text.GetComponent<Text>();
        total++;
        ActualizaItems();
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GameManager.instance.GeoScore++;
            ActualizaItems();
			AudioSource.PlayClipAtPoint (SonidoRecogida, gameObject.transform.position);
            Destroy(this.gameObject);
            if (GameManager.instance.GeoScore == total)
                GameManager.instance.FinExamenGeografia();
        }
    }
    void ActualizaItems(){
        textoItems.text = "Items: " + GameManager.instance.GeoScore + "/" + total;
    }
}