using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegroBehaibour : MonoBehaviour {
  
	Rigidbody2D myRigidBody;
	GameManager Manager;
    Animator animator;
    Vector2 direction;
    Vector2 x;

    void Start () {
		Manager = GameManager.instance;
		myRigidBody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAnimatorLayers(animator);
    }
        void FixedUpdate () {
        if (Manager.habladoNegro == true)
        {
            //Creamos un vector que apunte desde la posicion del negro a la posicion detrás del jugador.
            x = (Manager.ActualPlayerPosition - Manager.ActualPlayerDirecction) - myRigidBody.position;
            if (x.magnitude > 0.1)
            {
                //si se encuentra dento de un rango respecto al jugador se para.
                if ((Manager.ActualPlayerPosition - myRigidBody.position).magnitude > 1.1)
                {
                    x.Normalize();
                    myRigidBody.velocity = x * 3;
                }
                else
                    myRigidBody.velocity = Vector2.zero;
            }
            else
                myRigidBody.velocity = Vector2.zero;
        }
	}
    void HandleAnimatorLayers(Animator animators)
    {
        if (myRigidBody.velocity != Vector2.zero)
        {
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
        }
        else direction = Vector2.zero;
        

        if (direction != Vector2.zero)
        {
            animators.SetLayerWeight(1, 1f);
            animators.SetFloat("x", direction.x);
            animators.SetFloat("y", direction.y);
        }
        else
            animators.SetLayerWeight(1, 0f);
    }
}
