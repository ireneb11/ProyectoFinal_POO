//para importar namespaces: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinamica : MonoBehaviour
{
    // DECLARACIÓN DE VARIABLES
    private MeshRenderer m_Renderer_Jugador;                     // MeshRenderer -> componente que hace visible un objeto 3D
    private Rigidbody rb_jugador;                               // Rigidbody -> componente que le da física

    private float coordenadaX, coordenadaY, coordenadaZ;
    public float multiplicadorDesplazamiento = 8.0f;
    private Vector3 velocidad_i;

    private GameObject gameManager;
    private AudioSource eaten;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Selecciono las componentes Mesh y RigidBody del GameObject jugador  -> inicializar las variables
        //GetComponent<>() busca un componente en el  objeto
        m_Renderer_Jugador = GetComponent<MeshRenderer>();         //Busca el MeshRendered del GameObject y lo asigna a la variable
        rb_jugador = GetComponent<Rigidbody>();


        //Activo la componentes Mesh y en RigidBody activo la gravedad del GameObject jugador 
        m_Renderer_Jugador.enabled = true;      // Activa el MeshRenderer -> Si estuviera en false, el objeto no se vería en la escena, aunque seguiría existiendo
        rb_jugador.useGravity = true;

        // AUDIO 
        // tenemos que seleccionar el GameObject GameManager que es el que contiene los audios
        gameManager = GameObject.Find("GameManager");     // Cargo el objeto donde está el componente de audio
        eaten = gameManager.GetComponent<AudioSource>();   // cargo el sonido de captura del bonus
    }

    // Update is called once per frame
    void Update()
    {
        // MOVERNOS POR TECLADO
        
        coordenadaY = rb_jugador.linearVelocity.y;   // obtenemos la componente 'y' de la velocidad del rigidbody. Hay fuerzas que afectaran al eje Y, no queremos que el jugador lo controle por el teclado

        //Pido a Unity el desplzamiento horizontal y vertical desde el teclado y lo multiplico por un incremento (velocidad del movimiento del jugador[8.0f]).
        coordenadaX = Input.GetAxis("Horizontal") * multiplicadorDesplazamiento;  // A / Flecha izquierda = -1    // D / Flecha derecha= 1.
        coordenadaZ = Input.GetAxis("Vertical") * multiplicadorDesplazamiento;    // S / Flecha abajo => "Vertical" =-1      // W / Flecha arriba = 1


        //Creo la nueva velocidad, considerando que la coordenada Y no la controlo por teclado.
        velocidad_i = new Vector3(coordenadaX, coordenadaY, coordenadaZ);     //x,z -> horizontal      y -> vertical

        //Actualizo en cada instante la velocidad y, en consecuencia, la posición.
        rb_jugador.linearVelocity = velocidad_i;   // Le dices al RigidBody cual es su nueva velocidad en cada frame
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
