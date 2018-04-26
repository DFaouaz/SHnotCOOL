using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NPC : Mission {
	public string scene;
	public Queue<string> dialogo = new Queue<string> ();
	public Queue<string> frasesEstandar = new Queue<string> ();
	[HideInInspector]
	public bool alreadyTalked = false;


	protected override void Start(){
		base.Start ();
		LeeDialogo ();
	}

    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			DialogueManager.instance.dialogueMensaje.gameObject.SetActive (true);
			DialogueManager.instance.currentNPC = this;
		}
    }

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			DialogueManager.instance.dialogueMensaje.gameObject.SetActive (false);
			DialogueManager.instance.currentNPC = null;
		}
	}


   public void LeeDialogo() {
		StreamReader file = new StreamReader ("Dialogos.txt");
        string linea;
		//Buscamos el nombre
        do{
			linea = file.ReadLine();
		} while (!file.EndOfStream && linea != nombrePersonaje);

		//Decodificamos
		linea = file.ReadLine();
		while (linea != "" && linea != null && !file.EndOfStream) {
			switch (linea) {
			//Usaremos # como marca de que empieza y termina la conversación
			//y ## como marca de que empiezan y terminan los pasos.
			case "#":
				linea = file.ReadLine ();
				do {
					dialogo.Enqueue (linea);
					linea = file.ReadLine ();
				} while(linea != "#");
				break;
			case "##":
				linea = file.ReadLine ();
				do {
					frasesEstandar.Enqueue (linea);
					linea = file.ReadLine ();
				} while(linea != "##");
				break;
			}
			linea = file.ReadLine();
		}
		dialogo.Enqueue (null);
		frasesEstandar.Enqueue (null);
		file.Close();
    }

	public void UpdateNPCs(){
		string actualScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
		if (scene == actualScene)
			this.gameObject.SetActive (true);
		else
			this.gameObject.SetActive (false);
	}
}
