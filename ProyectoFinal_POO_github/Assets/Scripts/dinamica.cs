//para importar namespaces: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Con este Script se activa la visualización de “Jugador” y se le activa la gravedad (useGravity) desde el código. 
public class dinamica : MonoBehaviour
{
    private MeshRenderer m_Renderer_Jugador;
    private Rigidbody rb_jugador;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Selecciono las componentes Mesh y RigidBody del GameObject jugador  -> inicializar las variables
        m_Renderer_Jugador = GetComponent<MeshRenderer>();
        rb_jugador = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Activo la componentes Mesh y en RigidBody activo la gravedad del GameObject jugador 
        m_Renderer_Jugador.enabled = true;      // Activa el MeshRenderer -> Si estuviera en false, el objeto no se vería en la escena, aunque seguiría existiendo
        rb_jugador.useGravity = true;
    }
}
