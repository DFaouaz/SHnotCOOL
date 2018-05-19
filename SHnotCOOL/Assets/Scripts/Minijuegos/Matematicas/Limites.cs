using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour {

    public GameObject limite;
    float camaraPosX, camaraPosY, alto, ancho;
    BoxCollider2D borde;

    void Start() {

        alto = Camera.main.orthographicSize * 2;
        ancho = alto * Camera.main.aspect;

        camaraPosX = Camera.main.transform.position.x;
        camaraPosY = Camera.main.transform.position.y;

        CreaLimite(camaraPosX, camaraPosY + alto / 2, ancho, 1f);
        CreaLimite(camaraPosX, camaraPosY - alto / 2, ancho, 1f);
        CreaLimite(camaraPosX + ancho / 2, camaraPosY, 1f, alto);
        CreaLimite(camaraPosX - ancho / 2, camaraPosY, 1f, alto);
    }

    void CreaLimite(float posX, float posY, float tamañoX, float tamañoY)
    {
        borde = Instantiate(limite, new Vector2(posX, posY), Quaternion.identity).GetComponent<BoxCollider2D>();
        borde.size = new Vector2(tamañoX, tamañoY);
    }
}