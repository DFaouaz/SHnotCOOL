using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinJuego : MonoBehaviour {
    public Text dinero;
    public Text amigos;
    public Text nota;

    Text textoFin;
    // Use this for initialization
    void Start()
    {
        textoFin = GetComponent<Text>();
        if(GameManager.instance.notaFinal>5)
            textoFin.text = "Enhorabuena has aprobado el curso";
        else
            textoFin.text = "Has suspendido";
        dinero.text = "Dinero: " + (GameManager.instance.dinero);
        amigos.text = "Amigos: " + GameManager.instance.numFriends;
        nota.text = "Nota Final: " + (GameManager.instance.notaFinal);
		InputConfiguration.SelectFirstFoundButton ();
    }


}
