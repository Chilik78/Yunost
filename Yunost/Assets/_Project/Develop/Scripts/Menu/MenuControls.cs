using Global;
using ProgressModul;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("is_loaded", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("is_loaded", 1);
        SceneManager.LoadScene(1);
    }

    public void SaveGame()
    {
        ServiceLocator.Get<SaveLoadSystem>().SaveGame(SaveType.File);
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Выход отработал");
    }
}
