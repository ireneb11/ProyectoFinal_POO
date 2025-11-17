using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Necesario para trabajar con el sistema de navegación (NavMesh)
public class Enemigo : MonoBehaviour
{
    private GameObject miEnemigo, miJugador;      // Referencias al enemigo (este objeto) y al jugador
    private NavMeshAgent asistenteEnemigo;        // Componente NavMeshAgent que permite la navegación del enemigo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Busca en tu escena un GameObject con el nombre exacto:"Jugador"
        // Es importante que el jugador tenga este nombre en la jerarquía
        miJugador = GameObject.Find("Jugador");

        // Referencia a este GameObject, que representa al enemigo
        miEnemigo = this.gameObject;

        // Obtiene el componente NavMeshAgent del enemigo
        // Este componente controla el movimiento del enemigo en un NavMesh
        asistenteEnemigo = miEnemigo.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // Asigna como destino del enemigo la posición del jugador
        // Esto hace que el enemigo persiga al jugador automáticamente usando NavMesh
        asistenteEnemigo.destination = miJugador.transform.position;

    }

    
    private void OnCollisionEnter(Collision collision)      // Método OnCollisionEnter: se llama cuando este objeto colisiona con otro
    {
        // Obtiene el objeto con el que colisionó el enemigo
        GameObject playerCazado = collision.gameObject;    // collision.gameObject -> es el objeto que el enemigo ha tocado.

        
        if (playerCazado.tag == "Jugador")        // Comprueba si el objeto con el que colisionó tiene la etiqueta "Jugador"
        {
            // Si el objeto colisionado es el jugador, lo destruye
            // Esto simula que el enemigo "caza" al jugador
            Destroy(playerCazado);
        }
    }
}
