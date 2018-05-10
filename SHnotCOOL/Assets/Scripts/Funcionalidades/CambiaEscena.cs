using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiaEscena : MonoBehaviour {
	public Text mensajeEscena;
	[HideInInspector]
	public Entrance entrance;

	void Start(){
		if (mensajeEscena == null)
			mensajeEscena = GameObject.FindGameObjectWithTag ("MensajeEscena").GetComponent<Text> ();
		if (entrance == null)
			mensajeEscena.gameObject.SetActive (false);
	}

    void Update() {
		if (mensajeEscena == null)
			mensajeEscena = GameObject.FindGameObjectWithTag ("MensajeEscena").GetComponent<Text> ();
		if(Input.GetKeyDown(GameManager.instance.botonInteractuar) && entrance != null) {
			//SavePositions ();
			//ChooseExam ();
			if (isPasillosTime ())
				SceneManager.LoadScene ("Pasillos");
			/*else if (actualScene != "Piso1" || actualScene != "Piso2")
				SceneManager.LoadScene (entrada.escenaACambiar);
			else
				SceneManager.LoadScene (GameManager.instance.lastScene);*/
        }
    }

	bool isPasillosTime(){
		if (entrance.isExit) {
			int num = Random.Range (0, 10);
			return num < 3;
		} else
			return false;
	}

	/*void SavePositions(){
		if (actualScene == "Piso1" || actualScene == "Piso2") {
			if (entrada.escenaACambiar != "Piso1" && actualScene != "Piso2")
				GameManager.instance.Escena1PlayerPos = GameManager.instance.ActualPlayerPosition;
			else if (entrada.escenaACambiar != "Piso2" && actualScene != "Piso1")
				GameManager.instance.Escena2PlayerPos = GameManager.instance.ActualPlayerPosition;
		}
	}

	void ChooseExam(){
		if (entrance.isExit) {
			if (entrance.examSceneName == "Matematicas" && !GameManager.instance.finMates)
				GameManager.instance.Examen = 0;
			else if (entrance.examSceneName == "Historia" && !GameManager.instance.finHistoria)
				GameManager.instance.Examen = 1;
			else if (entrance.examSceneName == "Lengua" && !GameManager.instance.finLengua)
				GameManager.instance.Examen = 2;
			else if (entrance.examSceneName == "Geografia" && !GameManager.instance.finGeo)
				GameManager.instance.Examen = 3;
			else
				GameManager.instance.Examen = 4;
		}
	}*/

}

