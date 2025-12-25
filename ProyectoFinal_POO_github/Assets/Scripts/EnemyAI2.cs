using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;
    public PlayerHealth playerHealth;

    [Header("Settings")]
    public float detectionRadius = 15f;
    public float attackRange = 2f;
    public float patrolRadius = 20f;
    public float attackCooldown = 2f;
    public float patrolIdleTime = 3f;
    public float rotationSpeed = 7f;
    public float attackDuration = 1.75f; // Duration of attack animation 

    private NavMeshAgent agent;
    private float cooldownTimer;
    private float idleTimer;
    private float attackTimer;

    private Vector3 patrolPoint;
    private bool isPatrolling;
    private bool isIdle;
    private bool isAttacking;

    private enum State { Patrol, Chase, Attack }
    private State currentState;



    public GameObject gameOverCanvas;  // canvas con el mensaje de Atrapado
    private bool gameOver = false;  // para proteger el update cuando el enemigo mata al jugador

    void Start()
    {
        gameOverCanvas.SetActive(false);   // empieza el canvas oculto

        agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();
        if (playerHealth == null && player != null) playerHealth = player.GetComponent<PlayerHealth>();

        // Ajusta la distancia de parada del enemigo antes de atacar
        agent.stoppingDistance = attackRange - 0.5f; // Se detendrá justo al llegar al rango de ataque

        SetNewPatrolPoint();
        currentState = State.Patrol;
    }

    void Update()
    {
        if (gameOver || player == null) return;
        cooldownTimer -= Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Cancel attack if player leaves attack range
        if (isAttacking && distanceToPlayer > attackRange)
        {
            CancelAttack();
            currentState = State.Chase;
        }

        // Handle attack duration manually (no animation event needed)
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                EndAttack();
            }
        }

        // State switching
        if (!isAttacking)
        {
            if (distanceToPlayer <= attackRange && cooldownTimer <= 0f)
                currentState = State.Attack;
            else if (distanceToPlayer <= detectionRadius)
                currentState = State.Chase;
            else
                currentState = State.Patrol;
        }

        // Execute state
        switch (currentState)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: ChasePlayer(); break;
            case State.Attack: Attack(); break;
        }

        animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f && !isAttacking);

        if (!isAttacking)
            RotateTowardsMovementDirection();
    }

    void Patrol()
    {
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= patrolIdleTime)
            {
                SetNewPatrolPoint();
                idleTimer = 0f;
            }
            return;
        }

        if (!isPatrolling || Vector3.Distance(transform.position, patrolPoint) < 1.5f)
        {
            isIdle = true;
            isPatrolling = false;
            agent.ResetPath();
        }
    }

    void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            agent.SetDestination(patrolPoint);
            isPatrolling = true;
            isIdle = false;
        }
    }

    void ChasePlayer()
    {
        isIdle = false;
        isPatrolling = false;

        if (agent.isOnNavMesh && player != null)
            agent.SetDestination(player.position);
    }

    void Attack()
    {
        if (isAttacking) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            currentState = State.Chase;
            return;
        }

        isAttacking = true;
        cooldownTimer = attackCooldown;
        attackTimer = attackDuration;
        agent.ResetPath();

        // Rotate to face player instantly
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position), Time.deltaTime * rotationSpeed);

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");

        // Espera a que termine la animación antes de Game Over
        Invoke(nameof(TriggerGameOver), attackDuration);

        
    }
    void TriggerGameOver()
    {
        
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Game Over");
            // SceneManager.LoadScene("GameOver");    //cambia de escena a la de game over
            gameOverCanvas.SetActive(true);    // muestra el canvas 

            // Destruir jugador
            // player.gameObject.SetActive(false);
            player = null;
            
            Time.timeScale = 0f;    // para el juego
            StartCoroutine(EsperarYContinuar());  // lanza la espera
             
        }
    }

    IEnumerator EsperarYContinuar()
    {
        yield return new WaitForSecondsRealtime(2f); // espera REAL aunque el juego esté en pausa

        Time.timeScale = 1f; // MUY IMPORTANTE: reanudar el tiempo
        SceneManager.LoadScene("ScreenStart");
    }


    public void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;

        
    }

    public void CancelAttack()
    {
        if (!isAttacking) return;

        isAttacking = false;
        attackTimer = 0f;
        cooldownTimer = attackCooldown;

        animator.ResetTrigger("Attack");

        // Instantly cut the attack animation
        if (animator.HasState(0, Animator.StringToHash("Walk")))
            animator.CrossFade("Walk", 0.1f);
        else if (animator.HasState(0, Animator.StringToHash("Walk")))
            animator.CrossFade("Walk", 0.1f);

        if (agent.isOnNavMesh && player != null)
            agent.SetDestination(player.position);
    }

    void RotateTowardsMovementDirection()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

