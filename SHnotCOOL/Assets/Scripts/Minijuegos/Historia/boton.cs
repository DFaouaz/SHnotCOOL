using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boton : MonoBehaviour {
	public int index;
	opcion MiOpcion;
	int valor;

	Text texto;
	void Start () {
		texto = GetComponentInChildren<Text> ();
		AsignarOpcion ();
	
	}


	void Update()
	{
		if(!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
		
			AsignarOpcion ();
		
	}
	void AsignarOpcion()
	{
		MiOpcion = AnswerManager.instance.getOpcion (index);
		texto.text = MiOpcion.frase;
		valor = MiOpcion.valor;

	}
	public void clikado()
	{
        AnswerManager.instance.setDanio(valor);
        AnswerManager.instance.setPulsado (true);
		AnswerManager.instance.InitButtons ();
		AsignarOpcion ();
	}
}
