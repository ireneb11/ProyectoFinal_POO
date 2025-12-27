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


    void Awake() { 
        instance = this;
    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActualizarTexto();

    }

    
    public void AddPoint(){
        objetosRecogidos += 1;
        ActualizarTexto();


    }

    void ActualizarTexto()
    {
        objetosRecogidosText.text = objetosRecogidos + " / " + totalObjetos;
    }
}
