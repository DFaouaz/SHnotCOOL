using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NegroBehaviour : NPC {

	public PlayerController playerTarget;
	public float minDistance;
	Rigidbody2D myRigidbody;
	Animator animator;


	new void Start(){
		animator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
		base.Start ();
		isAcepted = true;
	}

	void FixedUpdate () {
		if(alreadyTalked)
			FollowPlayer ();
	}
		

	void FollowPlayer(){
		Vector2 x = playerTarget.myRigidbody.position - playerTarget.directions_4.direction.normalized - myRigidbody.position;
		if (x.magnitude > 0 && (playerTarget.myRigidbody.position - myRigidbody.position).magnitude > minDistance) {
			x.Normalize ();
			myRigidbody.velocity = x * 3;
		} else
			myRigidbody.velocity = Vector2.zero;
		HandleAnimatorLayers (animator, x);
	}
	void HandleAnimatorLayers(Animator animators, Vector2 x){
		Vector2 direction = Vector2.zero;
		if (myRigidbody.velocity != Vector2.zero){
			if (Mathf.Abs(x.x) > Mathf.Abs(x.y)){
				if (x.x < 0)
					direction = Vector2.left;
				else
					direction = Vector2.right;
			}else if (Mathf.Abs(x.x) < Mathf.Abs(x.y)){
				if (x.y < 0)
					direction = Vector2.down;
				else
					direction = Vector2.up;
			}
		}else
			direction = Vector2.zero;


		if (direction != Vector2.zero){
			animators.SetLayerWeight(1, 1f);
			animators.SetFloat("x", direction.x);
			animators.SetFloat("y", direction.y);
		}else
			animators.SetLayerWeight(1, 0f);
	}
}
