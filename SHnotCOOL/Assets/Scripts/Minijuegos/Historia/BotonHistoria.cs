using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonHistoria : MonoBehaviour {

    public int index;
    Opcion miOpcion;
    Text text;
    int value;

    void Start() {

        text = GetComponentInChildren<Text>();
        AsignarOpcion();
    }

    void Update() {

        if (!GameManager.instance.pauseMode && !GameManager.instance.ventanaAbierta)
            AsignarOpcion();
    }

    void AsignarOpcion()
    {
        miOpcion = AnswerManager.instance.GetOpcion(index);
        text.text = miOpcion.frase;
        value = miOpcion.valor;
    }

    public void Clickado()
    {
        AnswerManager.instance.SetDamage(value);
        AnswerManager.instance.SetPulsado(true);
        AnswerManager.instance.InitButtons();
        AsignarOpcion();
    }
}