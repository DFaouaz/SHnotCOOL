using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMaton : MonoBehaviour {
	int prob;
	int prueba = 5;
	int tiempo;
   
	// Use this for initialization
	void Start () {
		tiempo = 10 + prueba;
		Debug.Log ("Tiempo: " + tiempo);
		Invoke("probMaton", tiempo);
	}
	
    void probMaton()
    {
        prob = Random.Range(0, 100);
		if (prob < 20 && !GameManager.instance.ventanaAbierta)
        {
            prob = 50;
            SceneManager.LoadScene("Maton");
        }
        /*else
            Invoke("probMaton", 20);*/
		tiempo += 20;
		Debug.Log ("Tiempo: " + tiempo);
    }
}
