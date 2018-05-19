using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public float step, initialDelay, speed;
	public bool leftSense;
	public GameObject varPrefab;

	void Start () {
		//Llama a la funcion Spawn por primera vez con el Delay inicial
		Invoke ("Spawn", initialDelay);
	}

	//Crea el Prefab en la posicion del spawner y llama a la 
	//funcion Move de RowMovement para poner el objeto en movimiento
	void Spawn()
	{
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta) {
			GameObject obj = Instantiate (varPrefab, transform.position, Quaternion.identity);
			RowMovement row = obj.GetComponent<RowMovement> ();
			row.Move (speed, leftSense);
		}
		//LLamada recursiva a la funcion de spawn para crear varios
		Invoke ("Spawn", step);
	}

}
