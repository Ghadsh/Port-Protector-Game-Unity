using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false); // hide at start
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // show when paused
        Time.timeScale = 0f;          // freeze gameplay
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // hide again
        Time.timeScale = 1f;          // unfreeze gameplay
    }
}