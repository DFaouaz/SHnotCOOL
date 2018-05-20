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
	[HideInInspector]
	public Vector3 pos;
	public bool isClass;
	[Header("Marcar si es una sala")]
	public bool isRoom;
	[Header("Marcar si es una salida")]
	public bool isExit;

	public Entrance (Entrance entrance)
	{
		entranceName = entrance.entranceName;
		entranceConnection = null;
		em = entrance.em;
		pos = entrance.pos;
		isClass = entrance.isClass;
		isExit = entrance.isExit;
	}

	void Awake(){
		em = GetComponentInParent<EntranceManager>();
		pos = transform.position;
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
			MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString () + " para salir de\n" + GameManager.instance.lastEntranceName);
		
	}

	public void MoveToConnection(GameObject character){
		if (isExit) {
			if (GameManager.instance.lastEntrance != null)
				character.transform.position = GameManager.instance.lastEntrance.pos;
			else
				character.transform.position = GameManager.instance.lastPosPasillos;
		}
		else
			character.transform.position = entranceConnection.pos;
		GameManager.instance.lastEntrance = this;
		GameManager.instance.lastEntranceName = entranceName;
		GameManager.instance.lastPosEntrance = this.pos;
	}

	public void ChooseExam(){
		if (isClass) {
            if (examSceneName == "Matematicas" && !GameManager.instance.finMates)
            {
                GameManager.instance.exam = new Examen(0, trimestreAparicion);
                if (trimestreAparicion <= GameManager.instance.trimestre)
                    GameManager.instance.marcaExamen = true;
                else
                    GameManager.instance.marcaExamen = false;
            }
            else if (examSceneName == "Historia" && !GameManager.instance.finHistoria)
            {
                GameManager.instance.exam = new Examen(1, trimestreAparicion);
                if (trimestreAparicion <= GameManager.instance.trimestre)
                    GameManager.instance.marcaExamen = true;
                else
                    GameManager.instance.marcaExamen = false;
            }
            else if (examSceneName == "Lengua" && !GameManager.instance.finLengua)
            {
                GameManager.instance.exam = new Examen(2, trimestreAparicion);
                if (trimestreAparicion <= GameManager.instance.trimestre)
                    GameManager.instance.marcaExamen = true;
                else
                    GameManager.instance.marcaExamen = false;
            }
            else if (examSceneName == "Geografia" && !GameManager.instance.finGeo)
            {
                GameManager.instance.exam = new Examen(3, trimestreAparicion);
                if (trimestreAparicion <= GameManager.instance.trimestre)
                    GameManager.instance.marcaExamen = true;
                else
                    GameManager.instance.marcaExamen = false;
            }
            }
        else
            GameManager.instance.exam = new Examen(4, 0);
    }
	public void CheckMaton(){
		if (isRoom)
			GameManager.instance.matonAble = false;
		else
			GameManager.instance.matonAble = true;
	}

}