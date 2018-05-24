using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

    public Text finJuego;
    public float timeLimit;
    Text text;

    void Start() {

        text = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        text.text = timeLimit.ToString();
    }

    void Update() {

		if(!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
			Timer();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.GeoScore >= 5)
                finJuego.text = "No esta mal";
            else
                finJuego.text = "Das asco";
            GameManager.instance.FinExamenGeografia();
        }
    }

    void Timer()
    {
        if (timeLimit > 0)
        {
            timeLimit -= (float)Time.deltaTime;
            text.text = timeLimit.ToString();
        }
        else if (timeLimit <= 0)
        {
            finJuego.text = "Das asco";
            GameManager.instance.FinExamenGeografia();
        }
    }
}