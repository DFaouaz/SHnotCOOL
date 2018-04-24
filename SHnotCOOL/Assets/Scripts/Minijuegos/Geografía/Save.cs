using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
    GameObject respawn, go;
    Animator animator;

    void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            respawn.transform.position = transform.position;
            animator.SetBool("Llegado", true);
        }
    }
}
