using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(escenaACambiar);
    }
}
