using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;     // para poder usar las funciones relacionadas con las escenas en Unity (cargar, cambiar, recargar)


public class GameManager : MonoBehaviour      // gameManager hereda de MonoBehaviour
{
    private int nivel;                            // guarda el índice de la escena actual en la Build Settings
    private string nombreNivel;                   // guarda el nombre de la escena actual
    private GameObject miGestor, miJugador;       // miGestor ? referencia al GameObject que contiene este script  (GameManager)
                                                  // miJugador ? referencia al jugador


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene escenaActiva = SceneManager.GetActiveScene();    // obtiene la escena que está actualmente cargada y activa en Unity
        nombreNivel = escenaActiva.name;                       // Guardas el nombre de la escena activa en la variable nombreNivel
        nivel = escenaActiva.buildIndex;                      // índice de la escena en la lista de Build Settings.
        miGestor = this.gameObject;                           // Guarda el GameObject que contiene este script en la variable miGestor
    }

    // Update is called once per frame
    void Update()
    {
        int numBonus = miGestor.transform.childCount;    // devuelve cuántos hijos (bonus) tiene el GameObject “miGestor” en la jerarquía

        if (numBonus == 0)                      // Si no hay bonus, significa que el jugador ha terminado el nivel
        {
            if (nivel == 1)                    // nivel es el índice de la escena (buildIndex)
            {                                  
                SceneManager.LoadScene(2);     // Si el nivel actual es 0 -> carga la escena 1
            }
            else if (nivel == 2)
            {
                SceneManager.LoadScene(1);    // Si el nivel actual es 1 -> carga la escena 0.
            }
        }

    }
}
