using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Mission : MonoBehaviour {

	[Header("Escribir el nombre clave que se encuentra")]
	[Header("en la codificación del dialogo y misiones")]
	public string nombrePersonaje;
	[HideInInspector]
	public string tituloDeLaMision;
	[HideInInspector]
	public int indiceMision;
	public enum TipoDeMision {None, Espionaje, DarObjeto}
	[HideInInspector]
	public TipoDeMision tipoDeMision;
	public Queue<string> conversacion = new Queue<string>();
	public Queue<Pasos> pasos = new Queue<Pasos>();
	public Queue<string> finMision = new Queue<string> ();
	[HideInInspector]
	public bool isAcepted = false;
	[HideInInspector]
	public bool isComplete = false;
	[HideInInspector]
	public bool isFriend = false;
	[Header("No. de misiones para ser amigos")]
	[SerializeField]
	int maxMision = 0;



	protected virtual void Start(){
		LeeMision ();
	}

	//Busca la mision
	public void LeeMision(){
		StreamReader file = new StreamReader ("Misiones.txt",System.Text.Encoding.Default);
		//Busca el nombre en clave y el numero
		string [] partes = new string[1];
		do{
			partes = file.ReadLine().Split('-');
		}while(!file.EndOfStream && ((partes[0] != nombrePersonaje)
			|| (partes[0] == nombrePersonaje && int.Parse(partes[1]) != indiceMision)
			|| (partes[0] == nombrePersonaje && int.Parse(partes[1]) == indiceMision && !(int.Parse(partes[3]) <= GameManager.instance.trimestre))));	
		if (!file.EndOfStream)
			DecodificaMision (file, partes);
		else {
			tipoDeMision = TipoDeMision.None;
			isAcepted = false;
			isComplete = false;
		}
		file.Close ();
	}
	//Decodifica la mision
	void DecodificaMision(StreamReader file,string [] partes){
		//Tipo de mision: None = 0, Espionaje = 1, DarObjeto = 2.
		tipoDeMision = (TipoDeMision)int.Parse(partes[2]);
		//Asignamos el titulo de la mision si la mision no es de tipo None
		if (tipoDeMision != TipoDeMision.None)
			tituloDeLaMision = partes [4];
		else
			tituloDeLaMision = null;
		//Comenzamos decodificacion de los textos
		if (!file.EndOfStream) {
			//Depende del tipo de mision decofificamos de una forma u otra
			//Hasta que encuentre el final con ""
			string linea = file.ReadLine ();
			while (linea != "" && linea != null && !file.EndOfStream) {
				string[] aux;
				switch (linea) {
				//Usaremos # como marca de que empieza y termina la conversación
				//y ## como marca de que empiezan y terminan los pasos.
				case "#":
					linea = file.ReadLine ();
					do {
						conversacion.Enqueue (linea);
						linea = file.ReadLine ();
					} while(linea != "#");
					break;
					//Para los pasos
				case "##":
					aux = file.ReadLine ().Split (':');
					do {
						Pasos p = new Pasos ();
						p.lineaPaso = aux [0];
						p.tagObjeto = aux [1];
						pasos.Enqueue (p);
						aux = file.ReadLine ().Split (':');
						//Si es de Espionaje, decodificará la conversación que se espia
						DecodConversacionEspia(ref aux, p, file);
					} while(aux [0] != "##");
					break;
				case "###":
					linea = file.ReadLine ();
					do {
						finMision.Enqueue (linea);
						linea = file.ReadLine ();
					} while(linea != "###");
					break;
				}
				linea = file.ReadLine ();
			}
			conversacion.Enqueue (null);
			finMision.Enqueue (null);
			pasos.Enqueue (null);
		} else
			tipoDeMision = TipoDeMision.None;
	}
	//Vacia las listas de pasos, de conversación, finMision.
	void VaciaListas(){
		conversacion.Clear ();
		pasos.Clear ();
		finMision.Clear ();
	}
	//Carga la siguiente mision
	void CargarSiguienteMision(){
		if (tipoDeMision != TipoDeMision.None) {
			indiceMision++;
			VaciaListas ();
			LeeMision ();
			if (tipoDeMision != TipoDeMision.None) {
				isComplete = false;
				isAcepted = false;
			}
		}
	}

	void DecodConversacionEspia(ref string[] partes, Pasos p, StreamReader file){
		if(partes[0] == "--"){
		partes = file.ReadLine ().Split (':');
			while (!file.EndOfStream && partes [0] != "--") {
				FraseEspia fr = new FraseEspia ();
				fr.nombre = partes [0];
				fr.frase = partes [1];
				p.frasesEspia.Enqueue (fr);
				partes = file.ReadLine ().Split (':');
			}
			partes = file.ReadLine ().Split (':');
			p.frasesEspia.Enqueue (null);
		}
	}

	public void TerminarMision(){
		MissionManager.instance.EliminaMision (this);
		CargarSiguienteMision ();
		CheckFriendship ();
	}
	void CheckFriendship(){
		if (maxMision <= indiceMision && !isFriend) {
			isFriend = true;
			if (nombrePersonaje == "Pijas" || nombrePersonaje == "Emos")
				GameManager.instance.SubeAmigos();
			GameManager.instance.SubeAmigos();
		}
	}
}
