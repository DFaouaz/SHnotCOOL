using UnityEngine;
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
	public string escenaPrincipal;
	public string EscenaPiso2;
	public KeyCode botonInteractuar;
	[HideInInspector]
	public Entrance lastEntrance;
	[HideInInspector]
	public Vector3 lastPosEntrance;

	// Añadimos las variables necesarias para almacenar información
	[HideInInspector]
	public Vector2 ActualPlayerPosition = Vector2.zero;
	[HideInInspector]
	public Vector2 ActualPlayerDirecction;
	[HideInInspector]
	public string lastScene;
	[HideInInspector]
    public bool habladoNegro=false;
	[HideInInspector]
	public bool ventanaAbierta = false;
	[HideInInspector]
	public bool pauseMode = false;
	[HideInInspector]
	public int dinero = 100;
    
	public int tamInv = 0;
	[HideInInspector]
	public int matematicasScore = 0;
	[HideInInspector]
	public bool finMates = false;
	[HideInInspector]
	public int lenguaScore = 0;
	[HideInInspector]
	public bool finLengua = false;
    [HideInInspector]
    public int GeoScore = 0;
    [HideInInspector]
	public bool finGeo = false;
	[HideInInspector]
    public int historiaScore = 0;
	[HideInInspector]
	public bool finHistoria = false;
    [HideInInspector]
    public int Examen = 0;//0 mates, 1 historia 2 lengua 3 geografia
	[HideInInspector]
	public int numFriends = 0;
	public int maxFriends;
	[HideInInspector]
	public bool thereIsAnInteractiveEvent = false;

    // En cuanto el objeto se active
    void Awake() {
		
		// Si no hay ningún objeto GameManager ya creado
		if (instance == null) {
			// Almacenamos la instancia actual
			instance = this;
			ActualizaDinero ();
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
		SceneManager.LoadScene (escenaPrincipal);
		Time.timeScale = 1;
	}

	//Minijuego de matematicas.
	public void FinExamenMatematicas()
	{
		Text textoFin = GameObject.FindGameObjectWithTag("FinMatematicas").GetComponent<Text>();
		if (matematicasScore >= 5)
			textoFin.text = "Bien Hecho";
		else
			textoFin.text = "Das Asco";

		Invoke ("CambiaAEscenaPrincipal", 0.6f);
		Time.timeScale = 0.2f;
	}

    //Minijuego de Historia
    public void FinExamenHistoria()
    {
		finHistoria = true;
        Invoke("CambiaAEscenaPrincipal",0.1f);
		Time.timeScale = 0.2f;
    }
    //Minijuego de Lengua
    public void FinExamenLengua(){
		finLengua = true;
		Invoke ("CambiaAEscenaPrincipal", 0.3f);
		Time.timeScale = 0.1f;
	}
    //Minijuego de Geografía
    public void FinExamenGeografia(){
		finGeo = true;
        Invoke("CambiaAEscenaPrincipal", 0.3f);
		Time.timeScale = 0.1f;
    }
    /*public void FinPasillos(){
		Invoke("CambiaAEscenaPrincipal", 0.3f);
    }*/
    public void FinMaton(){
		Invoke("CambiaAEscenaPrincipal", 0);
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
	public void Destapa(char letra){
		FindObjectOfType<Palabra> ().Destapa (letra);
	}
	public void UpdateExamsRender(){
		FindObjectOfType<HUDManager> ().UpdateExams ();
	}

	//Amigos
	public void SubeAmigos(){
		numFriends++;
		FindObjectOfType<HUDManager> ().UpdateFriends ();
	}
	public void ActualizaDinero(){
		FindObjectOfType<HUDManager> ().UpdateMoney ();
	}

	//Mueve personajes
	public void MuevePersonajes(){
		if (lastPosEntrance != Vector3.zero) {
			//Movemos los personajes a la ultima entrada
			GameObject.FindGameObjectWithTag ("Player").transform.position = lastPosEntrance;
			GameObject.FindGameObjectWithTag ("Negro").transform.position = lastPosEntrance;
		}
	}
}
