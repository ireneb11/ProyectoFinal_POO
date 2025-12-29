using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ScoreManagement : MonoBehaviour
{
    public static ScoreManagement instance;

    
    public TextMeshProUGUI objetosRecogidosText;

    int objetosRecogidos = 0;

    [Header("Configuración")]
    public int totalObjetos = 4;

    [Header("Objetos Recoger UI")]
    public List<GameObject> imagenesObjetos;   // lista donde iran las imagenes de monedas


    void Awake() { 
        instance = this;
    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActualizarTexto();
        
        foreach (GameObject moneda in imagenesObjetos)  // Desactiva todas las monedas al inicio
        {
            moneda.SetActive(false);
        }

        
    }

    
    public void AddPoint(GameObject monedaObjeto)
    {
        objetosRecogidos += 1;
        ActualizarTexto();

        // Activar la moneda correspondiente si no es null
        if (monedaObjeto != null)
        {
            monedaObjeto.SetActive(true);
        }

    }

    void ActualizarTexto()
    {
        objetosRecogidosText.text = objetosRecogidos + " / " + totalObjetos;
    }
}
