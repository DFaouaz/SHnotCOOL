using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookingnpc : MonoBehaviour {
	Rigidbody2D myRigidBody;
	GameManager Manager;
	Animator animator;
	Vector2 direction;
	Vector2 x;
	// Use this for initialization
	void Start () {
		Manager = GameManager.instance;
		myRigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		x = Manager.ActualPlayerPosition - myRigidBody.position;


		if (Mathf.Abs(x.x) > Mathf.Abs(x.y))
		{

			if (x.x < 0)
				direction = Vector2.left;
			else
				direction = Vector2.right;
		}
		else if (Mathf.Abs(x.x) < Mathf.Abs(x.y))
		{
			if (x.y < 0)
				direction = Vector2.down;
			else
				direction = Vector2.up;
		}
		animator.SetFloat("x", direction.x);
		animator.SetFloat("y", direction.y);
	}
}
