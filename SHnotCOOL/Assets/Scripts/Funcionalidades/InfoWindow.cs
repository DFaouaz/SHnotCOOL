using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    public GameObject mesagePanel;
    public Text text;
    public string mesage;

	void Awake ()
    {
        Time.timeScale = 0;
	}

	void Start ()
    {
        text.text = mesage;
	}

    public void ClosePanel()
    {
        mesagePanel.SetActive(false);
        Time.timeScale = 1;
    }
}