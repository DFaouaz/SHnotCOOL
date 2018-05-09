using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioMaton : MonoBehaviour {
	public int probInicial = 20;
	public int time = 20;
   

	void Start () {
		Invoke("probMaton", time);
	}
	
    void probMaton() {
        int prob = Random.Range(0, 100);
        if (prob < probInicial - ((GameManager.instance.numFriends * probInicial) / GameManager.instance.maxFriends)
            && !GameManager.instance.ventanaAbierta)
			SceneManager.LoadScene("Maton");
        
	}
}
