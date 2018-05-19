using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour {

    TextMesh bala;
    string[] signos = { "+", "-", "*", "/" };

    void Start () {
        
        bala = gameObject.GetComponent<TextMesh>();
        bala.text = "" + signos[Random.Range(0, 4)];
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Limite"))
            Destroy(this.gameObject);
    }
}