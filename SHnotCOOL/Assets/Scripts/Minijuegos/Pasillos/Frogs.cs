using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frogs : MonoBehaviour {
	
	Text vidasText;

	void Start () {
		vidasText = GetComponent<Text> ();

		vidasText.text="VIDAS: "+ PasillosManager.instance.Frogs ().ToString();
	}
}