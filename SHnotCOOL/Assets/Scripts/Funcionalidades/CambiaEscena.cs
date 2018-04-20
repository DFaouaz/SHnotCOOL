using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;// Aula si es un examen
    public string Examen;
	void OnTriggerEnter2D(Collider2D col){
		
		if (col.tag == "Player" ) {
            if (escenaACambiar=="Aula")
            {
                int num = Random.Range(0, 10);
                if(num<3)
                    SceneManager.LoadScene("Pasillos");
                else
                    SceneManager.LoadScene(escenaACambiar);
            }
            else
                SceneManager.LoadScene(escenaACambiar);
            if (escenaACambiar!="Piso1")
			    GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
			
            if (Examen == "Matematicas" && !GameManager.instance.finMates)
                GameManager.instance.Examen = 0;
            else if (Examen == "Historia" && !GameManager.instance.finHistoria)
                GameManager.instance.Examen = 1;
            else if (Examen == "Lengua" && !GameManager.instance.finLengua)
                GameManager.instance.Examen = 2;
            else if (Examen == "Geografia" && !GameManager.instance.finGeo)
                GameManager.instance.Examen = 3;
            else
                GameManager.instance.Examen = 4;
        }
	}
}
