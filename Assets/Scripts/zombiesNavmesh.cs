using UnityEngine;
using UnityEngine.AI;

public class zombiesNavmesh : MonoBehaviour
{

    [Header("References")]
    public Transform player; // Assign in Inspector or tag "Player"
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Detection Settings")]
    public float patrolRadius = 8f;     // Patrol area radius
    public float chaseRange = 10f;      // Detection range
    public float attackRange = 2f;      // Distance to attack
    public float loseSightDelay = 3f;   // Time before giving up chase

    [Header("Attack Settings")]
    public float attackCooldown = 1.5f;

    private float lastAttackTime;
    private float lostSightTimer;
    private Vector3 patrolTarget;
    private bool chasingPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Auto-find player if not set
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }

        // Pick first patrol point
        PickNewPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            chasingPlayer = true;
            lostSightTimer = 0f; // Reset timer if we see player
        }
        else if (chasingPlayer)
        {
            lostSightTimer += Time.deltaTime;
            if (lostSightTimer > loseSightDelay)
                chasingPlayer = false; // Stop chasing if lost player too long
        }

        if (chasingPlayer)
        {
            if (distance > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                SetAnimation("Walk");
            }
            else
            {
                agent.isStopped = true;
                AttackPlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolTarget) < 1f)
        {
            PickNewPatrolPoint();
        }
        SetAnimation("Walk");
    }

    void PickNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
            agent.isStopped = false;
            agent.SetDestination(patrolTarget);
        }
    }

    void AttackPlayer()
    {
        SetAnimation("Attack");

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log($"{name} attacks the player!");
            // TODO: Reduce player health here
            lastAttackTime = Time.time;
        }
    }

    void SetAnimation(string state)
    {
        if (animator != null)
        {
            animator.Play(state); // Assumes you have "Walk" and "Attack" states
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}