using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
            PasillosManager.instance.Home();
	}
}