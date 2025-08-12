using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningMenu : MonoBehaviour
{
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
    void Update()
    {
       
        if (GameObject.FindGameObjectsWithTag("EnemyLayer").Length == 0)
        {
            SceneManager.LoadScene("Win");
        }
        
    }
}
