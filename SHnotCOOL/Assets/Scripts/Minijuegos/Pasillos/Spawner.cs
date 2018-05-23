using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float step, initialDelay, speed;
	public bool leftSense;
	public GameObject varPrefab;

	void Start () {

		Invoke ("Spawn", initialDelay);
	}
    
	void Spawn()
	{
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
			GameObject obj = Instantiate (varPrefab, transform.position, Quaternion.identity);
			RowMovement row = obj.GetComponent<RowMovement>();
			row.Move (speed, leftSense);
		}
		Invoke ("Spawn", step);
	}
}