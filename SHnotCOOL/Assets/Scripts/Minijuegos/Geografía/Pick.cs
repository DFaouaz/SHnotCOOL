using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pick : MonoBehaviour {

    Text textItems;
    public AudioClip soundPick;
    static int total;

    void Start () {

        textItems = GameObject.FindGameObjectWithTag("Items").GetComponent<Text>();
        total++;
        UpdateItems();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GameManager.instance.GeoScore++;
            AudioSource.PlayClipAtPoint(soundPick, gameObject.transform.position);
            UpdateItems();

            Destroy(this.gameObject);
        }
    }

    void UpdateItems()
    {
        textItems.text = "Items: " + GameManager.instance.GeoScore + "/" + total;
    }
}