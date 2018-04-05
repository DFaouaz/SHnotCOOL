using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public string nombre;
    [HideInInspector]
    public GameObject objeto;
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
				objeto = im.objeto.gameObject;
				nombre = im.objeto.NombreColeccionable;
				imagenObjeto = im.objeto.imagenRepresentacion;
				//Desactivamos el objeto
				im.objeto.gameObject.SetActive (false);
				im.modoSustitucion = false;
				im.mensajeSustitucion.gameObject.SetActive (false);
				im.inventory.gameObject.SetActive (false);
			} else if (im.modoDarObjeto) {
				Text textos = GetComponentInChildren<Text> ();
				textos.text = "Vacio";
				objeto = null;                
				im.modoDarObjeto = false;
				im.inventory.gameObject.SetActive (false);
				GameManager.instance.darObjeto = false;
				GameManager.instance.ventanaAbierta = false;
				im.teclaParaAbrirYCerrar = im.aux;
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
    public void CleanSlot()
    {
        if (estado == Estado.Desbloqueado && objeto != null)
        {
            //Dropeamos en la posicion
            objeto.transform.position = GameManager.instance.ActualPlayerPosition;
            objeto.SetActive(true);
			textoBoton.text = "Vacio";
            objeto = null;
        }
    }

    //Renderiza el estado actual del slot
    public void UpdateRender(){
		if (estado == Estado.Desbloqueado) {
			if (textoBoton.text != nombre && objeto != null) {
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
}