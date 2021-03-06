﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Tipos de Movimiento
public enum Movimiento{Directions_4, Free, Platform}

//Clase para 4Directions
[System.Serializable]
public class Directions_4 {
	//Variables
	public float speed;
	[HideInInspector]
	public float playerSpeed;
	public Vector2 direction;
	bool isRunning=false;

	//Movimiento
	public void Move(Rigidbody2D rb){

		if (isRunning)
			playerSpeed = speed + speed / 2;
		else 
			playerSpeed = speed;
		
		rb.velocity = direction.normalized * playerSpeed;

		GameManager.instance.ActualPlayerPosition = rb.position;

	}
	//Input
	public void CheckInput()
	{
		direction = Vector2.zero;
		isRunning = false;
		if (!GameManager.instance.ventanaAbierta) {
			if (Input.GetButton ("Vertical")) {
				direction = Vector2.up * Mathf.Sign (Input.GetAxis ("Vertical"));
            
				GameManager.instance.ActualPlayerDirecction = direction.normalized;

			} else if (Input.GetButton ("Horizontal")) {
				direction = Vector2.right * Mathf.Sign (Input.GetAxis ("Horizontal"));

				GameManager.instance.ActualPlayerDirecction = direction.normalized;
			}
			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
				isRunning = true;
		}
	}
	//Animator
	public void HandleAnimatorLayers(Animator animator){
		if (direction != Vector2.zero) {
			animator.SetLayerWeight (1, 1f);
			animator.SetFloat ("x", direction.x);
			animator.SetFloat ("y", direction.y);
		}
		else
			animator.SetLayerWeight (1, 0f);				
	}
}
//Clase para Free
[System.Serializable]
public class Free {
	//Variables
	public float speed;
	public Vector2 direction;

	//Métodos
	//Movimiento
	public void Move(Rigidbody2D rb){
		rb.velocity = direction.normalized * speed;
	}
	//Input
	public void CheckInput()
	{
		if (!GameManager.instance.ventanaAbierta) {
			direction.y = Input.GetAxis ("Vertical");
			direction.x = Input.GetAxis ("Horizontal");
		}
	}
}
//Clase para Plataformas
[System.Serializable]
public class Platform {
	//Variables
	public float jumpForce;
	public float maxSpeed;
	public Transform groundCheck;
	public Vector2 direction;
	bool jump = false;
	bool grounded = true;
	public AudioClip jumpClip;

	//Métodos
	//Input
	public void CheckInput(GameObject go){
		if (!GameManager.instance.ventanaAbierta) {
			//Movimimiento principal
			direction.x = Input.GetAxis ("Horizontal");
			grounded = Physics2D.Linecast (go.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
			if (Input.GetButtonDown ("Jump") && grounded) {
				jump = true;
				AudioSource.PlayClipAtPoint (jumpClip, go.transform.position);
			}
		}
	}
	//Movimiento
	public void Move(Rigidbody2D rb){
		rb.velocity = new Vector2(direction.x * maxSpeed, rb.velocity.y);
		if (jump) {
			rb.AddForce (Vector2.up * jumpForce);
			jump = false;
		}
	}
	//Animator
	public void HandleAnimatorLayers(Animator animator){
		if (direction != Vector2.zero && grounded) {
			animator.SetLayerWeight (1, 1f);
			animator.SetFloat ("x", direction.x);
		} else {
			animator.SetFloat ("x", direction.x);
			animator.SetLayerWeight (1, 0f);
		}			
	}
}
//Clase principal
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {
	
	public Movimiento movement = Movimiento.Directions_4;
	public Directions_4 directions_4;
	public Free directions_8;
	public Platform platform;
	public Rigidbody2D myRigidbody;
	Animator animator;
	void Start(){
		animator = GetComponent<Animator> ();
		/*if (GameManager.instance.Escena1PlayerPos == Vector2.zero)
			GameManager.instance.Escena1PlayerPos = myRigidbody.position;
		else if (GameManager.instance.Escena2PlayerPos == Vector2.zero)
			GameManager.instance.Escena2PlayerPos = myRigidbody.position;
		else {
			if (SceneManager.GetActiveScene ().name == GameManager.instance.escenaPrincipal)
				myRigidbody.position = GameManager.instance.Escena1PlayerPos;
			else if (SceneManager.GetActiveScene ().name == GameManager.instance.EscenaPiso2)
				myRigidbody.position = GameManager.instance.Escena2PlayerPos;
		}*/
	}

	void Update () {

		if (!GameManager.instance.pauseMode) {
			if (movement == Movimiento.Directions_4) {
				directions_4.CheckInput ();
				directions_4.HandleAnimatorLayers (animator);
			}
			if (movement == Movimiento.Free) {
				directions_8.CheckInput ();
				directions_4.CheckInput ();						//PROVISIONAL
				directions_4.HandleAnimatorLayers (animator);
			}
			if (movement == Movimiento.Platform) {
				platform.CheckInput (gameObject);
				platform.HandleAnimatorLayers (animator);
			}
		}
	}

	void FixedUpdate(){
		if (!GameManager.instance.pauseMode) {
			if (movement == Movimiento.Directions_4)
				directions_4.Move (myRigidbody);
			if (movement == Movimiento.Free)
				directions_8.Move (myRigidbody);
			if (movement == Movimiento.Platform)
				platform.Move (myRigidbody);
		}	
	}
}
