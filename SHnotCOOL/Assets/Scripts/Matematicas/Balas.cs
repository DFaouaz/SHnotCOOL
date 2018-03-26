using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour {

    TextMesh bala;
    // Use this for initialization
    void Start () {
        string[] numeros = { "+", "-", "*", "/" };
        bala = gameObject.GetComponent<TextMesh>();
        bala.text = "" + numeros[Random.Range(0, 4)];
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Limite"))
        
            Destroy(this.gameObject);
    }

}
