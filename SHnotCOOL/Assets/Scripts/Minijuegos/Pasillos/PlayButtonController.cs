using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonController : MonoBehaviour {
	
	// Llama al método Game del GameManager
	public void LoadGame()
	{
		PasillosManager.instance.Game ();
	}
}
