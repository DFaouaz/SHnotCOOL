using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {
    GameObject respawn;
    Animator animator;

    void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawn.transform.position = transform.position;
            animator.SetBool("Llegado", true);
        }
    }
}