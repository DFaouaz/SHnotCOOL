using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Personajes { public bool completado;public string personaje; }
public class MisionManager : MonoBehaviour {
    public static MisionManager instance = null;

   	public Personajes lista;
        
  

    void Awake()
    {
        // Si no hay ningún objeto GameManager ya creado
        if (instance == null)
        {
            // Almacenamos la instancia actual
            instance = this;
            // Nos aseguramos de no destruir el objeto, es decir, 
            // de que persista, si cambiamos de escena
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Si ya existe un objeto GameManager, no necesitamos uno nuevo
            Destroy(this.gameObject);
        }
    }
	//Metodos para la clase Mission
	//Aceptar mision

}
