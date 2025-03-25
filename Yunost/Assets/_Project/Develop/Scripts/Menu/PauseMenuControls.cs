using Global;
using ProgressModul;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            Debug.Log("Выход в паузу отработал");
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /*    public void QuitGame()
        {
            Application.Quit();
        }*/

    public void BackToMenu()
    {
        GlobalInitScript.UnregisterServices();
        SceneManager.LoadScene("Menu");
        Resume();
        Debug.Log("Переход к меню отработал");
    }

/*    public void Start()
    {
        
        pauseMenuUI = transform.gameObject;
        pauseMenuUI.SetActive(false);
    }*/
}
