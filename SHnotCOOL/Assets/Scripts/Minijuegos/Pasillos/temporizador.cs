using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class temporizador : MonoBehaviour {
	Text temporiza;
	int temporizadorapoyo,temporizador;
	// Use this for initialization
	void Start () {
		temporizadorapoyo = 0;
		temporiza = GetComponent<Text> ();

		temporiza.text=PasillosManager.instance.TiempoRestante().ToString() +"S";
	}
	
	// Update is called once per frame
	void Update () {
		temporizador = (int)Time.time;
		print (temporizador);
		if (temporizador > temporizadorapoyo) {
			temporizadorapoyo = temporizador;
			PasillosManager.instance.RestarTiempo ();
			temporiza.text=PasillosManager.instance.TiempoRestante().ToString() +"S";
		}
	}
}
