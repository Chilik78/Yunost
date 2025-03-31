using Global;
using ProgressModul;
using UnityEngine;

public static class GlobalInitScript
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        InitGlobalServices();
    }

    private static void InitGlobalServices()
    {
        SaveLoadSystem saveLoadSystem = new SaveLoadSystem();
        ServiceLocator.Register(saveLoadSystem);
    }
}