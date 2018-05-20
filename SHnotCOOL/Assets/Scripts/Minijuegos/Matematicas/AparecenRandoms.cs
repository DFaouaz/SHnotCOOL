using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AparecenRandoms : MonoBehaviour {

    float alto, ancho, camaraPosX, camaraPosY, tiempoAux;
    int cambiaOperacion = 2;
    bool fin = false;
    string operacion;
    GameObject copiaColeccionable;
    Vector2 pos;

    public GameObject coleccionable;
    public Text operacionTexto, progreso;
    public float tiempo;

    void Start() {

        alto = (Camera.main.orthographicSize * 2) - 0.5f;
        ancho = (ancho * Camera.main.aspect) - 0.5f;
        camaraPosX = (Camera.main.transform.position.x) - 0.5f;
        camaraPosY = (Camera.main.transform.position.y) - 0.5f;

		ActualizaAspecto ();
        GeneraSolucion();
    }

    void Update() {

		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta && GameManager.instance.matematicasScore <= 10 && !fin)
        {
			tiempoAux -= Time.deltaTime;

			if (tiempoAux <= 0 || copiaColeccionable == null)
            {
				GeneraSolucion ();
				ActualizaAspecto ();
			}
			if (GameManager.instance.matematicasScore == 10)
            {
				fin = true;
				GameManager.instance.FinExamenMatematicas ();
			}				
		}
		else if (GameManager.instance.matematicasScore > 10)
        {
			GameManager.instance.matematicasScore = 10;
		}
    }

    public void GeneraSolucion()
	{
		if (copiaColeccionable != null)
			Destroy (copiaColeccionable);

		tiempoAux = tiempo;
		pos = new Vector2 (Random.Range (camaraPosX - alto / 2, camaraPosX - 1 + ancho / 2), Random.Range (camaraPosY + 1 - alto / 2, camaraPosY + alto / 2));
		copiaColeccionable = Instantiate (coleccionable, pos, Quaternion.identity);

		int resultado = CalculaOperacion ();
		copiaColeccionable.GetComponent<TextMesh> ().text = resultado.ToString ();
	}

    int CalculaOperacion()
    {
        int operando1 = Random.Range(1, 10);
        int operando2 = Random.Range(1, 10);

        if ((cambiaOperacion % 2) == 0)
        {
            operacion = operando1.ToString() + "+" + operando2.ToString();
            cambiaOperacion++;
            return operando1 + operando2;
        }
        else
        {
            operacion = operando1.ToString() + "-" + operando2.ToString();
            cambiaOperacion++;
            return operando1 - operando2;
        }
    }

	void ActualizaAspecto()
    {
		operacionTexto.text = operacion;
		progreso.text = GameManager.instance.matematicasScore + "/" + 10;
	}
}