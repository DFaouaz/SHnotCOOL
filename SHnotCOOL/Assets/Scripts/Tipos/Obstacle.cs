using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour, IComparable<Obstacle>
{
	public SpriteRenderer MySpRenderer { get; set; }

	void Awake () {
		MySpRenderer = GetComponent<SpriteRenderer> ();
	}
	
	public int CompareTo (Obstacle other)
	{
		if (MySpRenderer.sortingOrder > other.MySpRenderer.sortingOrder)
			return 1;
		else if (MySpRenderer.sortingOrder < other.MySpRenderer.sortingOrder)
			return -1;
		return 0;
	}
}
