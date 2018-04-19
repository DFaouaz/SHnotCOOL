using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick : MonoBehaviour {
    static int total;
    int destruidos;
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
            destruidos++;
            ActualizaItems();
            Destroy(this.gameObject);
            if (destruidos == total)
                GameManager.instance.FinExamenGeografia();
        }
    }
    void ActualizaItems()
    {
        textoItems.text = "Items: " + destruidos + "/" + total;
    }
}