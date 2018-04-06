using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour {

	[SerializeField]
	Slot [] slots;
	int indice;
	public CanvasRenderer inventory;
	public KeyCode teclaParaAbrirYCerrar;
	public KeyCode teclaCoger;
	public Text mensajeCoger;
	public Text mensajeSustitucion;
	public Text mensajeNoSustitucion;
	public Sprite Aprobado;
	public Sprite Suspenso;
	//Hace referencia al ultimo objeto que se ha tocado
	[HideInInspector]
	public Coleccionable objeto;
	bool huecoNegro = false;
	[HideInInspector]
	public bool modoSustitucion=false;
	[HideInInspector]
	public bool modoDarObjeto = false;
	public Sprite imagenDeVacio, imagenDeBloqueo;
	[HideInInspector]
	public KeyCode aux;
	//PARA MISIONES
	[HideInInspector]
	public string tagDarObjeto;




	void Start () {
		modoDarObjeto = false;
		mensajeCoger.text = "Pulsar " + teclaCoger.ToString () + " para coger el objeto.";
		InicializeSlots ();
		inventory.gameObject.SetActive (false);
		mensajeCoger.gameObject.SetActive (false);
		mensajeSustitucion.gameObject.SetActive (false);
		mensajeNoSustitucion.gameObject.SetActive (false);
	}


	void Update () {
		if (!GameManager.instance.pauseMode) {
			CheckInputOpenCloseInventory ();
			CheckInputObject ();
			//SUPER MEGA PROVISIONAL
			CheckNegro (); 
		}
	}     

	//Guarda el objeto
	void SaveObject(){
		//Si hay espacio, guardalo
		if (isEmpty ()) {
			slots [indice].objeto = objeto.gameObject;
			slots [indice].nombre = objeto.NombreColeccionable;
			slots [indice].imagenObjeto = objeto.imagenRepresentacion;
			slots [indice].UpdateRender ();
			objeto.gameObject.SetActive (false);
		} else {
			//Abrir el inventario con el mensaje de sustituir para que el jugador diga que objeto quiere sustituir
			mensajeSustitucion.gameObject.SetActive (true);
			mensajeNoSustitucion.gameObject.SetActive (false);
			inventory.gameObject.SetActive (true);
			modoSustitucion = true;
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
		
	bool wholeEmpty(){
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
			if (slots [indice].objeto != null && slots [indice].objeto.tag == tagDarObjeto)
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
				mensajeCoger.gameObject.SetActive (true);
				if (Input.GetKeyDown (teclaCoger))
					SaveObject ();
			} else
				mensajeCoger.gameObject.SetActive (false);
		}
	//Comprueba si el negro ya esta desbloqueado
	void CheckNegro(){
		if (GameManager.instance.habladoNegro && !huecoNegro && GameManager.instance.tamInv < 4) {	//Cambiar porque es PROVISIONAL
			GameManager.instance.tamInv++;
			InicializeSlots ();
			huecoNegro = true;
		}
	}


	public void AbreYCierraInventario(){
		if (!inventory.gameObject.activeInHierarchy && !GameManager.instance.ventanaAbierta) {
			inventory.gameObject.SetActive (true);
			GameManager.instance.ventanaAbierta = true;
			//Selecciona el primer Slot en el inventario
			slots [0].boton.Select ();
			mensajeNoSustitucion.gameObject.SetActive (true);
		} else {
			if (mensajeSustitucion.IsActive ()) {
				mensajeSustitucion.gameObject.SetActive (false);
				modoSustitucion = false;
			}
			GameManager.instance.ventanaAbierta = false;
			inventory.gameObject.SetActive (false);
			mensajeNoSustitucion.gameObject.SetActive (false);
		}
	}
	

	//Para implementar en las misiones
	public void GiveObject ()
	{
		if (!wholeEmpty ()) {
			aux = teclaParaAbrirYCerrar;
			teclaParaAbrirYCerrar = KeyCode.None;
			DialogueManager.instance.AbreCierraDialogueCanvas ();
			AbreYCierraInventario ();
			modoDarObjeto = true;
		}            
	}
}









