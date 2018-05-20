using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour {

	public HUDManager HUD;
	public Button botonSalir, botonHablar, botonDarObjeto, botonAceptarMision, botonCompletarMision;
    public Text nombre;
    public Text sentence;
    public GameObject dialogueBox;
	public GameObject missionPanel;
	[HideInInspector]
	public NPC currentNPC;
	[HideInInspector]
	public bool isTalking = false;
	//Para corregir el input del Espacio
	[HideInInspector]
	public bool ableInput = false;
    // cola de strings( el primero en meterse es el primero en salir
	public Queue<string> frases;
	public Queue<FraseEspia> frasesEspia;
	bool seeingMission = false;
	public Sprite exclamacion, interrogacion;


	public static DialogueManager instance;

	void Awake(){
		if (instance == null){
			instance = this;
		}else
			Destroy(this);
	}

    void Start(){
		dialogueBox.SetActive (false);
		missionPanel.SetActive (false);
		InicializaMissionMarks ();
    }

    void Update() {
		CheckInputDialogue ();
    }
		
	//Comprueba si el ususario da la orden de comenzar la conversación
	void CheckInputDialogue(){
		
		if (Input.GetKeyDown (GameManager.instance.botonInteractuar) && !isTalking && currentNPC != null) {
			MessageManager.instance.CloseMessage();
			AbreCierraDialogueCanvas ();
		} else if (isTalking && (Input.GetKeyDown (KeyCode.Space)
			|| Input.GetKeyDown (KeyCode.Mouse0) || Input.GetKeyDown (GameManager.instance.botonInteractuar))) { 
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
			nombre.text = currentNPC.nombrePersonaje;
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
	public void VerMision(){
		seeingMission = true;
		frases = new Queue<string>(currentNPC.conversacion);
		MuestraFrases ();
	}
	public void DarObjeto(){
		if (currentNPC.nombrePersonaje != "Negro")
			HUD.tagDarObjeto = string.Copy (currentNPC.pasos.ToArray () [0].tagObjeto);
		HUD.GiveObject ();
	}
	public void CompletarMision(){
		if (currentNPC.isComplete) {
			Debug.Log ("Mision Completa");
			frases = new Queue<string>(currentNPC.finMision);
			MuestraFrases ();
			currentNPC.TerminarMision ();
		} else
			FinConversacion ();
		SelectMissionMark ();
	}
	public void EmparejaEspia(){
		Espionaje go = GameObject.FindGameObjectWithTag (currentNPC.pasos.ToArray () [0].tagObjeto).AddComponent<Espionaje> ();
		go.NPCMision = currentNPC;
		go.scene = "Escuela";
	}

	//Dialogo
	public void AbreCierraDialogueCanvas(){
		if (!dialogueBox.activeInHierarchy && !GameManager.instance.ventanaAbierta) {
			transform.SetAsLastSibling ();
			GameManager.instance.ventanaAbierta = true;
			dialogueBox.SetActive (true);
			MessageManager.instance.CloseMessage();
			if (currentNPC != null) {
				ActualizaInicioDelCanvas ();
				SeleccionaBoton ();
			}
		} else if (dialogueBox.activeInHierarchy && !GameManager.instance.pauseMode) {
			dialogueBox.SetActive (false);
			InputConfiguration.DeselectButton ();
			if (currentNPC != null)
				MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para interactuar.");
			GameManager.instance.ventanaAbierta = false;
		}
	}

	void EligeBoton(){
		botonAceptarMision.gameObject.SetActive (false);
		botonCompletarMision.gameObject.SetActive (false);
		botonDarObjeto.gameObject.SetActive (false);
		if (currentNPC.isAcepted) {
			if (currentNPC.tipoDeMision == Mission.TipoDeMision.DarObjeto) {
				if (currentNPC.nombrePersonaje == "Negro"){
					if (currentNPC.alreadyTalked && !HUD.wholeEmpty()) {
						botonDarObjeto.gameObject.SetActive (true);
						HUD.tagDarObjeto = null;
					}
				}else if(currentNPC.pasos.ToArray () [0] != null){
					HUD.tagDarObjeto = string.Copy(currentNPC.pasos.ToArray () [0].tagObjeto);					
					if (HUD.ExistingObject ()) {
						botonDarObjeto.gameObject.SetActive (true);
						HUD.tagDarObjeto = null;
					}
				}
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
			if(currentNPC.nombrePersonaje != "Negro" && currentNPC.alreadyTalked && currentNPC.tipoDeMision != Mission.TipoDeMision.None)
				botonAceptarMision.gameObject.SetActive (true);
			botonDarObjeto.gameObject.SetActive (false);
			botonCompletarMision.gameObject.SetActive (false);
		}
	}
	public void HablarConNPC(){
		if (!currentNPC.alreadyTalked) {
			frases = currentNPC.dialogo;
			currentNPC.alreadyTalked = true;
			//Si es el negro
			if (currentNPC.nombrePersonaje == "Negro") {
				GameManager.instance.habladoNegro = true;
				FindObjectOfType<HUDManager> ().ActivateNegroSlot ();
			}
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
		if (frasesEspia.ToArray () [0] != null) {
			FraseEspia fe = frasesEspia.Dequeue ();
			if (fe != null) {
				sentence.text = fe.frase;
				nombre.text = fe.nombre;
			} else
				FinConversacion ();	
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
		else if (seeingMission) {
			isTalking = false;
			VerPanelDeMision ();
		}
		else
			FinConversacion ();
	}

	public void AceptarMision(){
		currentNPC.isAcepted = true;
		if (currentNPC.tipoDeMision == Mission.TipoDeMision.Espionaje)
			EmparejaEspia();
		MissionManager.instance.AñadirMision (currentNPC);
		seeingMission = false;
		CerrarPanelDeMision ();
		SelectMissionMark ();
	}

	void VerPanelDeMision(){
		missionPanel.SetActive (true);
		DesactivaBotones ();
		sentence.gameObject.SetActive(false);
		missionPanel.GetComponentInChildren<Button> ().Select ();
	}

	public void CerrarPanelDeMision(){
		seeingMission = false;
		missionPanel.SetActive (false);
		ActivaBotones ();
		ActualizaInicioDelCanvas ();
		SeleccionaBoton ();
		sentence.gameObject.SetActive(false);
	}

	void CerrarDialogueBox(){
		//Escondo el cuadro de texto
		sentence.gameObject.SetActive(false);
		//Muestro botones
		ActivaBotones();
		//Cierro el DialogueBox
		CerrarPanelDeMision ();
		AbreCierraDialogueCanvas();
		//Vacio frases
		frases.Clear();
		//Ya no habla
		isTalking = false;
		ableInput = false;
		//Ya no ventana abierta
		GameManager.instance.ventanaAbierta = false;
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
		if (currentNPC != null) {
			//Vuelve a abrir el DialogueCanvas
			AbreCierraDialogueCanvas();
			botonHablar.Select ();
			SelectMissionMark ();
		}
	}

	void SelectMissionMark(){
		if (currentNPC.missionMark != null) {
			if (!currentNPC.isAcepted && currentNPC.tipoDeMision != Mission.TipoDeMision.None)
				currentNPC.missionMark.UpdateRender (MissionMark.Est.Exclamacion);
			else if (currentNPC.isAcepted && currentNPC.tipoDeMision != Mission.TipoDeMision.None)
				currentNPC.missionMark.UpdateRender (MissionMark.Est.Interrogacion);
			else
				currentNPC.missionMark.UpdateRender (MissionMark.Est.None);
		}
	}

	void InicializaMissionMarks(){
		NPC[] n = FindObjectsOfType<NPC> ();
		foreach (NPC i in n) {
			if (i.tipoDeMision != Mission.TipoDeMision.None && i.missionMark != null)
				i.missionMark.UpdateRender (MissionMark.Est.Exclamacion);
			else if(i.missionMark != null)
				i.missionMark.UpdateRender (MissionMark.Est.None);
		}
	}
}