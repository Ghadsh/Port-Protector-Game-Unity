using UnityEngine;

public class SceneEndHandler : MonoBehaviour
{
    [Header("Assign the target position in the Inspector")]
    public Transform endSceneSpawnPoint;

    private Transform playerTransform;

    void Start()
    {
        // Assume this script is on the player or find the player in the scene
        playerTransform = this.transform; // If attached to player
        // Or: playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Call this method to "end the scene" and move player
    public void EndScene()
    {
        if (endSceneSpawnPoint != null)
        {
            playerTransform.position = endSceneSpawnPoint.position;
            playerTransform.rotation = endSceneSpawnPoint.rotation;

            Debug.Log("Player moved to new spawn point.");
        }
        else
        {
            Debug.LogWarning("End scene spawn point is not assigned!");
        }
    }
}
