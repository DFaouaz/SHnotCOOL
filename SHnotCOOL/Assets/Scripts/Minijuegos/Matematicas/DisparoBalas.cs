using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisparoBalas : MonoBehaviour {

    public AudioClip sonidoDisparo;
    public GameObject bala;
    public float velocidad;
    
    Vector3 velocidadDisparo;
    Vector3 posicionJugador;
    float numeroProyectiles, tamañoCamara, angulo, anguloGiro, giro, cadenciaDisparo, tiempoCambio;
    int modoDisparo = 3;

    public int modoDisparo1, modoDisparo2, modoDisparo3;

    void Start () {

        numeroProyectiles = Random.Range(4, 6);
        tamañoCamara = Camera.main.orthographicSize;
        angulo = 360 / numeroProyectiles;
        anguloGiro = 90 / numeroProyectiles;

        switch (GameManager.instance.trimestre)
        {
            case (1):
                cadenciaDisparo = Random.Range(1f, 1.5f);
                if (modoDisparo1 <= 3 || modoDisparo1 >= 0)
                    SeleccionaDisparo(modoDisparo1);
                break;
            case (2):
                cadenciaDisparo = Random.Range(1f, 1.5f);
                if (modoDisparo2 <= 3 || modoDisparo2 >= 0)
                    SeleccionaDisparo(modoDisparo2);
                break;
            case (3):
                cadenciaDisparo = Random.Range(1f, 1.5f);
                if (modoDisparo3 <= 3 || modoDisparo3 >= 0)
                    SeleccionaDisparo(modoDisparo3);
                break;
        }
	}
	
    void CreaBala(Vector3 pos)
    {
        GameObject bullet = Instantiate(bala, pos, Quaternion.identity);
        AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);

        bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * velocidad;
        Destroy(bullet, tamañoCamara * (3 / velocidad));
    }

    void DisparoPosicion()
	{
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {        
			posicionJugador = GameManager.instance.ActualPlayerPosition;
			velocidadDisparo = posicionJugador - new Vector3 (transform.position.x, transform.position.y);
            CreaBala(transform.position);
		}
        Invoke("DisparoPosicion", cadenciaDisparo);
    }

    void DisparoExplosion()
    {       
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
			float i = 0;
			while (i / 360 < 1)
            {
				velocidadDisparo = new Vector2 (Mathf.Sin (i * Mathf.Deg2Rad), Mathf.Cos (i * Mathf.Deg2Rad));
                CreaBala(transform.position + velocidadDisparo);
				i += angulo;
			}
		}
        Invoke("DisparoExplosion", cadenciaDisparo);
    }

    void EspiralVariosProyectiles()
    {
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
			giro = giro % 360;

            float i = 0;
            while (i / 360 < 1)
            {
				velocidadDisparo = new Vector2 (Mathf.Sin (giro * Mathf.Deg2Rad), Mathf.Cos (giro * Mathf.Deg2Rad));
                CreaBala(transform.position + velocidadDisparo);
                giro = (giro + anguloGiro) % 360;
				i += angulo;
			}
		}
        Invoke("EspiralVariosProyectiles", cadenciaDisparo);
    }

    void DisparoVariante()
    {
        CancelInvoke();
        tiempoCambio = Random.Range(5f, 10f);
        if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
            SeleccionaDisparo(modoDisparo % 3);
            modoDisparo++;
        }
        Invoke("DisparoVariante", tiempoCambio);
    }

    void SeleccionaDisparo(int modoDisparo)
    {
        switch(modoDisparo)
        {
            case (0):
                DisparoPosicion();
                break;
            case (1):
                DisparoExplosion();
                break;
            case (2):
                EspiralVariosProyectiles();
                break;
            case (3):
                DisparoVariante();
                break;

        }
    }
}