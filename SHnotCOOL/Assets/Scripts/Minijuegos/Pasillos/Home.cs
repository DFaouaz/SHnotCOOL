using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {
	
	// Llama al método Game del GameManager
	public void OnTriggerEnter2D(Collider2D other)
	{
		GameObject go = other.gameObject;
		if (go.CompareTag ("player")) 
			PasillosManager.instance.Home ();
	}
}
