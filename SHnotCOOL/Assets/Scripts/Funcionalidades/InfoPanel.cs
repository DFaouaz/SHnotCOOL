using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject mesagePanel;
    public Text text;
    public string mesage;

    void OpenPanel()
    {
        text.text = mesage;
		mesagePanel.GetComponentInParent<Transform> ().SetAsLastSibling ();
		mesagePanel.GetComponentInChildren<Button> ().Select ();
        mesagePanel.SetActive(true);
		GameManager.instance.ventanaAbierta = true;
    }

    public void ClosePanel()
    {
        mesagePanel.SetActive(false);
		GameManager.instance.ventanaAbierta = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player"))
        {
            OpenPanel();
			Destroy (this.gameObject);
        }
    }
}