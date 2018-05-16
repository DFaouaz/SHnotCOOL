using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour {


	GameObject player;


    void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"&& GameManager.instance.trimestre>=GameManager.instance.exam.Trimestre()) {
			MessageManager.instance.ShowMessage ("Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para realizar el examen.");
			player = col.gameObject;
		}		
    }

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			MessageManager.instance.CloseMessage ();
			player = null;
		}
	}

	void Update(){
        if (player != null && Input.GetKeyDown(GameManager.instance.botonInteractuar))
        {
            EnterExam();
			GameManager.instance.lastPosEntrance = Vector3.zero;
			GameManager.instance.matonAble = true;
        }
	}



	void EnterExam(){
		switch (GameManager.instance.exam.NExamen())
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
