using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnLetter : MonoBehaviour {

	string letras = "abcdefghijklmnopqrstuvwxyz";
	public Letter letraPrefab;
	public float firstSpawnDelay;
	public float spawnDelay;
	BoxCollider2D bounds;

	void Start(){
		//Caching del rect
		bounds = GetComponent<BoxCollider2D>();
		InvokeRepeating ("Spawn", firstSpawnDelay, spawnDelay);
	}

	void Spawn(){
		//Elige al azar una letra
		Vector2 randomPos = new Vector2 (Random.Range (bounds.bounds.min.x + 1, bounds.bounds.max.x - 1), this.gameObject.transform.position.y);
		Letter letra = Instantiate<Letter> (letraPrefab, randomPos, Quaternion.identity);
		letra.letra = (char)letras[Random.Range(0, letras.Length)];
	}

}
