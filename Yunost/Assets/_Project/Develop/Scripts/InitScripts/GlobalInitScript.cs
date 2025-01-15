using Global;
using ProgressModul;
using UnityEngine;

public static class GlobalInitScript
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
        InitGlobalServices();
    }

    public static void InitServices()
    {
        TaskObserver taskObserver = new();
        ListOfItems listOfItems = new();
        PlayerStats playerStats = new(100, 100);
        TimeControl timeControl = new(14, 0);

        SaveLoadSystem saveLoadSystem = ServiceLocator.Get<SaveLoadSystem>();
        saveLoadSystem.AddToSaveLoad(taskObserver);
        saveLoadSystem.AddToSaveLoad(listOfItems);
        saveLoadSystem.AddToSaveLoad(playerStats);
        ServiceLocator.Register(taskObserver);
        ServiceLocator.Register(listOfItems);
        ServiceLocator.Register(playerStats);
        ServiceLocator.Register(timeControl);

        ServiceLocator.Get<SceneControl>().StartLoading += () => SetSceneIndicator();
    }

    static public void SetSceneIndicator()
    {
        PlayerPrefs.SetInt("IsFirstInScene", 1);
    }

    public static void UnregisterServices()
    {
        ServiceLocator.Get<SceneControl>().StartLoading -= () => ServiceLocator.Get<PlayerStats>().SetDefault();
        ServiceLocator.Unregister<TaskObserver>();
        ServiceLocator.Unregister<ListOfItems>();
        ServiceLocator.Unregister<PlayerStats>();
        ServiceLocator.Unregister<TimeControl>();
    }

    private static void InitGlobalServices()
    {
        SceneControl sceneControl = new SceneControl(1);
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
       
        ServiceLocator.Register(sceneControl);
        ServiceLocator.Register(saveLoadSystem);

        saveLoadSystem.AddToSaveLoad(sceneControl);
    }
}
