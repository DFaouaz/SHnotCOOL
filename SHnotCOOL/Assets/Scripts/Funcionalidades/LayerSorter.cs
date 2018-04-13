﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour {

	SpriteRenderer parentSp;
	List<Obstacle> obstacles = new List<Obstacle>();

	void Start(){
		parentSp = GetComponentInParent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Obstacle") {
			Obstacle obs = col.GetComponentInParent<Obstacle> ();
			if(obstacles.Count == 0 || obs.MySpRenderer.sortingOrder - 1 < parentSp.sortingOrder)
				parentSp.sortingOrder = obs.MySpRenderer.sortingOrder - 1;
			obstacles.Add (obs);
			
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Obstacle") {
			Obstacle obs = col.GetComponentInParent<Obstacle> ();
			obstacles.Remove (obs);
			if (obstacles.Count == 0) {
				if (this.gameObject.tag == "Negro")
					parentSp.sortingOrder = 199;
				else
					parentSp.sortingOrder = 200; //Provisional
			}
			else {
				obstacles.Sort ();
				parentSp.sortingOrder = obstacles [0].MySpRenderer.sortingOrder - 1;
			}
		}
	}
}