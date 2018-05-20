using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Espionaje : MonoBehaviour {

	public NPC NPCMision;
	public string scene;
	 
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player")
			SacaConversacion ();			
    }

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player")
			Destroy (this.gameObject);
	}

	void SacaConversacion(){
		//TP de los personajes espaciales
		if ((NPCMision.nombrePersonaje == "Delegada" && NPCMision.indiceMision == 2) ||
			(NPCMision.nombrePersonaje == "Minusvalido" && NPCMision.indiceMision == 0))
			NPCMision.transform.position = transform.position;
		Debug.Log ("Saca conversacion espia");
		DialogueManager.instance.frasesEspia = new Queue<FraseEspia>(NPCMision.pasos.Dequeue ().frasesEspia);
		DialogueManager.instance.AbreCierraDialogueCanvas ();
		DialogueManager.instance.MuestraFrasesEspia ();
		DialogueManager.instance.ableInput = true;
		//Si hay mas pasos, los carga
		if (NPCMision.pasos.ToArray()[0] != null) {
			Espionaje esp = GameObject.FindGameObjectWithTag (NPCMision.pasos.ToArray () [0].tagObjeto).AddComponent<Espionaje> ();
			esp.NPCMision = NPCMision;
			esp.scene = "Escuela";
			MissionManager.instance.ActualizaPasos ((Mission) NPCMision);
		} else
			NPCMision.isComplete = true;
		gameObject.SetActive(false);
	}
}
