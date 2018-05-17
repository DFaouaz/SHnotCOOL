using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject mesagePanel;
    public Text text;
    public string mesage;
    bool active = true;

    void Awake()
    {
        if (active)
        {
            OpenPanel();
        }
    }

    void OpenPanel()
    {
        text.text = mesage;
        mesagePanel.SetActive(active);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        mesagePanel.SetActive(!active);
        Time.timeScale = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenPanel();
            Destroy(this);
        }
    }
}