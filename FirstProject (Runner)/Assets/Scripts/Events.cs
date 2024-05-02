using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public GameObject PausePanel;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseButtonPressed()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueButtonPressed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }
}
