using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiaEscena : MonoBehaviour {

	public string escenaACambiar;// Aula si es un examen
    public string Examen;
    bool pasillos;

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Player") {
			GameManager.instance.mensajeEscena.gameObject.SetActive(true);
            if (escenaACambiar != "Aula")
				GameManager.instance.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para acceder al\n" + escenaACambiar;
            else
				GameManager.instance.mensajeEscena.text = "Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para acceder al\nAula de " + Examen;
                if (escenaACambiar == "Aula") {
                    int num = Random.Range(0, 10);
                    if (num < 3)
                        pasillos = true;                        
                }
           
            if (escenaACambiar != "Piso1")
                GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
            if (escenaACambiar != "Piso2")
                GameManager.instance.Escena2PlayerPos = GameManager.instance.ActualPlayerPosition;

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

    void OnTriggerExit2D(Collider2D other){
		GameManager.instance.mensajeEscena.gameObject.SetActive(false);
    }

    void Update() {
		if(Input.GetKeyDown(GameManager.instance.botonInteractuar) && GameManager.instance.mensajeEscena.gameObject.activeInHierarchy) {
            if (pasillos)
                SceneManager.LoadScene("Pasillos");
            else
                SceneManager.LoadScene(escenaACambiar);
			GameManager.instance.mensajeEscena.gameObject.SetActive (false);
        }
    }
}

