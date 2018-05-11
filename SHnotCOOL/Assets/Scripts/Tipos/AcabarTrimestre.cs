﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AcabarTrimestre : MonoBehaviour {
    bool col, finTrim;
    public GameObject TrimMark;
    public HUDManager hud;

    void TrimestreAcabado()
    {
        if ((GameManager.instance.trimestre == 1 && (GameManager.instance.finMates && GameManager.instance.finHistoria)) || (GameManager.instance.trimestre == 2 && (GameManager.instance.finHistoria && GameManager.instance.finMates && GameManager.instance.finLengua)))
        {
            finTrim = true;
            TrimMark.SetActive(true);
            if (col && Input.GetKeyDown(GameManager.instance.botonInteractuar))
                CambioTrimestre();
                
        }
        else if (GameManager.instance.trimestre == 3 && (GameManager.instance.finHistoria && GameManager.instance.finMates && GameManager.instance.finLengua && GameManager.instance.finGeo))
        {
            GameManager.instance.notaFinal = GameManager.instance.media / 3;
            //fin juego
        }
        else
            TrimMark.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&finTrim)
        {
            MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString() + "pasar al siguiente trimestre");
            col = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MessageManager.instance.CloseMessage();
            col = false;
        }
    }
    void CambioTrimestre()
    {

        GameManager.instance.media += (GameManager.instance.historiaScore + GameManager.instance.matematicasScore + GameManager.instance.lenguaScore + GameManager.instance.GeoScore) / (GameManager.instance.trimestre + 1);
        GameManager.instance.finGeo = false;
        GameManager.instance.finHistoria = false;
        GameManager.instance.finLengua=false;
        GameManager.instance.finMates = false;
        GameManager.instance.GeoScore = 0;
        GameManager.instance.historiaScore = 0;
        GameManager.instance.matematicasScore = 0;
        GameManager.instance.lenguaScore = 0;
        GameManager.instance.trimestre++;
        GameManager.instance.dinero += 100;
        hud.UpdateMoney();
        finTrim = false;
        TrimMark.SetActive(false);
    }
    private void Update()
    {
        TrimestreAcabado();
    }
}