using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerManager : MonoBehaviour {
	//Guarda todos los obstaculos
	Obstacle[] obstacles;
	//Para introducir el alto del mapa
	public int mapHeight;

	void Awake(){
		obstacles = FindObjectsOfType<Obstacle> ();
		//Para todo obstaculo, su orden es menor cuando mas lejos.
		for (int i = 0; i < obstacles.Length; i++) {
			obstacles [i].MySpRenderer.sortingOrder = (int)(mapHeight * 2 - obstacles[i].transform.position.y * 2);
		}
	}
}
