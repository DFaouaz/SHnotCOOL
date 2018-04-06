using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Mission : MonoBehaviour {

	public string nombreClavePersonaje;
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
		if (!file.EndOfStream) {
			//Depende del tipo de mision decofificamos de una forma u otra
			//Hasta que encuentre el final con ""
			string linea = file.ReadLine ();
			while (linea != "" && linea != null && !file.EndOfStream) {
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
				case "##":
					string[] aux = file.ReadLine ().Split (':');
					Pasos p = new Pasos ();
					do {
						p.lineaPaso = aux [0];
						p.tagObjeto = aux [1];
						pasos.Enqueue (p);
						aux = file.ReadLine ().Split (':');
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


	public void TerminarMision(){
		isComplete = true;
		CargarSiguienteMision ();
	}
}
