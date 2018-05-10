using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjetoEnVenta : MonoBehaviour {
	public HUDManager HUD;
	public Button boton;
	public Coleccionable objeto;
	public Text texto_boton;
	public PersistantObjects Padre_;
	public int coste_;
	// Use this for initialization
	void Start(){
		texto_boton.text = objeto.NombreColeccionable + " " + coste_+"€";
	}

	public void BotonPulsado(){		
		if (HUD.BuyObject (objeto,Padre_) && coste_ <= GameManager.instance.dinero) {
			GameManager.instance.dinero -= coste_;
			GameManager.instance.ActualizaDinero ();
			texto_boton.text = "Agotado";
			boton.FindSelectableOnDown ().Select ();
			boton.interactable = false;
		}
	}
}
