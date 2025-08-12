using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float detectionRange = 4f;
    public float attackRange = 2f;
    public int maxHealth = 2;

    private int currentHealth;
    private float attackCooldown = 1.5f;
    private float attackTimer = 2f;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0f)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(1);
                    attackTimer = attackCooldown;
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Zombie health: " + currentHealth);

        if (currentHealth <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.EnemyKilled();
            }

            Destroy(gameObject); // This removes it from the scene
        }
    }

}