using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]// Permite que otros scripts accedan a esta clase
public class Dialogue
{

    public string nombre;// nombre del NPC con el que se habla

    public string[] frases;// frases que forman el dialogo

}
public class DialogueManager : MonoBehaviour
{

    public GameObject salir;
    public GameObject hablar;
    public GameObject DarObjeto;
    public Text nombre;
    public Text sentence;
    public GameObject dialogueBox;
	public Text dialogueMensaje;
	public KeyCode botonParaHablar;
	[HideInInspector]
	public NPC currentNPC;

    bool negroo;
	[HideInInspector]
	public string personaAhora;
    bool empezado;
    GameObject jugador;
    Animator playerAnimator;
    PlayerController playerMovement;
    Rigidbody2D rb;
    // cola de strings( el primero en meterse es el primero en salir
    Queue<string> frases;
    // Use this for initialization
    void Start()
    {
		dialogueMensaje.text = "Pulsar " + botonParaHablar.ToString () + " para interactuar.";
		dialogueMensaje.gameObject.SetActive (false);
        frases = new Queue<string>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        playerMovement = jugador.GetComponent<PlayerController>();
        rb = jugador.GetComponent<Rigidbody2D>();
        playerAnimator = jugador.GetComponent<Animator>();
		negroo = false;
    }
    public void TriggerDialogo(Dialogue dialogo)
    {
        if (dialogo.nombre == "Negro" && !GameManager.instance.darObjeto)
            negroo = true;
        else
            negroo = false;

        playerAnimator.SetLayerWeight(1, 0f);//Cambia la animacion a Idle
        rb.velocity = Vector2.zero;
        playerMovement.enabled = false;//Desactiva el movimiento mientras que dure el dialogo
        empezado = false;
        dialogueBox.SetActive(true);
        frases.Clear();// vacia la cola
        nombre.text = dialogo.nombre;
		personaAhora=dialogo.nombre;
        foreach (string frase in dialogo.frases)
            if (frase != null)
                frases.Enqueue(frase);//mete en la cola todos los elementos del array de dialogo
        salir.gameObject.SetActive(true);
		if (!negroo || !GameManager.instance.habladoNegro)
			hablar.gameObject.SetActive(true);
		else 
			hablar.gameObject.SetActive(false);
        DarObjeto.gameObject.SetActive(true);
        sentence.text = "";

        if (GameManager.instance.darObjeto)
            SiguienteFrase();

    }


    public void SiguienteFrase()
    {
        empezado = true;
        if (negroo)
            GameManager.instance.habladoNegro = true;
        negroo = false;
        salir.gameObject.SetActive(false);
        hablar.gameObject.SetActive(false);
        DarObjeto.gameObject.SetActive(false);

        string frase;
        if (frases.Count == 0)// si no quedan frases en la cola es que se ha acabado el dialogo
            EndDialogue();
        else
        {
            //saca la primera frase de la cola(la primera que se metio) y la muestra en el cuadro de texto
            frase = frases.Dequeue();
            sentence.text = frase;
        }
    }
    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        playerMovement.enabled = true;
        

    }
    public void SalirConversacion()
    {
        // GameManager.instance.habladoNegro = false;
    }


    void Update()
    {
		CheckInputDialogue ();

        if (Input.GetMouseButtonDown(0) == true && empezado && !GameManager.instance.darObjeto)
        {
            SiguienteFrase();
        }
    }
		
	//Comprueba si el ususario da la orden de comenzar la conversación
	void CheckInputDialogue(){
		if (Input.GetKeyDown (botonParaHablar) && currentNPC != null) {
			dialogueMensaje.gameObject.SetActive (false);
			currentNPC.hablando = true;
			currentNPC.ComienzaDialogo ();
		}			
	}
}