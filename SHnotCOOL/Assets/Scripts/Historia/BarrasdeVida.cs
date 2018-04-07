using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health
{
    float vida;
    public Health()
    {
        vida = 100;
    }
    public void ModificaVida(float daño)
    {
        if (vida > 0 && vida<=100)
            vida-=daño;

    }
    public float DevuelveVida()
    {
        return vida;
    }
    public void TruncaVida()
    {
        if (vida > 100)
            vida = 100;
        else if (vida < 0)
            vida = 0;
    }
}
public class BarrasdeVida : MonoBehaviour {
    Health enemigo;
    Health player;
    GameObject barraEnemigo, barraPlayer;
	// Use this for initialization
	void Start () {
        barraEnemigo = GameObject.FindGameObjectWithTag("VidaEnemigo");
        barraPlayer = GameObject.FindGameObjectWithTag("VidaJugador");
        player = new Health();
        enemigo = new Health();
     

    }

    // Update is called once per frame
    private void Update()
    {
        Ataque();
        ActualizaBarras();
        player.TruncaVida();
        enemigo.TruncaVida();
        FinJuego();
    }
    void Ataque()
    {
        if(AnswerManager.instance.getPulsado()&& !AnswerManager.instance.getQuitadaVida())
        {
            float daño = AnswerManager.instance.getDaño();
            if(daño<=0 && daño>=-10)
            {
                player.ModificaVida(-daño);
            }
            else if(daño<-10)
            {
                player.ModificaVida(-daño);
                enemigo.ModificaVida(daño / 2);
            }
            else if(daño>0 && daño<=10 )
            {
                enemigo.ModificaVida(daño);
            }
            else
            {
                player.ModificaVida(-daño/2);
                enemigo.ModificaVida(daño);
            }
            AnswerManager.instance.setQuitadaVida(true);
        }
    }

    void FinJuego()
    {
        if (player.DevuelveVida() <= 0)
            AnswerManager.instance.JuegoGanado(false);
        else if (enemigo.DevuelveVida() <= 0)
            AnswerManager.instance.JuegoGanado(true);
    }
    void ActualizaBarras()
    {
        barraPlayer.GetComponent<Slider>().value =player.DevuelveVida();
        barraEnemigo.GetComponent<Slider>().value = enemigo.DevuelveVida();
    }
}
