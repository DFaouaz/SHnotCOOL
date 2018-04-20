using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMaton : MonoBehaviour {
    int Prob;
   
	// Use this for initialization
	void Start () {
        Invoke("probMaton", 20);
	}
	
    void probMaton()
    {
        Prob = Random.Range(0, 100);
        if (Prob < 20 && !DialogueManager.instance.isTalking)
        {
            Prob = 50;
            SceneManager.LoadScene("Maton");
        }
        else
            Invoke("probMaton", 20);
    }
}
