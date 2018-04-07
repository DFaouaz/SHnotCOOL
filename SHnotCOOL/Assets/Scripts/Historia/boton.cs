using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boton : MonoBehaviour {
	public int index;
	opcion MiOpcion;
	bool turno;
	int valor;

	Text texto;
	void Start () {
		
		texto = GetComponentInChildren<Text> ();
		AsignarOpcion ();
		turno = false;
	}


	void Update()
	{
		if (!turno) {
			if (AnswerManager.instance.getPulsado ())
				AsignarOpcion ();
		}
		else {
			AnswerManager.instance.setPulsado (false);
			turno = false;
		}
	}

	void AsignarOpcion()
	{
		MiOpcion = AnswerManager.instance.getOpcion (index);
		texto.text = MiOpcion.frase;
		valor = MiOpcion.valor;

	}
	public void clikado()
	{
		AnswerManager.instance.setPulsado (true);
		AnswerManager.instance.InitButtons ();
		turno = true;
		AsignarOpcion ();
	}
}
