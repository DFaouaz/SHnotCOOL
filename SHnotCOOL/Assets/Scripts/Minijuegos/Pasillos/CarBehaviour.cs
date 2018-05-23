using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player"))
			col.gameObject.GetComponent<Player> ().Dead ();
	}
}