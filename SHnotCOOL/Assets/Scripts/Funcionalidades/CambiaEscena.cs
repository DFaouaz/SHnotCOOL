using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiaEscena : MonoBehaviour {
	public Text mensajeEscena;
	[HideInInspector]
	public Entrance entrada;

	void Start(){
		if (mensajeEscena == null)
			mensajeEscena = GameObject.FindGameObjectWithTag ("MensajeEscena").GetComponent<Text> ();
		if (entrada == null)
			mensajeEscena.gameObject.SetActive (false);
	}

    void Update() {
		if(Input.GetKeyDown(GameManager.instance.botonInteractuar) && entrada != null) {
			SavePositions ();
			ChooseExam ();
			if (isPasillosTime())
                SceneManager.LoadScene("Pasillos");
            else
                SceneManager.LoadScene(entrada.escenaACambiar);
        }
    }

	bool isPasillosTime(){
		if (entrada.escenaACambiar == "Aula") {
			int num = Random.Range (0, 10);
			return num < 3;
		} else
			return false;
	}

	void SavePositions(){
		if (entrada.escenaACambiar != "Piso1")
			GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
		if (entrada.escenaACambiar != "Piso2")
			GameManager.instance.Escena2PlayerPos = GameManager.instance.ActualPlayerPosition;
	}

	void ChooseExam(){
		if (entrada.escenaACambiar == "Aula") {
			if (entrada.examen == "Matematicas" && !GameManager.instance.finMates)
				GameManager.instance.Examen = 0;
			else if (entrada.examen == "Historia" && !GameManager.instance.finHistoria)
				GameManager.instance.Examen = 1;
			else if (entrada.examen == "Lengua" && !GameManager.instance.finLengua)
				GameManager.instance.Examen = 2;
			else if (entrada.examen == "Geografia" && !GameManager.instance.finGeo)
				GameManager.instance.Examen = 3;
			else
				GameManager.instance.Examen = 4;
		}
	}

}

