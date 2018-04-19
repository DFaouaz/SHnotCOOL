using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
    GameObject respawn, go;

	void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        go = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            respawn.transform.position = go.transform.position;

    }
}
