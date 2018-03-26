using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisparoBalas : MonoBehaviour {

    public float cadenciaDisparo;
    public float speed;
    public GameObject bala;
    public int numeroProyectiles;
    public float anguloGiro;
    Vector2 velocidadDisparo;

    Vector2 posicionJugador;

    float angulo;
    float incremento;
    float tamañoCamara;
    float giro;
    public int modoDisparo;
    // Use this for initialization
    void Start () {
        tamañoCamara = Camera.main.orthographicSize;
        angulo=360/numeroProyectiles;
        if (modoDisparo == 0)
            DisparoAPosicion();
        else if (modoDisparo == 1)
            DisparoEspiral();
        else if (modoDisparo == 2)
            DisparoExplosion();
        else if (modoDisparo == 3)
            ExplosionEnEspiral();
        else if (modoDisparo == 4)
            EspiralVariosProyectiles();

	}
	
    void DisparoAPosicion()
    {
        
        posicionJugador = GameManager.instance.ActualPlayerPosition;
        velocidadDisparo= posicionJugador- new Vector2(transform.position.x,transform.position.y);
        GameObject bullet=Instantiate(bala,transform.position, Quaternion.identity );
        bullet.GetComponent<Rigidbody2D>().velocity =velocidadDisparo.normalized*speed;
        Destroy(bullet, tamañoCamara * (3/speed));
        Invoke("DisparoAPosicion", cadenciaDisparo);
       
    }
    void DisparoEspiral()
    {
        velocidadDisparo = new Vector2(Mathf.Sin(incremento*Mathf.Deg2Rad),Mathf.Cos(incremento*Mathf.Deg2Rad));
        GameObject bullet = Instantiate(bala, transform.position, Quaternion.identity);
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
            bullet.GetComponent<Rigidbody2D>().velocity = velocidadDisparo.normalized * speed;
            giro = (giro + anguloGiro) % 360;
            incremento += angulo;
        }

        Invoke("EspiralVariosProyectiles", cadenciaDisparo);
    }
    }
