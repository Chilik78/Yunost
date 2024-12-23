using UnityEngine;
using ProgressModul;
using Global;

public class InitScript : MonoBehaviour
{

    void Awake()
    {
        PlayerStats playerStats = new(50, 100);
        ServiceLocator.Register(playerStats);
        TimeControl timeControl = new(14, 0);
        ServiceLocator.Register(timeControl);

        TextAsset initTasksJson = Resources.Load<TextAsset>("InitTasks");

        TaskObserver taskObserver = new(initTasksJson.text);
        ServiceLocator.Register(taskObserver);

        DontDestroyOnLoad(GameObject.Find("GameSystems"));
    }
}
