using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [Tooltip("Where the player will be teleported to")]
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport player to spawn position, slightly above ground to avoid clipping
            other.transform.position = spawnPoint.position + Vector3.up * 1f;

            // Reset player rotation to face forward (adjust as needed)
            other.transform.rotation = Quaternion.Euler(0, 0, 0);

            Debug.Log("Player teleported to spawn point.");
        }
    }
}
