using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowMovement : MonoBehaviour {

	Rigidbody2D rb;
	SpriteRenderer sprite;
	public float velocity;
	public bool leftSense;
    
	void Awake() {

		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		Move (velocity, leftSense);
	}

    public void Move(float velocity, bool sense)
    {
        if (sense == true)
        {
            rb.velocity = new Vector2(-velocity, 0f);
            sprite.transform.right = new Vector2(velocity, 0f);
        }
        if (sense == false)
        {
            rb.velocity = new Vector2(velocity, 0f);
            sprite.transform.right = new Vector2(-velocity, 0f);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject go = other.gameObject;

		if(!go.CompareTag("Player")&&!go.CompareTag("Obstacle"))
			Destroy (gameObject);
	}
}