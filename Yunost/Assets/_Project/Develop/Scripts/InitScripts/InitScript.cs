using UnityEngine;
using ProgressModul;
using Global;

public class InitScript : MonoBehaviour
{

    void Start()
    {
        PlayerStats playerStats = new(100, 100);
        ServiceLocator.Register(playerStats);
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister<PlayerStats>();
    }
}
