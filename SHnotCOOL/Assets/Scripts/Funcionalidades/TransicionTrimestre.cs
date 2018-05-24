using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransicionTrimestre : MonoBehaviour {
    public Text dinero;
    public Text amigos;
    public Text nota;

    Text textoCambio;
	// Use this for initialization
	void Start () {
        textoCambio = GetComponent<Text>();
        textoCambio.text = "Has completado el trimestre " + (GameManager.instance.trimestre-1);
        dinero.text = "Dinero: " + (GameManager.instance.dinero - 100);
        amigos.text = "Amigos: " +GameManager.instance.numFriends;
        nota.text = "Nota: " + (GameManager.instance.media);
		InputConfiguration.SelectFirstFoundButton ();
	}
	
    public void VuelveAEscena()
    {
        SceneManager.LoadScene("Escuela");
    }
}
