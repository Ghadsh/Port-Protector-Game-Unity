using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 1;
    public LayerMask enemyLayer;
    public Animator animator;

    private float attackCooldown = 1f;
    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimer <= 0f)
        {
            animator.SetTrigger("Attack1"); 
            Attack();
            attackTimer = attackCooldown;
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<ZombieAI>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
