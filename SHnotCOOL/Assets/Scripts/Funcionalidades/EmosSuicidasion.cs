using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmosSuicidasion : MonoBehaviour {

	static NPC npc;

	void Start(){
		npc = GetComponent<NPC> ();
	}




	public static void CheckMuerte(){
		if (npc.indiceMision == 2) {
			GameManager.instance.emosMuerto = true;
			npc.scene = "Muerte";
			npc.gameObject.SetActive (false);
			npc = null;
			GameManager.instance.BajaNAmigos (0);
		}
	}



}
