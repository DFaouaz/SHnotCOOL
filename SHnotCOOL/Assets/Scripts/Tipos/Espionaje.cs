using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espionaje : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
       MisionManager.instance.lista.completado = true;
    }
}
