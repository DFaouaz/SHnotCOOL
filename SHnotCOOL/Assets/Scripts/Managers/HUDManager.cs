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

	void Start () {
		modoDarObjeto = false;
		mensajeCoger.text = "Pulsar " + teclaCoger.ToString () + " para coger el objeto.";
		inventory.gameObject.SetActive (true);
		InicializeSlots ();
		inventory.gameObject.SetActive (false);
		mensajeCoger.gameObject.SetActive (false);
		mensajeSustitucion.gameObject.SetActive (false);
		mensajeNoSustitucion.gameObject.SetActive (false);
	}

	void Update () {
		OpenCloseInventory ();
		CheckInputObject ();
		CheckNegro ();        
	}
	//Guarda el objeto
	void SaveObject(){
		//Si hay espacio, guardalo
		if (isEmpty ()) {
			slots [indice].objeto = objeto.gameObject;
			slots [indice].nombre = objeto.NombreColeccionable;
			slots [indice].imagenObjeto = objeto.imagenRepresentacion;
			slots[indice].UpdateRender();
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
		while (indice < slots.Length && slots[indice].objeto != null && slots[indice].estado != Slot.Estado.Bloqueado)
			indice++;
		if (indice >= slots.Length || slots[indice].estado == Slot.Estado.Bloqueado)
			return false;
		else
			return true;
	} 
    bool wholeEmpty()
    {
            indice = 0;
		    while (indice<slots.Length &&( slots[indice].objeto == null || slots[indice].estado == Slot.Estado.Bloqueado))

                indice++;
            if (indice >= slots.Length)
                return true;
            else
                return false;
    }
	//Devuelve true si ya hay referencia del mismo objeto en el inventario
	bool ExistingObject(){
		indice = 0;
		while (indice < slots.Length && slots [indice].nombre != objeto.NombreColeccionable)
			indice++;
		if (indice >= slots.Length)
			return false;
		else if (slots[indice].nombre == objeto.NombreColeccionable)
			return true;
		return false;
	}
	//Para abrir y cerrar la interfaz del inventario
	void OpenCloseInventory(){
		if (Input.GetKeyDown (teclaParaAbrirYCerrar)) {
			if (!inventory.gameObject.activeInHierarchy) {
				inventory.gameObject.SetActive (true);
				mensajeNoSustitucion.gameObject.SetActive (true);
			}
			else {
				if (mensajeSustitucion.IsActive ()) {
					mensajeSustitucion.gameObject.SetActive (false);
					modoSustitucion = false;
				}
				inventory.gameObject.SetActive (false);
				mensajeNoSustitucion.gameObject.SetActive (false);
			}
		}
	}

	//Inicializa los slots
	void InicializeSlots(){
		inventory.gameObject.SetActive (true);
		for (int i = 0; i < slots.Length; i++) {
			Text textos = slots [i].GetComponentInChildren<Text> ();
			if (i < GameManager.instance.tamInv) {
				textos.text = "Vacio";
				slots [i].estado = Slot.Estado.Desbloqueado;
			} else {
				textos.text = "";
				slots [i].estado = Slot.Estado.Bloqueado;
			}
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


	//Para implementar en las misiones
	public void GiveObject(){
        if (!wholeEmpty())
        {
            aux = teclaParaAbrirYCerrar;
            teclaParaAbrirYCerrar = KeyCode.None;
            GameManager.instance.darObjeto = true;
            inventory.gameObject.SetActive(true);
            modoDarObjeto = true;
        }            
    }
}




