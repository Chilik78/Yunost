using UnityEngine;
using ProgressModul;
using Global;

public class InitScript : MonoBehaviour
{
    private Object _gameSystems;
    void Awake()
    {
        PlayerStats playerStats = new(100, 100);
        ServiceLocator.Register(playerStats);
        _gameSystems = Instantiate(Resources.Load("GameSystems"));
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister<PlayerStats>();
        Destroy(_gameSystems);
    }
}
