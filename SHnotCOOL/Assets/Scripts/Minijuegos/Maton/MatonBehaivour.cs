using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatonBehaivour : MonoBehaviour {

	public int probInicial, tiempo;

	void Start () {

		Invoke("ProMaton", tiempo);
	}
	
    void ProMaton()
    {
        int prob = Random.Range(0, 100);

		if (prob < (probInicial - ((GameManager.instance.numFriends * probInicial) / GameManager.instance.maxFriends)) && GameManager.instance.matonAble 
            && !GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
			GameManager.instance.lastPosPasillos = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameManager.instance.lastPosEntrance = Vector3.zero;
            SceneManager.LoadScene("Maton");
        }
        Invoke("ProMaton", tiempo);
    }
}