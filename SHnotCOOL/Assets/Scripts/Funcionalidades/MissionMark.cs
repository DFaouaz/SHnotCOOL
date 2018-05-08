using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MissionMark : MonoBehaviour {

	public enum Est{Exclamacion,Interrogacion,None};
	[HideInInspector]
	public Est estado;
	SpriteRenderer render;

	void Start () {
		estado = Est.None;
		render = GetComponent<SpriteRenderer> ();
		UpdateRender (estado);
	}

	public void UpdateRender(Est nuevoEstado){
		estado = nuevoEstado;
		if (estado == Est.Exclamacion)
			render.sprite = DialogueManager.instance.exclamacion;
		else if (estado == Est.Interrogacion)
			render.sprite = DialogueManager.instance.interrogacion;
		else
			render.sprite = null;
	}
}
