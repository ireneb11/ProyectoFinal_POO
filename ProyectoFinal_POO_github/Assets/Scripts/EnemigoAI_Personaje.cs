using UnityEngine;
using UnityEngine.AI;    // Necesario para trabajar con el sistema de navegación (NavMesh)

public class EnemigoAI_Personaje : MonoBehaviour
{

    public NavMeshAgent agent;          // Agente de navegación
    public Transform player;            // Referencia al jugador

    public LayerMask whatIsGround;      // Capa del suelo
    public LayerMask whatIsPlayer;      // Capa del jugador

    // ===== PATRULLA =====
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;

    // ===== DETECCIÓN =====
    public float sightRange = 8f;       // Distancia para detectar al jugador
    public float killRange = 1.2f;      // Distancia para matar al jugador

    bool playerInSightRange;
    bool playerInKillRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInKillRange = Physics.CheckSphere(transform.position, killRange, whatIsPlayer);

        if (!playerInSightRange) Patroling();

        if (playerInSightRange && !playerInKillRange) ChasePlayer();

        if (playerInKillRange) KillPlayer();



        
    }

    // ===== PATRULLA =====
    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(walkPoint, Vector3.down, 2f, whatIsGround))
            walkPointSet = true;
    }

    // ===== PERSECUCIÓN =====
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    // ===== MUERTE DEL JUGADOR =====
    void KillPlayer()
    {
        agent.SetDestination(transform.position);
        Debug.Log("Jugador cazado");
        Destroy(player.gameObject);
    }




    // ===== GIZMOS =====
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killRange);
    }
}
