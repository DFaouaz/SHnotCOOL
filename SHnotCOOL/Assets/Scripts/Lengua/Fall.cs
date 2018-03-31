using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {


	public float minSpeed, maxSpeed;
	[HideInInspector]
	public float speed;

	void Start(){
		speed = Random.Range (minSpeed, maxSpeed);
	}

	void Update () {
		transform.Translate (0, -speed * Time.deltaTime, 0);
	}
}
