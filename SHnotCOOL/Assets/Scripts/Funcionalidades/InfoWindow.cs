using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    public GameObject mesagePanel;
    public Text text;
    public string mesage;

	void Start ()
    {
        text.text = mesage;
		GameManager.instance.pauseMode = true;
		GameManager.instance.ventanaAbierta = true;
		mesagePanel.GetComponentInChildren<Button> ().Select ();
	}

    public void ClosePanel()
    {
        mesagePanel.SetActive(false);
		GameManager.instance.ventanaAbierta = false;
		GameManager.instance.pauseMode = false;
		InputConfiguration.SelectFirstFoundButton ();
        //Time.timeScale = 1;
    }
}