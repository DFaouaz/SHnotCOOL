using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		//Llama al metodo Dead de player cuando la rana es atropellada
		if (col.gameObject.CompareTag ("player"))
			col.gameObject.GetComponent<Player> ().Dead ();
	}

	
}
