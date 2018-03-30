using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {


	public float speed;

	void Update () {
		transform.Translate (0, -speed * Time.deltaTime, 0);
	}
}
