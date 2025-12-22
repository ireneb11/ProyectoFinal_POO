using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Personaje : MonoBehaviour
{

    // VARIABLES DE MOVIMIENTO   ===============================

    public float multiplicadorVelocidad = 8.0f;   // Multiplicador para la velocidad de desplazamiento del personaje    // AL ser estática, esta variable es compartida entre todas las instancias de esta clase.
    public float multiplicadorRotacion = 10.0f;   // Multiplicador para la velocidad de rotación del personaje

    
    
    


    // VARIABLES DE INPUT   ================================
    
    private float listenerX, listenerY;  // Entrada del teclado en los ejes X (horizontal) e Y (vertical).


    // VARIABLES DE ANIMACIÓN   ================================
    private Animator animacionPersonaje;   // Referencia al componente Animator del GameObject   // Esto controlará las animaciones del personaje.


    // VARIABLES DE AUDIO ================================
    private GameObject gameManager;   // Referencia al GameManager (donde está el AudioSource)
    private AudioSource eaten;    // Sonido que se reproduce al coger un objeto "Eatable"






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        




        // Obtenemos el componente Animator del GameObject al que está asignado este script.
        // Esto permite manipular las animaciones asociadas al persd aje.
        animacionPersonaje = GetComponent < Animator> ();


        // AUDIO ================
        // tenemos que seleccionar el GameObject GameManager que es el que contiene los audios
        gameManager = GameObject.Find("GameManager");     // Cargo el objeto donde está el componente de audio
        eaten = gameManager.GetComponent<AudioSource>();   // cargo el sonido de captura del bonus

    }

    // Update is called once per frame
    void Update()
    {
        // Capturamos Las entradas del teclado en los ejes "Horizontal" y "Vertical" definidos en el Input Manager de Unity.
        // Por ejemplo, las teclas "A" y "D" controlan el eje Horizontal, mientras que "W" y "S" controlan el eje Vertical.
        listenerX = Input.GetAxis("Horizontal"); // Movimiento a la izquierda (-1) o derecha (+1).
        listenerY = Input.GetAxis("Vertical"); // Movimiento hacia atrás (-1) o adelante (+1).

        // Rotamos al personaje sobre su eje Y (arriba y abajo) según la entrada en el eje Horizontal.
        // "Tine.deltaTime'asegura que la rotación sea consistente independientemente del rendimiento del dispositivo.
        transform.Rotate(0.0f, (Time.deltaTime * multiplicadorRotacion) * listenerX, 0.0f);




        // Movemos al personaje hacia adelante o atrás (eje Z) según la entrada en el eje Vertical. Usamos "Time deltaTime' para garantizar una velocidad uniforme en diferentes dispositivos.
       
        // MOVIMIENTO FRONTAL
        Vector3 movimientoFrontal = transform.forward * listenerY * multiplicadorVelocidad * Time.deltaTime;
        // MOVIMIENTO LATERAL (strafe)
        Vector3 movimientoLateral = transform.right * listenerX * multiplicadorVelocidad * Time.deltaTime;
        // APLICAMOS EL MOVIMIENTO TOTAL
        transform.Translate(movimientoFrontal + movimientoLateral, Space.World);
        
        // transform.Translate(0.0f, 0.0f, (Time.deltaTime * multiplicadorVelocidad) * listenerY,Space.Self);




        // actualizamos valor de vx y vy en el animator
        // variables que se pueden usar en el controlador de animacion pra cambiar entre animaciones (como caminar y correr)
        animacionPersonaje.SetFloat("Vx", listenerX);   // enviar el valor del movimiento lateral al Animator
        animacionPersonaje.SetFloat("Vy", listenerY);   // enviar el valor del movimiento frontal al Animator


    }

    private void OnCollisionEnter(Collision collision)      // cuando este GameObject colisiona con otro
    {
        GameObject colisionado = collision.gameObject;     // Obtiene el GameObject con el que ocurrió la colisión
        GameObject colisionante = this.gameObject;          // Obtiene el GameObject que está colisionando, es decir, el que tiene este script
        Debug.Log("Me lo he zampado");                      // Imprime un mensaje en la consola para indicar que ha sucedido la colisión
        string tagColisionado = colisionado.tag;            // Obtiene la etiqueta (tag) del objeto colisionado
        string tagColisionante = colisionante.tag;          // Obtiene la etiqueta (tag) del objeto que está colisionando

        if (tagColisionado == "Eatable")                    // Si el objeto colisionado tiene la etiqueta "Eatable"
        {
            eaten.Play();                                  // reproduzco el sonido
            Destroy(colisionado);                           // Destruye el objeto colisionado
        }


    }
}
