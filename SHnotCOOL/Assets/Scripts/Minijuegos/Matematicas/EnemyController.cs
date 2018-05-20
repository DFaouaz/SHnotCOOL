using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    float alto, camaraPosX, camaraPosY;
    float randomX, randomY, maxX, minX, maxY, minY, timeChange = 0;
    public float moveSpeed;

    void Start() {

        alto = Camera.main.orthographicSize * 2;
        camaraPosX = Camera.main.transform.position.x;
        camaraPosY = Camera.main.transform.position.y;

        maxX = -1 + camaraPosX + alto / 2;
        maxY = -1 + camaraPosY + alto / 2;
        minX = 1 + camaraPosX - alto / 2;
        minY = 1 + camaraPosY - alto / 2;
    }
    
    void Update() {

		if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
        {
			if (Time.time >= timeChange)
            {
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