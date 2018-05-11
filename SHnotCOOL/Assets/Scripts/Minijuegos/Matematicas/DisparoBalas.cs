using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisparoBalas : MonoBehaviour {

    public AudioClip disparo;
    public float speed;
    public GameObject bala;

    float cadenciaDisparo;
    int numeroProyectiles;
    float anguloGiro;
    Vector2 velocidadDisparo;
    Vector2 posicionJugador;
    float angulo;
    float incremento;
    float tamañoCamara;
    float giro;
    // Use this for initialization
    void Start () {
        numeroProyectiles = Random.Range(4, 10);
        tamañoCamara = Camera.main.orthographicSize;
        angulo = 360 / numeroProyectiles;
        anguloGiro = 90 / numeroProyectiles;
        switch(GameManager.instance.trimestre)
        {
            case (1):
                cadenciaDisparo = Random.Range(1.25f, 1.5f);
                SeleccionaDisparo(3);
                break;
            case (2):
                cadenciaDisparo = Random.Range(1f, 1.25f);
                SeleccionaDisparo(3);
                break;
            case (3):
                cadenciaDisparo = Random.Range(0.75f, 1f);
                SeleccionaDisparo(5);
                break;
        }

	}
	
    void DisparoAPosicion()
    {
        
        posicionJugador = GameManager.instance.ActualPlayerPosition;
        velocidadDisparo= posicionJugador- new Vector2(transform.position.x,transform.position.y);
        GameObject bullet=Instantiate(bala,transform.position, Quaternion.identity );
        AudioSource.PlayClipAtPoint(disparo, transform.position);
        bullet.GetComponent<Rigidbody2D>().velocity =velocidadDisparo.normalized*speed;
        Destroy(bullet, tamañoCamara * (3/speed));
        Invoke("DisparoAPosicion", cadenciaDisparo);
       
    }
    void DisparoEspiral()
    {
        velocidadDisparo = new Vector2(Mathf.Sin(incremento*Mathf.Deg2Rad),Mathf.Cos(incremento*Mathf.Deg2Rad));
        GameObject bullet = Instantiate(bala, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(disparo, transform.position);
        bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * speed;
        Destroy(bullet, tamañoCamara * (3 / speed));
        incremento=(incremento+anguloGiro)%360;
        Invoke("DisparoEspiral", cadenciaDisparo);

    }
    void DisparoExplosion()
    {        
        incremento = 0;
        while(incremento/360<1)
        {
            velocidadDisparo =  new Vector2(Mathf.Sin(incremento*Mathf.Deg2Rad), Mathf.Cos(incremento*Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bala, new Vector2(transform.position.x, transform.position.y) + velocidadDisparo, Quaternion.identity);
            AudioSource.PlayClipAtPoint(disparo, transform.position);
            bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * speed;
            Destroy(bullet, tamañoCamara * (3 / speed));
            incremento+=angulo;
        }
        Invoke("DisparoExplosion", cadenciaDisparo);

    }
    void ExplosionEnEspiral()
    {
        incremento = 0;
        giro = (giro + anguloGiro) % 360;
        while (incremento / 360 < 1)
        {
            velocidadDisparo = new Vector2(Mathf.Sin(giro * Mathf.Deg2Rad), Mathf.Cos(giro * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bala, new Vector2(transform.position.x, transform.position.y) + velocidadDisparo, Quaternion.identity);
            AudioSource.PlayClipAtPoint(disparo, transform.position);
            bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * speed;
            Destroy(bullet, tamañoCamara * (3 / speed));
            giro += angulo;
            incremento += angulo;
        }

        Invoke("ExplosionEnEspiral", cadenciaDisparo);
    }
    void EspiralVariosProyectiles()
    {
        incremento = 0;
        giro = giro  % 360;
        while (incremento / 360 < 1)
        {
            velocidadDisparo = new Vector2(Mathf.Sin(giro * Mathf.Deg2Rad), Mathf.Cos(giro * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bala, new Vector2(transform.position.x, transform.position.y) + velocidadDisparo, Quaternion.identity);
            AudioSource.PlayClipAtPoint(disparo, transform.position);
            bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * speed;
            giro = (giro + anguloGiro) % 360;
            incremento += angulo;
        }

        Invoke("EspiralVariosProyectiles", cadenciaDisparo);
    }
    void DisparoVariante()
    {
        int disparo = Random.Range(1, 4);
        float tiempo = Random.Range(5, 15);
        SeleccionaDisparo(disparo);
        Invoke("CancelInvoke", tiempo);
        Invoke("DisparoVariante", tiempo);
    }
    void SeleccionaDisparo(int modoDisparo)
    {
        switch(modoDisparo)
        {
            case (0):
                DisparoAPosicion();
                break;
            case (1):
                DisparoEspiral();
                break;
            case (2):
                DisparoExplosion();
                break;
            case (3):
                ExplosionEnEspiral();
                break;
            case (4):
                EspiralVariosProyectiles();
                break;
            case (5):
                DisparoVariante();
                break;

        }

    }
    }
