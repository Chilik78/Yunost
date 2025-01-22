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
        DialogVariables dialogVariables = new DialogVariables();

        SaveLoadSystem saveLoadSystem = ServiceLocator.Get<SaveLoadSystem>();
        saveLoadSystem.AddToSaveLoad(taskObserver);
        saveLoadSystem.AddToSaveLoad(listOfItems);
        saveLoadSystem.AddToSaveLoad(playerStats);
        saveLoadSystem.AddToSaveLoad(dialogVariables);

        ServiceLocator.Register(taskObserver);
        ServiceLocator.Register(listOfItems);
        ServiceLocator.Register(playerStats);
        ServiceLocator.Register(timeControl);
        ServiceLocator.Register(dialogVariables);
    }

    public static void UnregisterServices()
    {
        ServiceLocator.Unregister<TaskObserver>();
        ServiceLocator.Unregister<ListOfItems>();
        ServiceLocator.Get<PlayerStats>().ClearAllListeners();
        ServiceLocator.Unregister<PlayerStats>();
        ServiceLocator.Unregister<TimeControl>();
        ServiceLocator.Unregister<DialogVariables>();
    }

    private static void InitGlobalServices()
    {
        SceneControl sceneControl = new SceneControl();
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
       
        ServiceLocator.Register(sceneControl);
        ServiceLocator.Register(saveLoadSystem);

        saveLoadSystem.AddToSaveLoad(sceneControl);
    }
}
