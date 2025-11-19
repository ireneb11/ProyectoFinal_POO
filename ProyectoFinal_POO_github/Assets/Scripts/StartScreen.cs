using UnityEngine;         
using System.Collections; // Incluye clases para manipular colecciones y enumeradores
using System.Collections.Generic; // Proporciona clases genéricas como listas y diccionarios using UnityEngine; // Contiene las clases base para trabajar con Unity (GameObjects, MonoBehaviour, etc.)
using UnityEngine.SceneManagement; // Permite gestionar las escenas en Unity

public class StartScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start_Game()
    {
        // Obtiene la escena actualmente activa y la almacena en una variable de tipo Scene
        Scene miEscena = SceneManager.GetActiveScene();

        // Recupera el indice de construcción (build index) de la escena activa
        // El indice corresponde a la posición de la escena en los Build Settings
        int nivel = miEscena.buildIndex;

        // Imprime en la consola el índice de la escena activa para fines de depuración
        Debug.Log($"Estoy en el nivel: {nivel}");

        // Carga la siguiente escena basada en el índice actual
        // Al sumar 1 al indice actual, se pasa a la siguiente escena en los Build Settings
        SceneManager.LoadScene(nivel + 1);

    }


    // Update is called once per frame
    public void Quit(){
        // Cierra la aplicación. Esto solo funciona en un entorno construido (ejecutable o móvil)
        // En el editor de Unity, este comando no tiene efecto visible
        Application.Quit();

    }
}
