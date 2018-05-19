using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float Altura;
    float Anchura;
    float camaraPosX;
    float camaraPosY;
    float randomX, randomY, maxX = 6f, minX = -6f, maxY = 6f, minY = -6f, timeChange = 0;
    public float randomRate, moveSpeed;
    // Use this for initialization
    void Start() {
        //x = transform.position.x;
        //y = transform.position.y;
        Altura = Camera.main.orthographicSize * 2;
        Anchura = Altura * Camera.main.aspect;
        camaraPosX = Camera.main.transform.position.x;
        camaraPosY = Camera.main.transform.position.y;
        maxX = -1+camaraPosX + Anchura / 2;
        maxY = -1+camaraPosY + Altura / 2;
        minX = 1+camaraPosX - Anchura / 2;
        minY = 1+camaraPosY - Altura / 2;
    }

    // Update is called once per frame
    void Update() {
		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta) {
			if (Time.time >= timeChange) {
				randomX = Random.Range (-2f, 2f);
				randomY = Random.Range (-2f, 2f);
				timeChange = Time.time + Random.Range (0.5f, 1.5f);
			}

			transform.Translate (new Vector2 (randomX, randomY) * moveSpeed * Time.deltaTime);
			if (transform.position.x >= maxX || transform.position.x <= minX)
				randomX = -randomX;

			if (transform.position.y >= maxY || transform.position.y <= minY)
				randomY = -randomY;
        
			Mathf.Clamp (transform.position.x, minX, maxX);
			Mathf.Clamp (transform.position.y, minY, maxY);
		}
	}
}
