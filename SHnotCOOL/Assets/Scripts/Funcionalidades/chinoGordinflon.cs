using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chinoGordinflon : MonoBehaviour {
	static NPC npc;

	void Start(){
		npc = GetComponent<NPC> ();
	}




	public static void CheckComida(){
		if (npc.indiceMision == 2) {
			GameManager.instance.perroComido = true;
		}
	}
}
