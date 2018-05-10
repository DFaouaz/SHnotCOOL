using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour {

	public string entranceName;
	public Entrance entranceConnection;
	[Header("Introduce el nombre y el trimestre de la escena del examen")]
	public string examSceneName;
    public int trimestreAparicion;
	EntranceManager em;
	[Header("Marcar si es una salida")]
	public bool isExit;

	void Awake(){
		em = GetComponentInParent<EntranceManager>();
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" && !GameManager.instance.thereIsAnInteractiveEvent) {
			MuestraMensaje ();
			em.entrance = this;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			GameManager.instance.thereIsAnInteractiveEvent = false;
			MessageManager.instance.CloseMessage ();
			em.entrance = null;
		}
	}

	void MuestraMensaje(){
		GameManager.instance.thereIsAnInteractiveEvent = true;
		if (!isExit)
			MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para acceder al\n" + entranceName);
		else
			MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para salir de\n" + GameManager.instance.lastEntrance.entranceName);
		
	}

	public void MoveToConnection(GameObject character){
		if (isExit) 
			character.transform.position = GameManager.instance.lastEntrance.gameObject.transform.position;
		else
			character.transform.position = entranceConnection.gameObject.transform.position; 
		ChooseExam ();
		GameManager.instance.lastEntrance = this;
		GameManager.instance.lastPosEntrance = this.gameObject.transform.position;
	}

	void ChooseExam(){
		if (examSceneName == "Matematicas" && !GameManager.instance.finMates)
			GameManager.instance.exam = new Examen(0,trimestreAparicion);
		else if (examSceneName == "Historia" && !GameManager.instance.finHistoria)
            GameManager.instance.exam = new Examen(1, trimestreAparicion);
        else if (examSceneName == "Lengua" && !GameManager.instance.finLengua)
            GameManager.instance.exam = new Examen(2, trimestreAparicion);
        else if (examSceneName == "Geografia" && !GameManager.instance.finGeo)
            GameManager.instance.exam = new Examen(3, trimestreAparicion);
        else
            GameManager.instance.exam = new Examen(4, 0);
    }

}