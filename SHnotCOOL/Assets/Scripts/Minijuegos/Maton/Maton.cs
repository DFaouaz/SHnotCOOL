using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maton : MonoBehaviour {

    public Button flechaDerecha, flechaAbajo, flechaIzquierda, flechaArriba;
    public Text tiempo, textoFin;
    public float decremento, tiempoLimite, tiempoCambioTecla;

    GameObject enemigo;
    float tiempoPasado = 0, tiempoFlechaActiva, distanciaMin;
    int flecha;
    bool pulsaTecla = false;

    void Start() {

        if (decremento % 2 == 0)
            decremento++;
        distanciaMin = decremento + 0.5f;
        enemigo = GameObject.FindGameObjectWithTag("Enemigo");
        AsignaTecla();
        DisminuyeTiempo();
    }

    void Update() {

        if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
            tiempoFlechaActiva += Time.deltaTime;
            tiempo.text = (tiempoLimite - tiempoPasado).ToString();

            if (Input.anyKeyDown)
                pulsaTecla = true;

            if (pulsaTecla)
            {
                CheckInput();
                CompruebaTiempo();
                pulsaTecla = false;
            }
            else
                CompruebaTiempo();

            if (transform.position.x - enemigo.transform.position.x < distanciaMin)
            {
                textoFin.text = "Te ha cogido";
                GameManager.instance.dinero -= 20;
                GameManager.instance.FinMaton();
            }
            else if (tiempoPasado >= tiempoLimite)
            {
                textoFin.text = "Has escapado";
                GameManager.instance.FinMaton();
            }
        }
    }

    void AsignaTecla()
    {
        flecha = Random.Range(0, 4);

        flechaDerecha.gameObject.SetActive(false);
        flechaAbajo.gameObject.SetActive(false);
        flechaIzquierda.gameObject.SetActive(false);
        flechaArriba.gameObject.SetActive(false);

        Invoke("MuestraTecla", 0.25f);
    }

    void MuestraTecla()
    {
        switch (flecha)
        {
            case 0:
                flechaDerecha.gameObject.SetActive(true);
                break;
            case 1:
                flechaAbajo.gameObject.SetActive(true);
                break;
            case 2:
                flechaIzquierda.gameObject.SetActive(true);
                break;
            case 3:
                flechaArriba.gameObject.SetActive(true);
                break;
        }
        CancelInvoke("MuestraTecla");
    }

    void CompruebaTiempo()
    {
        if (tiempoFlechaActiva > tiempoCambioTecla)
        {
            AsignaTecla();
            DisminuyeDistancia();
            tiempoFlechaActiva = 0;
        }
    }

    void DisminuyeTiempo()
    {
        if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta && tiempoPasado < tiempoLimite)
            tiempoPasado++;

        Invoke("DisminuyeTiempo", 1);
    }

    void DisminuyeDistancia()
    {
        if (transform.position.x > enemigo.transform.position.x + distanciaMin)
            transform.position = new Vector2(transform.position.x - decremento, transform.position.y);
    }

    void CheckInput()
    {

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S)))
        {
            switch (flecha)
            {
                case 0:
                    if (!Input.GetKeyDown(KeyCode.D))
                        DisminuyeDistancia();
                    else
                    {
                        AsignaTecla();
                        tiempoFlechaActiva = 0;
                    }
                    break;
                case 1:
                    if (!Input.GetKeyDown(KeyCode.S))
                        DisminuyeDistancia();
                    else
                    {
                        AsignaTecla();
                        tiempoFlechaActiva = 0;
                    }
                    break;
                case 2:
                    if (!Input.GetKeyDown(KeyCode.A))
                        DisminuyeDistancia();
                    else
                    {
                        AsignaTecla();
                        tiempoFlechaActiva = 0;
                    }
                    break;
                case 3:
                    if (!Input.GetKeyDown(KeyCode.W))
                        DisminuyeDistancia();
                    else
                    {
                        AsignaTecla();
                        tiempoFlechaActiva = 0;
                    }
                    break;
            }
        }
    }
}