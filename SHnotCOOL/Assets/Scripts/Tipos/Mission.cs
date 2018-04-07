using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Mission : MonoBehaviour {

	public string nombreClavePersonaje;
	public string tituloDeLaMision;
	public int indiceMision;
	public enum TipoDeMision {None, Espionaje, DarObjeto}
	public TipoDeMision tipoDeMision;
	public Queue<string> conversacion = new Queue<string>();
	public Queue<Pasos> pasos = new Queue<Pasos>();
	public Queue<string> finMision = new Queue<string> ();
	public bool isAcepted = false;
	public bool isComplete = false;


	protected virtual void Start(){
		LeeMision ();
	}

	//Busca la mision
	void LeeMision(){
		StreamReader file = new StreamReader ("Misiones.txt");
		//Busca el nombre en clave y el numero
		string [] partes;
		do{
			partes = file.ReadLine().Split('-');
		}while(partes[0] == "" || (!file.EndOfStream 
			&&(partes[0] != nombreClavePersonaje 										//partes[0] = nombreClavePersonaje
			|| int.Parse(partes[1]) != indiceMision)));									//partes[1] = indiceMision
		//Si lo encuentra, lo decodifica												//partes[2] = tipoDeMision
		DecodificaMision (file, partes);
		file.Close ();
	}
	//Decodifica la mision
	void DecodificaMision(StreamReader file,string [] partes){
		//Tipo de mision: None = 0, Espionaje = 1, DarObjeto = 2.
		tipoDeMision = (TipoDeMision)int.Parse(partes[2]);
		//Asignamos el titulo de la mision si la mision no es de tipo None
		if (tipoDeMision != TipoDeMision.None)
			tituloDeLaMision = partes [3];
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
			//PROVISIONAL PARA AÑADIR UN PASOS NULL A LA COLA
		} else
			tipoDeMision = TipoDeMision.None;
	}
	//Vacia las listas de pasos y de conversación
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
			} else {
				isComplete = true;
				isAcepted = true;
			}
		}
	}

	void DecodConversacionEspia(ref string[] partes, Pasos p, StreamReader file){
		if(partes[0] == "--")
		{
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
		CargarSiguienteMision ();
	}
}
