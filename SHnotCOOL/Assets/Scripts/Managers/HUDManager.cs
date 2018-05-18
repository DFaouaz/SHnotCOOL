using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour {

	[SerializeField]
	Slot [] slots = null;
	[SerializeField]
	ExamenSlot [] exams = null;
	int indice;
	public CanvasRenderer inventory;
	public KeyCode teclaParaAbrirYCerrar;
	public Text mensajeSustitucion;
	public Text mensajeNoSustitucion;
	public Sprite Aprobado;
	public Sprite Suspenso;
	[HideInInspector]
	public Sprite Nada;
	//Hace referencia al ultimo objeto que se ha tocado
	[HideInInspector]
	public Coleccionable objeto;
	[HideInInspector]
	public bool modoSustitucion=false;
	[HideInInspector]
	public bool modoDarObjeto = false;
	public Sprite imagenDeVacio, imagenDeBloqueo;
	public Text friendsCount;
	public Text moneyCount;
	public CanvasRenderer negroCount;
	//PARA MISIONES
	[HideInInspector]
	public string tagDarObjeto;




	void Start () {
		modoDarObjeto = false;
		InicializeSlots ();
		UpdateExams ();
		inventory.gameObject.SetActive (false);
		mensajeSustitucion.gameObject.SetActive (false);
		mensajeNoSustitucion.gameObject.SetActive (false);
		negroCount.gameObject.SetActive (false);
	}


	void Update () {
		CheckInputOpenCloseInventory ();
		CheckInputObject ();
        UpdateMoney();
	}

	//Guarda el objeto
	void SaveObject(){
		//Si hay espacio, guardalo
		if (isEmpty ()) {
			slots [indice].objeto = objeto;
			slots [indice].nombre = objeto.NombreColeccionable;
			slots [indice].imagenObjeto = objeto.imagenRepresentacion;
			slots [indice].UpdateRender ();
			objeto.isTaken = true;
			objeto.gameObject.SetActive (false);
		} else {
			//Abrir el inventario con el mensaje de sustituir para que el jugador diga que objeto quiere sustituir
			AbreYCierraInventario();
			mensajeSustitucion.gameObject.SetActive (true);
			mensajeNoSustitucion.gameObject.SetActive (false);
			modoSustitucion = true;
		}
	}

	public bool BuyObject(Coleccionable compra,PersistantObjects padre){
		//Si hay espacio, guardalo
		if (isEmpty ()) {
			GameObject comprado;
			Coleccionable compradoCaract;
			comprado=Instantiate (compra.gameObject, padre.gameObject.transform);
			compradoCaract = comprado.GetComponent<Coleccionable> ();
			slots [indice].objeto = compradoCaract;
			slots [indice].nombre = compradoCaract.NombreColeccionable;
			slots [indice].imagenObjeto = compradoCaract.imagenRepresentacion;
			slots [indice].UpdateRender ();
			compradoCaract.isTaken = true;
			compradoCaract.currentScene = "Escuela";
			compradoCaract.ActualizaObjeto ();
			return true;
			//objeto.gameObject.SetActive (false);
		} else {
			return false;
		}
	}
	//Devuelve true si hay espacio en el inventario
	bool isEmpty(){
		indice = 0;
		while (indice < slots.Length && slots [indice].objeto != null && slots [indice].estado != Slot.Estado.Bloqueado)
			indice++;
		if (indice >= slots.Length || slots [indice].estado == Slot.Estado.Bloqueado)
			return false;
		else
			return true;
	}
		
	public bool wholeEmpty(){
		indice = 0;
		while (indice < slots.Length && (slots [indice].objeto == null || slots [indice].estado == Slot.Estado.Bloqueado))
			indice++;
		return indice >= slots.Length;
	}

	//Devuelve true si ya hay referencia del mismo objeto en el inventario
	public bool ExistingObject(){
		indice = 0;
		bool exists = false;
		while (indice < slots.Length && !exists)
			if (slots [indice].objeto != null && slots [indice].nombre == tagDarObjeto)
				exists = true;
			else
				indice++;
		return exists;
	}

	//Para abrir y cerrar la interfaz del inventario
	void CheckInputOpenCloseInventory(){
		if (Input.GetKeyDown (teclaParaAbrirYCerrar)) {
			AbreYCierraInventario ();
		}
	}

	//Inicializa los slots
	void InicializeSlots(){
		inventory.gameObject.SetActive (true);
		for (int i = 0; i < slots.Length; i++) {
			if (i < GameManager.instance.tamInv)
				slots [i].estado = Slot.Estado.Desbloqueado;
			else
				slots [i].estado = Slot.Estado.Bloqueado;
			slots [i].UpdateRender ();
		}
		inventory.gameObject.SetActive (false);
	}


		//Comprueba si hay objeto y lo guarda en el inventario
		void CheckInputObject(){
		if (objeto != null) {
			MessageManager.instance.ShowMessage ("Pulsar " + GameManager.instance.botonInteractuar.ToString () + " para coger el objeto.");
			if (Input.GetKeyDown (GameManager.instance.botonInteractuar))
				SaveObject ();
		}
	}
	//Comprueba si el negro ya esta desbloqueado
	public void ActivateNegroSlot(){
		if (GameManager.instance.habladoNegro) {
			UnlockSlot ();
			UpdateFriends ();
		}
	}

	public void UnlockSlot(){
		int index = -1;
		if (thereIsSlotBlocked (out index)) {
			slots [index].estado = Slot.Estado.Desbloqueado;
			slots [index].UpdateRender ();
			GameManager.instance.tamInv++;
		}			
	}

	bool thereIsSlotBlocked(out int index){
		int i = 0;
		while (i < slots.Length && slots [i].estado != Slot.Estado.Bloqueado)
			i++;
		if (i < slots.Length) {
			index = i;
			return true;
		} else {
			index = -1;
			return false;
		}
	}


	public void AbreYCierraInventario(){
		if (!inventory.gameObject.activeInHierarchy && !DialogueManager.instance.dialogueBox.activeInHierarchy && !GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta) {
			inventory.gameObject.SetActive (true);
			GameManager.instance.ventanaAbierta = true;
			//Selecciona el primer Slot en el inventario
			slots [0].boton.Select ();
			mensajeNoSustitucion.gameObject.SetActive (true);
		} else if(inventory.gameObject.activeInHierarchy && !GameManager.instance.pauseMode){
			if (mensajeSustitucion.IsActive ()) {
				mensajeSustitucion.gameObject.SetActive (false);
				modoSustitucion = false;
			}
			if(!DialogueManager.instance.dialogueBox.activeInHierarchy)
				GameManager.instance.ventanaAbierta = false;
			inventory.gameObject.SetActive (false);
			mensajeNoSustitucion.gameObject.SetActive (false);
		}
	}
	

	//Para implementar en las misiones
	public void GiveObject () {
		if (!wholeEmpty ()) {
			DialogueManager.instance.AbreCierraDialogueCanvas (); 					//Cierra
			MessageManager.instance.CloseMessage();	//Desactiva el mensaje
			AbreYCierraInventario ();
			modoDarObjeto = true;
		}            
	}

	//Actualiza el aspecto de los examenes
	public void UpdateExams(){
		//Mates
		if (GameManager.instance.finMates && GameManager.instance.trimestre >= 1)
			exams [0].UpdateRender (Aprobado);
		else if (!GameManager.instance.finMates && GameManager.instance.trimestre >= 1)
			exams [0].UpdateRender (Suspenso);
		else
			exams [0].UpdateRender (Nada);
		//Historia
		if (GameManager.instance.finHistoria && GameManager.instance.trimestre >= 1)
			exams [1].UpdateRender (Aprobado);
		else if (!GameManager.instance.finHistoria && GameManager.instance.trimestre >= 1)
			exams [1].UpdateRender (Suspenso);
		else
			exams [1].UpdateRender (Nada);
		//Geografia
		if (GameManager.instance.finGeo && GameManager.instance.trimestre >= 3)
			exams [2].UpdateRender (Aprobado);
		else if (!GameManager.instance.finGeo && GameManager.instance.trimestre >= 3)
			exams [2].UpdateRender (Suspenso);
		else
			exams [2].UpdateRender (Nada);
		//Lengua
		if (GameManager.instance.finLengua && GameManager.instance.trimestre >= 2)
			exams [3].UpdateRender (Aprobado);
		else if (!GameManager.instance.finLengua && GameManager.instance.trimestre >= 2)
			exams [3].UpdateRender (Suspenso);
		else
			exams [3].UpdateRender (Nada);
	}

	public void UpdateFriends(){
		if (GameManager.instance.habladoNegro && !negroCount.gameObject.activeInHierarchy)
			negroCount.gameObject.SetActive (true);
		friendsCount.text = GameManager.instance.numFriends.ToString();
		if (GameManager.instance.emosMuerto)
			friendsCount.text += " - 2";
	}
	public void UpdateMoney(){
		moneyCount.text = GameManager.instance.dinero.ToString();
	}
}









