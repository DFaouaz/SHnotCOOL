using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AcabarTrimestre : MonoBehaviour {
    bool col, finTrim;
    public GameObject TrimMark;
	public Vector3 posInicial;


    void TrimestreAcabado()
    {
        if ((GameManager.instance.trimestre == 1 && (GameManager.instance.finMates && GameManager.instance.finHistoria)) || (GameManager.instance.trimestre == 2 && (GameManager.instance.finHistoria && GameManager.instance.finMates && GameManager.instance.finLengua))||( GameManager.instance.trimestre == 3 && (GameManager.instance.finHistoria && GameManager.instance.finMates && GameManager.instance.finLengua && GameManager.instance.finGeo)))
        {
            finTrim = true;
            TrimMark.SetActive(true);
			if (col && Input.GetKeyDown (GameManager.instance.botonInteractuar)) {
				GameManager.instance.lastPosPasillos = posInicial;
				GameManager.instance.lastPosEntrance = Vector3.zero;
				CambioTrimestre ();
			}
                
        }
        else
            TrimMark.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&&finTrim)
        {
            MessageManager.instance.ShowMessage("Pulsa " + GameManager.instance.botonInteractuar.ToString() + " para pasar al siguiente\ntrimestre");
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
    void CambioTrimestre(){
        GameManager.instance.media = (GameManager.instance.historiaScore + GameManager.instance.matematicasScore + GameManager.instance.lenguaScore + GameManager.instance.GeoScore) / (float)(GameManager.instance.trimestre + 1);
        GameManager.instance.notaFinal += GameManager.instance.media;
        finTrim = false;
        TrimMark.SetActive(false);
		col = false;
        if (GameManager.instance.trimestre != 3)
        {       
			if (GameManager.instance.trimestre == 2) {
				chinoGordinflon.CheckComida ();
				EmosSuicidasion.CheckMuerte ();
			}
            SceneManager.LoadScene("CambioTrimestre");
            GameManager.instance.finGeo = false;
            GameManager.instance.finHistoria = false;
            GameManager.instance.finLengua = false;
            GameManager.instance.finMates = false;
            GameManager.instance.GeoScore = 0;
            GameManager.instance.historiaScore = 0;
            GameManager.instance.matematicasScore = 0;
            GameManager.instance.lenguaScore = 0;
            GameManager.instance.trimestre++;
            GameManager.instance.dinero += 100;
            LoadNewMissions();
        }
        else
        {
            GameManager.instance.notaFinal = GameManager.instance.notaFinal / 3;
            SceneManager.LoadScene("FinJuego");

        }
    }
    private void Update()
    {
        TrimestreAcabado();
    }

	void LoadNewMissions(){
		NPC[] npcs = FindObjectsOfType<NPC> ();
		foreach (NPC i in npcs) {
			if (!i.isAcepted && i.tipoDeMision == Mission.TipoDeMision.None) {
				i.LeeMision ();
				if (i.tipoDeMision != Mission.TipoDeMision.None && i.missionMark != null)
					i.missionMark.UpdateRender (MissionMark.Est.Exclamacion);
				else if(i.missionMark != null)
					i.missionMark.UpdateRender (MissionMark.Est.None);
			}
		}
	}
}
