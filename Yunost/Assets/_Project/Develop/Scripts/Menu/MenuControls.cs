using Global;
using ProgressModul;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void StartGame()
    {
        GlobalInitScript.SetSceneIndicator();
        GlobalInitScript.InitServices();
        ServiceLocator.Get<SaveLoadSystem>().LoadDefault();
        ServiceLocator.Get<SceneControl>().Init();
    }

    public void LoadGame()
    {
        GlobalInitScript.InitServices();
        ServiceLocator.Get<SaveLoadSystem>().LoadGame(SaveType.File);
        ServiceLocator.Get<SceneControl>().InitLast();
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
