﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestor del juego (singleton simplificado). Controlará el estado del juego
/// y tendrá métodos llamados desde los distintos objetos que lo hacen avanzar.
/// Debe haber una única instancia. 
/// </summary>
public class GameManager : MonoBehaviour {

	// Creamos una variable estática para almacenar la instancia única
	public static GameManager instance = null;
	//Nombre del piso principal
	public string EscenaPiso1;

	public Vector2 Escena1PlayerPos;

	// Añadimos las variables necesarias para almacenar información
	[HideInInspector]
	public Vector2 ActualPlayerPosition = Vector2.zero;
	[HideInInspector]
	public Vector2 ActualPlayerDirecction;
	[HideInInspector]
	public Vector2 ActualPlayerVelocity;// Ejemplo de variable que podemos querer usar
	[HideInInspector]
    public bool habladoNegro=false;
	[HideInInspector]
    public bool darObjeto = true;
    
	public int tamInv = 0;
	[HideInInspector]
	public int matematicasScore = 0;
	[HideInInspector]
	public int lenguaScore = 0;

	// En cuanto el objeto se active
	void Awake() {
		// Si no hay ningún objeto GameManager ya creado
		if (instance == null) {
			// Almacenamos la instancia actual
			instance = this;
			// Nos aseguramos de no destruir el objeto, es decir, 
			// de que persista, si cambiamos de escena
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			// Si ya existe un objeto GameManager, no necesitamos uno nuevo
			Destroy(this.gameObject);
		}      
	}

	// A partir de aquí añadiríamos los métodos que necesitemos implementar
	// para conseguir las funcionalidades que pretendamos incluir.
	//Métodos generales
	void CambiaAEscenaPrincipal(){
		SceneManager.LoadScene (EscenaPiso1);
	}

	//Minijuego de matematicas.
	public void FinExamenMatematicas()
	{
		Text textoFin = GameObject.FindGameObjectWithTag("FinMatematicas").GetComponent<Text>();
		if (matematicasScore >= 5)
			textoFin.text = "Bien Hecho";
		else
			textoFin.text = "Das Asco";

		Invoke ("CambiaAEscenaPrincipal", 3);
	}

	//Minijuego de Lengua
	public void FinExamenLengua(){
		Invoke ("CambiaAEscenaPrincipal", 3);
	}
	public void SubePuntosLengua(){
		FindObjectOfType<Puntos> ().SubePuntos ();
	}
	public void BajaVidaLengua(){
		FindObjectOfType<VidasLengua> ().BajaVida ();
	}
	public void SubeVidaLengua(){
		FindObjectOfType<VidasLengua> ().SubeVida ();
	}
}
