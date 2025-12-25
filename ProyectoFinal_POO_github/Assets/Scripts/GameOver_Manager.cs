using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Manager : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Vuelve al menú principal
    public void VolvernuPrincipal()
    {
        SceneManager.LoadScene("ScreenStart"); 
    }

    public void Quit()
    {
        // Cierra la aplicación. Esto solo funciona en un entorno construido (ejecutable o móvil)
        // En el editor de Unity, este comando no tiene efecto visible
        Application.Quit();

        Debug.Log("Aplicación cerrada."); // Solo para probar en el editor

    }
}
