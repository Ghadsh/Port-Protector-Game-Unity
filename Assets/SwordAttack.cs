using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage = 1; // Damage per hit

    void OnTriggerEnter(Collider other)
    {
        // Check if the thing we hit has a ZombieAI script
        ZombieAI zombie = other.GetComponent<ZombieAI>();

        if (zombie != null)
        {
            zombie.TakeDamage(damage);
        }
    }
}
