using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D playerRB;
	Transform playerTR;
	Animator playerAnim;
	Vector3 lastPos;

    float tiempo;
	bool alive = true;

	int movX, movY;

	void Awake(){
		playerRB = GetComponent<Rigidbody2D> ();
		playerTR = GetComponent<Transform> ();
	}

	void Start () {
		playerAnim = GetComponentInChildren<Animator> ();
		movX = 0;
		movY = 0;
		lastPos = playerTR.position;
	}

	void Update () {
		if (alive&&tiempo>5) 
		{
			// Comprobar la entrada del usuario
			CheckInput ();
		}
        tiempo += Time.deltaTime;
	}
		
	// MÉTODOS

	// Entrada del usuario
	void CheckInput()
	{
        movY = 0;
        movX = 0;

		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
		{
			//ARRIBA
			movX = 0;
			movY = 1;
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S))
		{
			//ABAJO
			movX = 0;
			movY = -1;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A))
		{
			//IZQUIERDA
			movX = -1;
			movY = 0;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D))
		{
			//DERECHA
			movX = 1;
			movY = 0;
		}

		// Llamada a Move si el usuario ha presionado una tecla de movimiento
		if (movX != 0 || movY != 0) 
		{
			Move (movX, movY);
		}
	}

	// Movimiento si la rana no sale de los límites del área de juego
	void  Move (int x, int y)
	{
		//Vector2 targetPosition = new Vector2 (transform.position.x + x, transform.position.y + y); //  
		Vector2 movement = new Vector2 (x, y);
		Vector2 offset = new Vector2 (0.5f, 0.5f);
		Vector2 targetPosition = (Vector2)playerTR.position + movement;
		bool collision = Physics2D.Raycast ((Vector2)gameObject.transform.position + offset, movement, 1, 1 << LayerMask.NameToLayer ("Ground"));
		if (!PasillosManager.instance.IsOutOfBounds (targetPosition) && !collision) 
		{
            HandleAnimatorLayers();
			playerRB.MovePosition (targetPosition); // Mueve la rana a la posición indicada
            movX = 0;
            movY = 0;
            Invoke("HandleAnimatorLayers", 0.2f);
        }


	}

	// Muerte de la rana
	public void Dead()
	{
        playerAnim.SetLayerWeight(2,2f);
        alive = false;
		Invoke("Reset", 1.0f);
	}

	// La llamada a PlayerDead se hace con un Invoke a un método distinto a Dead
	// para que dé tiempo a ver la animación de muerte de la rana
	void Reset () {
		PasillosManager.instance.PlayerDead ();
	}

    public void HandleAnimatorLayers()
    {
        Vector2 direction;
        direction.x = movX;
        direction.y = movY;
		if (direction != Vector2.zero) {
			playerAnim.SetLayerWeight (1, 1f);
			playerAnim.SetFloat ("x", direction.x);
            playerAnim.SetFloat ("y", direction.y);
		}
		else
            playerAnim.SetLayerWeight (1, 0f);				
	}
}
