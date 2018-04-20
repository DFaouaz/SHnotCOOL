using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class temporizador : MonoBehaviour {
	Text temporiza;
	int temporizadorapoyo,temp;
	// Use this for initialization
	void Start () {
		temporizadorapoyo = 0;
		temporiza = GetComponent<Text> ();

		temporiza.text=PasillosManager.instance.TiempoRestante().ToString() +"S";
	}
	
	// Update is called once per frame
	void Update () {
		temp = (int)Time.time;
		print (temp);
		if (temp > temporizadorapoyo) {
			temporizadorapoyo = temp;
			PasillosManager.instance.RestarTiempo ();
			temporiza.text=PasillosManager.instance.TiempoRestante().ToString() +"S";
		}
	}
}
