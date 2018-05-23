using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

	Text timer;
	int tiempoAux, tiempo;

	void Start () {

        tiempoAux = 5;
        timer = GetComponent<Text>();
        timer.text = PasillosManager.instance.TiempoRestante().ToString() + "s";
	}
	
	void Update () {

        if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
            tiempo = (int)Time.time;
            if (tiempo > tiempoAux)
            {
                tiempoAux = tiempo;
                PasillosManager.instance.RestarTiempo();
                timer.text = PasillosManager.instance.TiempoRestante().ToString() + "s";
            }
        }
	}
}