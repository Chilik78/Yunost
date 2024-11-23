using Global;
using ProgressModul;
using UnityEngine;

public static class GlobalInitScript
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
        InitServices();
    }

    private static void InitServices()
    {
        SceneControl sceneControl = new SceneControl(1);
        ServiceLocator.Register(sceneControl);
    }
}
