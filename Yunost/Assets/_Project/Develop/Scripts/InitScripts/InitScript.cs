using UnityEngine;
using ProgressModul;
using Global;
using System.IO;

public class InitScript : MonoBehaviour
{

    void Awake()
    {
        PlayerStats playerStats = new(50, 100);
        ServiceLocator.Register(playerStats);
        TimeControl timeControl = new(14, 0);
        ServiceLocator.Register(timeControl);

        TaskObserver taskObserver;

        if (File.Exists(TaskObserver.SaveFilePath))
        {
            taskObserver = new();
            taskObserver.LoadTasks();
        }
        else
        {
            TextAsset initTasksJson = Resources.Load<TextAsset>("InitTasks");
            taskObserver = new(initTasksJson.text);
        }
        
        ServiceLocator.Register(taskObserver);
        //DontDestroyOnLoad(GameObject.Find("GameSystems"));
    }

    private void OnDestroy()
    {
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
            Destroy(player);

        ServiceLocator.Unregister<PlayerStats>();
        ServiceLocator.Unregister<TimeControl>();
        ServiceLocator.Unregister<TaskObserver>();
    }

    private void OnApplicationQuit()
    {
        if (File.Exists(TaskObserver.SaveFilePath))
        {
            File.Delete(TaskObserver.SaveFilePath);
        }
    }
}
