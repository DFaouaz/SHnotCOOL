﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour {

	public HUDManager HUD;
	public Button botonSalir, botonHablar, botonDarObjeto, botonAceptarMision, botonCompletarMision;
    public Text nombre;
    public Text sentence;
    public GameObject dialogueBox;
	public Text dialogueMensaje;
	public KeyCode botonInteraccion;
	[HideInInspector]
	public NPC currentNPC;
	//[HideInInspector]
	public bool isTalking = false;
	//Para corregir el input del Espacio
	[HideInInspector]
	public bool ableInput = false;
    // cola de strings( el primero en meterse es el primero en salir
	public Queue<string> frases;
	public Queue<FraseEspia> frasesEspia;
	public static DialogueManager instance;

	void Awake()
	{
		// Si no hay ningún objeto GameManager ya creado
		if (instance == null)
		{
			// Almacenamos la instancia actual
			instance = this;
			// Nos aseguramos de no destruir el objeto, es decir, 
			// de que persista, si cambiamos de escena
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			// Si ya existe un objeto GameManager, no necesitamos uno nuevo
			Destroy(this.gameObject);
		}
	}

    void Start(){
		dialogueMensaje.text = "Pulsar " + botonInteraccion.ToString () + " para interactuar.";
		dialogueMensaje.gameObject.SetActive (false);
    }

    void Update() {
		CheckInputDialogue ();
    }
		
	//Comprueba si el ususario da la orden de comenzar la conversación
	void CheckInputDialogue(){
		
		if (Input.GetKeyDown (botonInteraccion) && !isTalking && currentNPC != null) {
			dialogueMensaje.gameObject.SetActive (false);
			AbreCierraDialogueCanvas ();
		} else if (isTalking && (Input.GetKeyDown (KeyCode.Space)
		            || Input.GetKeyDown (KeyCode.Mouse0))) { 
			if (ableInput) {
				if (currentNPC != null)
					MuestraFrases ();
				else
					MuestraFrasesEspia ();
			} else
				ableInput = true;
		}
				
	}

	void ActualizaInicioDelCanvas(){
		if (currentNPC != null)
			nombre.text = currentNPC.nombreClavePersonaje;
		sentence.gameObject.SetActive (false);
		EligeBoton ();
	}

	void SeleccionaBoton(){
		if (botonHablar.gameObject.activeInHierarchy)
			//Selecciona el primer boton para la navegación
			botonHablar.Select ();
		else
			botonDarObjeto.Select ();
	}

	void DesactivaBotones(){
		botonSalir.gameObject.SetActive (false);
		botonHablar.gameObject.SetActive (false);
		botonCompletarMision.gameObject.SetActive (false);
		botonAceptarMision.gameObject.SetActive (false);
		botonDarObjeto.gameObject.SetActive (false);
	}
	void ActivaBotones(){
		botonSalir.gameObject.SetActive (true);
		botonHablar.gameObject.SetActive (true);
		botonCompletarMision.gameObject.SetActive (true);
		botonAceptarMision.gameObject.SetActive (true);
		botonDarObjeto.gameObject.SetActive (true);
	}

	//Misiones
	public void AceptarMision(){
		currentNPC.isAcepted = true;
		frases = currentNPC.conversacion;
		if (currentNPC.tipoDeMision == Mission.TipoDeMision.Espionaje)
			EmparejaEspia();
		MuestraFrases ();
	}
	public void DarObjeto(){
		//Asignamos el tag
		HUD.tagDarObjeto = currentNPC.pasos.ToArray()[0].tagObjeto;
		//Si existe el objeto necesario en en inventario
		if (HUD.ExistingObject ()) 
			HUD.GiveObject ();
		else
			FinConversacion ();
	}
	public void CompletarMision(){
		if (currentNPC.isComplete) {
			Debug.Log ("Mision Completa");
			frases = new Queue<string>(currentNPC.finMision);
			MuestraFrases ();
			currentNPC.TerminarMision ();
		} else
			FinConversacion ();
	}
	public void EmparejaEspia(){
		GameObject.FindGameObjectWithTag (currentNPC.pasos.ToArray () [0].tagObjeto).AddComponent<Espionaje> ().NPCMision = currentNPC;
	}

	//Dialogo
	public void AbreCierraDialogueCanvas(){
		if (!dialogueBox.activeInHierarchy && !GameManager.instance.ventanaAbierta) {
			GameManager.instance.ventanaAbierta = true;
			dialogueBox.SetActive (true);
			if (currentNPC != null) {
				ActualizaInicioDelCanvas ();
				SeleccionaBoton ();
			}
		} else {
			dialogueBox.SetActive (false);
			if (currentNPC != null)
				dialogueMensaje.gameObject.SetActive (true);
			GameManager.instance.ventanaAbierta = false;
		}
	}

	void EligeBoton(){
		if (currentNPC.isAcepted) {
			botonAceptarMision.gameObject.SetActive (false);
			botonCompletarMision.gameObject.SetActive (false);
			if (currentNPC.tipoDeMision == Mission.TipoDeMision.DarObjeto) {
				botonDarObjeto.gameObject.SetActive (true);
				botonCompletarMision.gameObject.SetActive (false);
			} else if (currentNPC.tipoDeMision == Mission.TipoDeMision.Espionaje) {
				botonDarObjeto.gameObject.SetActive (false);
				if(currentNPC.isComplete)
					botonCompletarMision.gameObject.SetActive (true);
			} else {
				botonAceptarMision.gameObject.SetActive (false);
				botonDarObjeto.gameObject.SetActive (false);
				botonCompletarMision.gameObject.SetActive (false);
			}
		} else {
			botonAceptarMision.gameObject.SetActive (true);
			botonDarObjeto.gameObject.SetActive (false);
			botonCompletarMision.gameObject.SetActive (false);
		}
	}
	public void HablarConNPC(){
		if (!currentNPC.alreadyTalked) {
			frases = currentNPC.dialogo;
			currentNPC.alreadyTalked = true;
		} else
			frases = new Queue<string>(currentNPC.frasesEstandar);			
		MuestraFrases();
	}

	public void MuestraFrasesEspia(){
		if(!isTalking){
			//Desactivar botones
			DesactivaBotones();
			//Mostramos caja de texto
			sentence.gameObject.SetActive(true);
			//Activamos que habla
			isTalking = true;
		}
		ActualizaDialogoEspia ();
	}
	public void ActualizaDialogoEspia(){
		FraseEspia fe = frasesEspia.Dequeue();
		if (fe != null) {
			sentence.text = fe.frase;
			nombre.text = fe.nombre;
		} else
			FinConversacion ();	
	}

	public void MuestraFrases(){
		if(!isTalking){
			//Desactivar botones
			DesactivaBotones();
			//Mostramos caja de texto
			sentence.gameObject.SetActive(true);
			//Activamos que habla
			isTalking = true;
		}
		//Actulizar texto in-game
		ActualizaDialogo ();
	}

	void ActualizaDialogo(){
		//Si la frase que hay es la ultima
  		string aux = frases.Dequeue(); 
		if (aux != null)
			sentence.text = aux;
		else
			FinConversacion ();
	}

	public void FinConversacion(){
		//Escondo el cuadro de texto
		sentence.gameObject.SetActive(false);
		//Muestro botones
		ActivaBotones();
		//Cierro el DialogueBox
		AbreCierraDialogueCanvas();
		//Vacio frases
		frases.Clear();
		//Ya no habla
		isTalking = false;
		ableInput = false;
		//Ya no ventana abierta
		GameManager.instance.ventanaAbierta = false;
	}
}