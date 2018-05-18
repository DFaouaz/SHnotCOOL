using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public string nombre;
    [HideInInspector]
	public Coleccionable objeto = null;
    [HideInInspector]
    public Sprite imagenObjeto;
    [HideInInspector]
    public enum Estado
    {
        Desbloqueado, Bloqueado
    }
    [HideInInspector]
	public Estado estado = Estado.Bloqueado;
	[HideInInspector]
	public Button boton;
	Image imagenBoton;
	Text textoBoton;
    HUDManager im;

	void Awake()
    {
        im = FindObjectOfType<HUDManager>();
		boton = GetComponent<Button> ();
		imagenBoton = GetComponent<Image> ();
		textoBoton = GetComponentInChildren<Text> ();
    }

    //Espera el click a uno de los botones del inventario para sustituir
	public void ClickSustituto() {
		if (estado == Estado.Desbloqueado) {            
			if (im.modoSustitucion) {
				CleanSlot ();
				//Asignamos el nuevo objeto
				objeto = im.objeto;
				nombre = im.objeto.NombreColeccionable;
				im.objeto.isTaken = true;
				imagenObjeto = im.objeto.imagenRepresentacion;
				//Desactivamos el objeto
				im.objeto.gameObject.SetActive (false);
				im.modoSustitucion = false;
				im.AbreYCierraInventario ();
			} else if (im.modoDarObjeto) {
				if (DialogueManager.instance.currentNPC.nombrePersonaje == "Negro") {
					if (objeto != null) {
						Text textos = GetComponentInChildren<Text> ();
						if (isBocata ())
							im.UnlockSlot ();
						textos.text = "Vacio";
						objeto.ObjetoUsado ();
						objeto.ActualizaObjeto ();
						objeto = null;
						im.AbreYCierraInventario ();	//Cierra
						DialogueManager.instance.HUD.tagDarObjeto = null;
						DialogueManager.instance.AbreCierraDialogueCanvas ();	//Abre
						DialogueManager.instance.frases = new Queue<string> (DialogueManager.instance.currentNPC.finMision);
						DialogueManager.instance.MuestraFrases ();
						DialogueManager.instance.currentNPC.indiceMision--;
						DialogueManager.instance.currentNPC.TerminarMision ();
						DialogueManager.instance.currentNPC.isAcepted = true;
					} else {
						im.AbreYCierraInventario ();
						DialogueManager.instance.AbreCierraDialogueCanvas ();
						DialogueManager.instance.FinConversacion ();
					}
				} else if (objeto != null && nombre == im.tagDarObjeto) {
					//Le damos el objeto
					objeto.ObjetoUsado();
					Text textos = GetComponentInChildren<Text> ();
					textos.text = "Vacio";
					objeto = null; 
					//Por precaucion
					 if (DialogueManager.instance.currentNPC.nombrePersonaje != "Negro") {
						DialogueManager.instance.currentNPC.pasos.Dequeue ();							//Actualizamos cola
						if (DialogueManager.instance.currentNPC.pasos.ToArray () [0] == null) {			//Si no hay mas pasos
							DialogueManager.instance.frases = new Queue<string> (DialogueManager.instance.currentNPC.finMision);
							im.AbreYCierraInventario ();	//Cierra
							DialogueManager.instance.HUD.tagDarObjeto = null;
							DialogueManager.instance.AbreCierraDialogueCanvas ();	//Abre
							DialogueManager.instance.MuestraFrases ();
							DialogueManager.instance.currentNPC.isComplete = true;
							DialogueManager.instance.currentNPC.TerminarMision ();
						} else {
							MissionManager.instance.ActualizaPasos ((Mission)DialogueManager.instance.currentNPC);
							im.AbreYCierraInventario ();
							DialogueManager.instance.AbreCierraDialogueCanvas ();
							DialogueManager.instance.FinConversacion ();
						}
					}			
				} else {
					im.AbreYCierraInventario ();
					MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para interactuar.");
				}
				im.modoDarObjeto = false;
			} else {
				CleanSlot ();
				//Cerramos las ventanas
				im.mensajeSustitucion.gameObject.SetActive (false);
				im.inventory.gameObject.SetActive (false);
				GameManager.instance.ventanaAbierta = false;
			}
            //Lo actualizamos visualmente
            UpdateRender();
        }
    }

    //Vacia el slot
    public void CleanSlot(){
        if (estado == Estado.Desbloqueado && objeto != null){
            //Dropeamos en la posicion
            objeto.transform.position = GameManager.instance.ActualPlayerPosition;
			objeto.currentScene = SceneManager.GetActiveScene ().name;
			objeto.isTaken = false;
            objeto.gameObject.SetActive(true);
			textoBoton.text = "Vacio";
            objeto = null;
        }
    }

    //Renderiza el estado actual del slot
    public void UpdateRender(){
		if (estado == Estado.Desbloqueado) {
			if (objeto != null) {
				textoBoton.text = nombre;
				imagenBoton.sprite = imagenObjeto;
			} else {
				textoBoton.text = "Vacio";
				imagenBoton.sprite = im.imagenDeVacio;
			}
		} else {
			textoBoton.text = "";
			imagenBoton.sprite = im.imagenDeBloqueo;
		}

    }
	bool isBocata(){
		if (nombre == "Bocata")
			return true;
		return false;
	}
}