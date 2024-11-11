using Global;
using ProgressModul;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void StartGame()
    {
        ServiceLocator.Get<SceneControl>().Init();
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Выход отработал");
    }
}
