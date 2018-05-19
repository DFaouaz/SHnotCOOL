using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {
    Text text;
    public float timeLimit;

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
        else if (timeLimit == 0)
            GameManager.instance.FinExamenGeografia();
    }
}