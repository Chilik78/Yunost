using UnityEngine;
using ProgressModul;

public class Script : MonoBehaviour
{

    void Start()
    {
        PlayerStats playerStats = new(100, 100);

        playerStats.HealthChanged += func;
        playerStats.StaminaChanged += func2;

        Debug.Log($"{playerStats.Health}");
        playerStats.Health = 50;
        Debug.Log($"{playerStats.Health}");

        Debug.Log($"{playerStats.Stamina}");
        playerStats.Stamina = 50;
        Debug.Log($"{playerStats.Stamina}");
    }

    void func()
    {
        Debug.Log("А это изменилось здоровье");
    }

    void func2()
    {
        Debug.Log("А тут стамина");
    }
}
