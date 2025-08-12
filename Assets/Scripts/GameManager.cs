using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalEnemies;
    public int enemiesKilled;
    public TextMeshProUGUI enemyCounterText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CountEnemiesInLayer("EnemyLayer");
        UpdateEnemyCounterUI();
    }

    void CountEnemiesInLayer(string layerName)
    {
        int enemyLayer = LayerMask.NameToLayer(layerName);
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        totalEnemies = 0;
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                totalEnemies++;
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateEnemyCounterUI();
    }

    void UpdateEnemyCounterUI()
    {
        int enemiesLeft = totalEnemies - enemiesKilled;
        if (enemyCounterText != null)
        {
            enemyCounterText.text = $"Killed: {enemiesKilled} / Left: {enemiesLeft}";
        }
    }
}
