using Global;
using ProgressModul;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void StartGame()
    {
        ServiceLocator.Get<SceneControl>().Init();
    }

    public void LoadGame()
    {
        ServiceLocator.Get<SceneControl>().Load();
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
