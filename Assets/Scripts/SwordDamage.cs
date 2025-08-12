using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 5;
    public PlayerControllerFull player;

    private void OnTriggerEnter(Collider other)
    {
        if (player != null && player.isAttacking)
        {
            if (other.CompareTag("EnemyLayer"))
            {
                ZombieAI zombie = other.GetComponent<ZombieAI>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
            }
        }
    }
}
