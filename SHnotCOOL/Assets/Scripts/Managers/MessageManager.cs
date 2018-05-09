using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

	public static MessageManager instance;
	public Text mensaje;

	void Awake(){
		if (instance == null){
			instance = this;
		}else
			Destroy(this);
	}

	void Start(){
		if (mensaje == null)
			mensaje = GetComponentInChildren<Text> ();
	}

	public void ShowMessage(string text){
		mensaje.gameObject.SetActive(true);
		mensaje.text = text;
	}

	public void CloseMessage(){
		mensaje.gameObject.SetActive (false);
	}
	public bool isActive(){
		return mensaje.gameObject.activeInHierarchy;
	}
	

}
