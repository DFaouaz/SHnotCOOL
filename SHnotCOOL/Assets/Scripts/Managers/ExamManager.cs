using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (GameManager.instance.Examen)
        {
            case (0):
                SceneManager.LoadScene("Matematicas");
                break;
            case (1):
                SceneManager.LoadScene("Historia");
                break;
            case (2):
                SceneManager.LoadScene("Lengua");
                break;
            case (3):
                SceneManager.LoadScene("Geografia");
                break;

        }
    }
}
