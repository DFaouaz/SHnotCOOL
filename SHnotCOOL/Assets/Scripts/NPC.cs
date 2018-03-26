using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class NPC : MonoBehaviour
{
    public Dialogue dialogo;
    bool encontrado;
    bool hablando = false;
    const int frasesMax = 20;
    StreamReader texto;


    public void ComienzaDialogo()
    {


        texto = new StreamReader("tex.txt", Encoding.Default);

        LeeDialogo(dialogo);
        FindObjectOfType<DialogueManager>().TriggerDialogo(dialogo);


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") == true)
        {
            hablando = true;
            ComienzaDialogo();
            
        }
    }
    void LeeDialogo(Dialogue dialogo)
    {
        dialogo.frases = new string[frasesMax];
        string linea;

        encontrado = false;


        do
        {
            linea = texto.ReadLine();
            encontrado = (linea == dialogo.nombre);
        } while (!texto.EndOfStream && !encontrado);


		if (dialogo.nombre == MisionManager.instance.lista.personaje) {
			encontrado = false;
			if (MisionManager.instance.lista.completado) {
				
				while (!texto.EndOfStream && !encontrado) {
					linea = texto.ReadLine ();
					encontrado = (linea == "(Mision completada).");
				}
			}
			if (GameManager.instance.darObjeto) {
				
				while (!texto.EndOfStream && !encontrado) {
					linea = texto.ReadLine ();
					encontrado = (linea == "(Dar Objeto)");
					MisionManager.instance.lista.completado = true;
				}
			}
		}
		else if (GameManager.instance.darObjeto) {
			encontrado = false;
			while (!texto.EndOfStream && !encontrado) {
				linea = texto.ReadLine ();
				encontrado = (linea == "(Dar Objeto)");
			}
		}
        int i = 0;
        linea = texto.ReadLine();
        do
        {

            dialogo.frases[i] = linea;
            linea = texto.ReadLine();
            i++;
        } while (linea != null && linea != "" && linea != "(Mision completada)." && linea != "(Dar Objeto)");
        texto.Close();
    }
    private void Update()
    {
		if (GameManager.instance.darObjeto && hablando && dialogo.nombre== FindObjectOfType<DialogueManager>().personaAhora)
        {
            ComienzaDialogo();
          
            hablando = false;
        }

    }
}
