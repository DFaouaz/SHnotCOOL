using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrasdeVida : MonoBehaviour {
    float Health=100;
	// Use this for initialization
	void Start () {
        
            DisminuyeVida();
     

    }

    // Update is called once per frame
    private void Update()
    {
        ActualizaBarra();
    }

    void DisminuyeVida()
    {
        if(Health>0)
            Health-= 10f;
        
    }
    void AumentaVida()
    {
        if (Health <100)
            Health += 1f;
    }
    void ActualizaBarra()
    {
        gameObject.GetComponent<Slider>().value =Health;
    }
}
