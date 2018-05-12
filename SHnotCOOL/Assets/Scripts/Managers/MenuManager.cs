using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour {

	public string EscenaPredeterminada;
	public Button /*botonReanudar,*/ botonNuevaPartida, botonOpciones;
	public GameObject botones;
	public CanvasRenderer /*panelNuevaPartida, panelReanudar,*/panelOpciones;
	//public BotonArchivo botonArchivo;
	RectTransform content;
	/*[HideInInspector]
	public string fileName;
	[HideInInspector]
	public BotonArchivo botonRef;
	string [] files;*/

	void Start(){
		//Asignamos a los elementos
		//content = panelReanudar.GetComponentInChildren<ScrollRect>().content;
		//Buscamos archivos existentes
		//BuscarArchivos();
		//Creamos tantos botonArchivo como archivos y cambiamos Scroll.Size a 1 por archivo
		//CreaBotonArchivo();
		//Desactiva los paneles
		//panelNuevaPartida.gameObject.SetActive (false);
		//panelReanudar.gameObject.SetActive (false);
		panelOpciones.gameObject.SetActive (false);
		botonNuevaPartida.Select ();
	}



	//Metodos para ReanudarPartida()
	//Reanuda la partida cargando el archivo de guardado y cargandolo en todos los manager
	public void ReanudaPartida(){
		//Lee el archivo
		////////////

		////////////

		//Carga los datos del archivo
		////////////

		////////////
		 
		//Carga la escena correspondiente

	}

	public void ActivaPanelReanudar(){
		//Desactiva botones
		botones.gameObject.SetActive(false);
		//Saca el panel de reanudar
		//panelReanudar.gameObject.SetActive(true);
	}

	//Lee archivo y lo decodifica
	public void AceptaReanudar(){
		//StreamReader archivoLeido = new StreamReader (botonRef.ruta);
		//Decodificacio
		///////////////

		///////////////
		//Asignamos a los distintos Managers
		///////////////

		///////////////
		//archivoLeido.Close();
	}

	public void CancelarReanudar(){
		//Desactiva panelReanudar
		//panelReanudar.gameObject.SetActive(false);
		//botonRef = null;
		//Activa botones
		botones.gameObject.SetActive(true);
		//botonReanudar.Select ();
	}

	//Metodos para NuevaPartida()
	//Crea los archivos necesarios para empezar a guardar la partida
	public void NuevaPartida(){
		//CrearArchivoNuevo (fileName);
		SceneManager.LoadScene (EscenaPredeterminada);
	}

	public void ActivaPanelNuevaPartida(){
		//Desactiva botones
		botones.gameObject.SetActive(false);
		//Saca pantalla para pedir nombre
		//panelNuevaPartida.gameObject.SetActive(true);
	}

	void CrearArchivoNuevo(string fileName){
		StreamWriter nuevoArchivo = new StreamWriter (Application.dataPath + @"\" + "fileName." + fileName + "." + System.DateTime.Today.Day + "." 
			+ System.DateTime.Today.Month + "." + System.DateTime.Today.Year + "." + System.DateTime.Now.TimeOfDay.Hours + "." + System.DateTime.Now.TimeOfDay.Minutes + ".txt");
		//Codificacion a continuacion (AUN NO DECIDIDA)
		/////////////////

		/////////////////
		nuevoArchivo.Close();
		Debug.Log("Archivo creado con nombre " + fileName + "." + System.DateTime.Today.Day + "." 
			+ System.DateTime.Today.Month + "." + System.DateTime.Today.Year + "." + System.DateTime.Now.TimeOfDay.Hours + "." + System.DateTime.Now.TimeOfDay.Minutes + ".txt");
	}

	//Cierra el juego por completo
	public void Salir(){
		Application.Quit ();
	}

	//Vuelve al menuPrincipal
	public void CancelarNuevaPartida(){
		//panelNuevaPartida.gameObject.SetActive (false);
		//panelNuevaPartida.GetComponentInChildren<InputField>().text = "";
		botones.SetActive (true);
		botonNuevaPartida.Select ();
	}

	//Crea el archivo con el nombre
	public void AceptarNuevaPartida(){
		//fileName = panelNuevaPartida.GetComponentInChildren<InputField> ().text;
		NuevaPartida ();
	}


	//Metodos generales
	void BuscarArchivos(){
		//files = Directory.GetFiles (Application.dataPath,"fileName.*.txt");
	}

	//Crea botonArchivos
	/*void CreaBotonArchivo(){
		//Si existen archivos
		if (files.Length > 0) {
			//Instanciamos botones y ajustamos.
			for (int i = 0; i < files.Length; i++) {
				BotonArchivo clone = Instantiate <BotonArchivo>(botonArchivo, content.transform);
				clone.GetComponent<RectTransform> ().anchoredPosition -= new Vector2 (0, 75 * i);
				string[] datos = files [i].Split ('.');
				clone.nombre = datos [1];
				clone.fecha = datos [2] + "/" + datos [3] + "/" + datos [4];
				clone.hora = datos [5] + ":" + datos [6];
				clone.ruta = Application.dataPath + @"\" + "fileName." + datos[1] + "." + datos[2] + "."
					+ datos[3] + "." + datos[4] + "." + datos[5] + "." + datos[6] + ".txt";
				clone.ActualizaDatos ();
			}
		}
		//Ajusta la barra
		content.sizeDelta = new Vector2 (0, 75 * (files.Length + 1));
	}*/

	//Resetea para cuando se elimine un archivo
	public void ReseteaLista(){
		Debug.Log ("Archivo borrado");
		//Borra todos los botones
		Button[] aux = content.GetComponentsInChildren<Button>();
		foreach (Button item in aux) {
			Destroy(item.gameObject);
		}
		BuscarArchivos ();
		//CreaBotonArchivo ();
	}

	//Métodos para el panel de opciones
	public void AbreOpciones(){
		botones.SetActive (false);
		panelOpciones.gameObject.SetActive (true);
	}
	public void CierraOpciones(){
		botones.SetActive (true);
		panelOpciones.gameObject.SetActive (false);
		botonOpciones.Select ();
	}
}
