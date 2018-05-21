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
        if (vida > 0 && vida <= 100)
            vida -= daño;
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

public class BarrasDeVida : MonoBehaviour {

    public Animator enemyAnimator;

    Health enemigo, player;
    GameObject barraEnemigo, barraPlayer;
    Text finHistoria;
    int indiceVida = 20;
    bool resucitado = false;

    void Start() {

        barraEnemigo = GameObject.FindGameObjectWithTag("VidaEnemigo");
        barraPlayer = GameObject.FindGameObjectWithTag("VidaJugador");
        finHistoria = GameObject.FindGameObjectWithTag("FinHistoria").GetComponent<Text>();

        player = new Health();
        enemigo = new Health();
        enemyAnimator.SetInteger("Trimestre", GameManager.instance.trimestre);
    }

    public void Ataque()
    {
        if (AnswerManager.instance.GetPulsado() && !AnswerManager.instance.GetVidaRestada())
        {
            float daño = AnswerManager.instance.GetDamage();

            if (daño <= 0 && daño >= -indiceVida)
                player.ModificaVida(-daño);
            else if (daño < -indiceVida)
            {
                player.ModificaVida(-daño);
                enemigo.ModificaVida(daño / 2);
            }
            else if (daño > 0 && daño <= indiceVida)
                enemigo.ModificaVida(daño);
            else
            {
                player.ModificaVida(-daño / 2);
                enemigo.ModificaVida(daño);
            }

            AnswerManager.instance.SetVidaRestada(true);
            ActualizaBarras();
            player.TruncaVida();
            enemigo.TruncaVida();
            FinJuego();
        }
    }

    void FinJuego()
    {
        if (player.DevuelveVida() <= 0)
        {
            finHistoria.text = "Una Pena";
            GameManager.instance.historiaScore = 0;
            GameManager.instance.FinExamenHistoria();
        }
        else if (enemigo.DevuelveVida() <= 0 && (GameManager.instance.trimestre != 3 || resucitado))
        {
            finHistoria.text = "Bien Hecho";
            GameManager.instance.historiaScore = (int)(player.DevuelveVida() / 10);
            GameManager.instance.FinExamenHistoria();
        }
        else if (enemigo.DevuelveVida() <= 0 && GameManager.instance.trimestre == 3 && !resucitado)
        {
            enemyAnimator.SetBool("Resucitado", true);
            resucitado = true;
            enemigo=new Health();
            ActualizaBarras();
        }

    }

    void ActualizaBarras()
    {
        barraPlayer.GetComponent<Slider>().value = player.DevuelveVida();
        barraEnemigo.GetComponent<Slider>().value = enemigo.DevuelveVida();
    }
}