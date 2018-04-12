using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maton : MonoBehaviour {
    public Button flechaDerecha;
    public Button flechaAbajo;
    public Button flechaIzquierda;
    public Button flechaArriba;
    public Text time;
    public Text textoFinJuego;
    public float decremento;
    public float distMinima;
    GameObject enemigo;
    bool pulsado=false;
    bool cogido = false;
    float tiempo=10;
    float tiempoCambio= 1f;
    float tiempoFlechaActiva;
    
    int flecha;//0 derecha, 1 abajo, 2 izquierda, 3 arriba
    public void ActivaFlecha()
    {
        
        switch (flecha)
        {
            case 0:
                flechaDerecha.gameObject.SetActive(true);
                flechaAbajo.gameObject.SetActive(false);
                flechaIzquierda.gameObject.SetActive(false);
                flechaArriba.gameObject.SetActive(false);
                break;
            case 1:
                flechaDerecha.gameObject.SetActive(false);
                flechaAbajo.gameObject.SetActive(true);
                flechaIzquierda.gameObject.SetActive(false);
                flechaArriba.gameObject.SetActive(false);
                break;
            case 2:
                flechaDerecha.gameObject.SetActive(false);
                flechaAbajo.gameObject.SetActive(false);
                flechaIzquierda.gameObject.SetActive(true);
                flechaArriba.gameObject.SetActive(false);
                break;
            case 3:
                flechaDerecha.gameObject.SetActive(false);
                flechaAbajo.gameObject.SetActive(false);
                flechaIzquierda.gameObject.SetActive(false);
                flechaArriba.gameObject.SetActive(true);
                break;
        }
    }
    public void CheckeaInput()
    {

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            switch (flecha)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        CambiaFlecha();
                        ActivaFlecha();

                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
                        CambiaFlecha();
                        ActivaFlecha();
                    }

                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CambiaFlecha();
                        ActivaFlecha();

                    }
                    else
                    {
                        CambiaFlecha();
                        ActivaFlecha();
                        transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
                    }

                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        CambiaFlecha();
                        ActivaFlecha();

                    }
                    else
                    {
                        CambiaFlecha();
                        ActivaFlecha();
                        transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
                    }

                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        CambiaFlecha();
                        ActivaFlecha();

                    }
                    else
                    {
                        CambiaFlecha();
                        ActivaFlecha();
                        transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
                    }

                    break;
            }
            pulsado = true;
            tiempoFlechaActiva = 0;
        }

        else if(tiempoFlechaActiva>tiempoCambio)
        {
            CambiaFlecha();
            ActivaFlecha();
            transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
            tiempoFlechaActiva = 0;
        }
    }
    public void CambiaFlecha()
    {
        flecha = Random.Range(0, 4);
    }
    private void Start()
    {
        enemigo = GameObject.FindGameObjectWithTag("Maton");
        CambiaFlecha();
        ActivaFlecha();
    }
    private void FixedUpdate()
    {
        tiempoFlechaActiva += Time.deltaTime;
        //si alguna de las teclas es pulasada
        if (!cogido && !pulsado)
           CheckeaInput();
       
        //cuando no hay ninguna tecla pulsada
        if (!Input.anyKeyDown)
            pulsado = false;
        if (cogido)
            textoFinJuego.text = "El matón te ha cogido";
        if (Time.time > tiempo)
        {
            textoFinJuego.text = "Has escapado";
            cogido = true;
        }
        else
            time.text = (tiempo - Mathf.Round( Time.time)).ToString();
        if (transform.position.x - enemigo.transform.position.x < distMinima)
            cogido = true;
        
    }
}
