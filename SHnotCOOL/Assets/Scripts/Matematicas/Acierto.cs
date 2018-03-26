using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acierto : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            GameManager.instance.matematicasScore++;
            FindObjectOfType<AparecenRandoms>().CambiaOperacion();
            
            Destroy(this.gameObject);
        }
    }
}
