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
    public Estado estado;
    HUDManager im;

    void Start()
    {
        im = FindObjectOfType<HUDManager>();
    }

    //Espera el click a uno de los botones del inventario para sustituir
    public void ClickSustituto()
    {
        if (estado == Estado.Desbloqueado && objeto != null)
        {
            //Dropeamos en la posicion
            
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
				im.teclaParaAbrirYCerrar = im.aux;
			} else
				CleanSlot ();
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
            Text textos = GetComponentInChildren<Text>();
            textos.text = "Vacio";
            objeto = null;
        }
    }

    //Renderiza el estado actual del slot
    public void UpdateRender()
    {
        if (estado == Estado.Desbloqueado)
        {
            Text textos = GetComponentInChildren<Text>();
            if (textos.text != nombre && objeto != null)
            {
                textos.text = nombre;
                gameObject.GetComponent<Image>().sprite = imagenObjeto;
            }
            else
            {
                textos.text = "Vacio";
                gameObject.GetComponent<Image>().sprite = im.imagenDeVacio;
            }
        }
        else
            gameObject.GetComponent<Image>().sprite = im.imagenDeBloqueo;

    }
}