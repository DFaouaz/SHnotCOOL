using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Espionaje : MonoBehaviour {

	public NPC NPCMision;
	 
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player")
			SacaConversacion ();			
    }
	void SacaConversacion(){
		Debug.Log ("Saca conversacion espia");
		DialogueManager.instance.frasesEspia = NPCMision.pasos.Dequeue ().frasesEspia;
		DialogueManager.instance.AbreCierraDialogueCanvas ();
		DialogueManager.instance.MuestraFrasesEspia ();
		DialogueManager.instance.ableInput = true;
		//Si hay mas pasos, los carga
		if (NPCMision.pasos.ToArray()[0] != null) {
			Espionaje esp = GameObject.FindGameObjectWithTag (NPCMision.pasos.ToArray () [0].tagObjeto).AddComponent<Espionaje> ();
			esp.NPCMision = NPCMision;
		} else
			NPCMision.isComplete = true;
		Destroy (this.gameObject);
	}
		
}
