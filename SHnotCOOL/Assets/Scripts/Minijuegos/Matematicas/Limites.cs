using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limites : MonoBehaviour {
    float camaraPosX, camaraPosY, Altura, Anchura;
    public GameObject bound;
   
    BoxCollider2D borde;
	// Use this for initialization
	void Start () {
        Altura = Camera.main.orthographicSize * 2;
        Anchura = Altura * Camera.main.aspect;
  
        camaraPosX = Camera.main.transform.position.x;
        camaraPosY = Camera.main.transform.position.y;
        borde=Instantiate(bound,new Vector2(camaraPosX,camaraPosY+Altura/2),Quaternion.identity).GetComponent<BoxCollider2D>();
        borde.size=new Vector2(Anchura,1);

        borde=Instantiate(bound, new Vector2(camaraPosX, camaraPosY - Altura / 2), Quaternion.identity).GetComponent<BoxCollider2D>();
        borde.size=new Vector2(Anchura, 1);
       
        borde =Instantiate(bound, new Vector2(camaraPosX+Anchura/2, camaraPosY ), Quaternion.identity).GetComponent<BoxCollider2D>();
        borde.size=new Vector2( 1,Altura);

        borde =Instantiate(bound, new Vector2(camaraPosX-Anchura/2, camaraPosY ), Quaternion.identity).GetComponent<BoxCollider2D>();
        borde.size=new Vector2(1,Altura);
    }
	

}
