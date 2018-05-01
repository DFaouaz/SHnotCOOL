using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour {

	Text mensaje;

	void Start(){
		mensaje = GameObject.FindGameObjectWithTag ("MensajeExamen").GetComponent<Text> ();
		mensaje.gameObject.SetActive (false);
	}


    void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player")
			mensaje.gameObject.SetActive (true);			
    }

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player")
			mensaje.gameObject.SetActive (false);
	}

	void Update(){
		if (mensaje.gameObject.activeInHierarchy && Input.GetKeyDown (GameManager.instance.botonInteractuar))
			EnterExam ();
	}



	void EnterExam(){
		switch (GameManager.instance.Examen)
		{
		case (0):
			SceneManager.LoadScene("Matematicas");
			break;
		case (1):
			SceneManager.LoadScene("Historia");
			break;
		case (2):
			SceneManager.LoadScene("Lengua");
			break;
		case (3):
			SceneManager.LoadScene("Geografia");
			break;
		}
	}
}
