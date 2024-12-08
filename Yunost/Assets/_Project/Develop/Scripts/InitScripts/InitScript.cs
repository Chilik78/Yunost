using UnityEngine;
using ProgressModul;
using Global;
using System.Collections.Generic;

public class InitScript : MonoBehaviour
{
    private Object _gameSystems;
    void Awake()
    {
        PlayerStats playerStats = new(100, 100);
        ServiceLocator.Register(playerStats);
        TimeControl timeControl = new(7, 0);
        ServiceLocator.Register(timeControl);

        TextAsset initTasksJson = Resources.Load<TextAsset>("InitTasks");

        TaskObserver taskObserver = new(initTasksJson.text);
        ServiceLocator.Register(taskObserver);

        _gameSystems = Instantiate(Resources.Load("GameSystems"));
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister<PlayerStats>();
        Destroy(_gameSystems);
    }
}
