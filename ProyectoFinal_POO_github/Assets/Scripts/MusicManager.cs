using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class MusicManager : MonoBehaviour
{
    // declaración de la instancia única del Singleton
    private static MusicManager thisInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Si la instancia no está inicializada, asignamos esta como la instancia única
        if (thisInstance == null)
        {
            thisInstance = this;

            // Hace que el objeto persista al cambiar de escena
            DontDestroyOnLoad(thisInstance);
        }
        else 
        {
            // Si ya existe una instancia, destruimos este objeto para mantener la unidad
            Destroy(this.gameObject);
        
        }
        
    }

}
