using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;// Aula si es un examen
    public string Examen;
	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("Entrada");
		if (col.tag == "Player" ) {	//ES MA PROVISIONAL
            if(escenaACambiar!="Piso1")
			    GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
			SceneManager.LoadScene (escenaACambiar);
            if (Examen == "Matematicas" && GameManager.instance.matematicasScore == 0)
                GameManager.instance.Examen = 0;
            else if (Examen == "Historia" && GameManager.instance.historiaScore == 0)
                GameManager.instance.Examen = 1;
            else if (Examen == "Lengua" && GameManager.instance.lenguaScore == 0)
                GameManager.instance.Examen = 2;
            else if (Examen == "Geografia" /*&& GameManager.instance.geografiaScore == 0*/)
                GameManager.instance.Examen = 3;
            else
                GameManager.instance.Examen = 4;
        }
	}
}
