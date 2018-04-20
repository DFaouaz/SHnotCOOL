using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMaton : MonoBehaviour {
    int Prob;
	// Use this for initialization
	void Start () {
        Invoke("MatonAleatorio",0);
	}
	void MatonAleatorio()
    {
        Prob = Random.Range(0, 100);
        if (Prob < 20&&!DialogueManager.instance.isTalking)
        {
            GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
            SceneManager.LoadScene("Maton");
            Prob = 50;
        }
        else
            Invoke("MatonAleatorio", 20);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
