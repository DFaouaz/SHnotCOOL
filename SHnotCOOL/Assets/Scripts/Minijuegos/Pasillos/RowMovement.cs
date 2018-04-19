using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowMovement : MonoBehaviour {
	Rigidbody2D rb;
	SpriteRenderer sprite;
	public float velocity;
	public bool leftSense;

	//Cachea el Rigidbody y el renderer del objeto 
	//y lo pone en movimiento a una velocidad y en un sentido 
	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		Move (velocity, leftSense);
	}

	//Mueve y orienta el objeto en funcion del sentido 
	public void Move(float velocity,bool sense){
		if (sense == true) {
			//modifica la velocidad del Rigidbody
			rb.velocity =new Vector2(-velocity,0f);
			//Gira el sprite en la direccion del sentido
			sprite.transform.right = new Vector2(velocity,0f);
		}
		if (sense == false) {
			rb.velocity =new Vector2(velocity,0f);
			sprite.transform.right = new Vector2(-velocity,0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Destruye el objeto al entrar en un Trigger
		//diferente del jugador y el agua (Dead zones)
		GameObject go = other.gameObject;
		if(!go.CompareTag("player")&&!go.CompareTag("Obstacle"))
			Destroy (gameObject);
	}
}
