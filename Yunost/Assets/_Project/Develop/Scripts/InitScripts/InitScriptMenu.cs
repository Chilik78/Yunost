using Global;
using ProgressModul;
using UnityEngine;

public class InitScriptMenu : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        SceneControl sceneControl = new SceneControl(1);
        ServiceLocator.Register(sceneControl);
    }
}
