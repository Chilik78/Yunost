using Global;
using ProgressModul;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    Health,
    Stamina
}

public class BarController : MonoBehaviour
{
    public Image bar;
    public BarType type;

    void Start()
    {
        var playerStats = ServiceLocator.Get<PlayerStats>();

        switch (type)
        {
            case BarType.Health:
                bar.fillAmount = playerStats.Health / 100.0f;
                playerStats.HealthChanged += () => setFill(playerStats);
                break;
            case BarType.Stamina:
                bar.fillAmount = playerStats.Stamina / 100.0f;
                playerStats.StaminaChanged += () => setFill(playerStats);
                break;
            default:
                bar.fillAmount = 0;
                break;
        }
    }

    private void OnDestroy()
    {
        if (type == BarType.Health) ServiceLocator.Get<PlayerStats>().ClearAllListeners();
    }

    void setFill(PlayerStats playerStats)
    {
        Debug.LogWarning($"{bar} is setting {playerStats.Health}");
        switch (type)
        {
            case BarType.Health:
                bar.fillAmount = playerStats.Health / 100.0f;
                break;
            case BarType.Stamina:
                bar.fillAmount = playerStats.Stamina / 100.0f;
                break;
            default:
                bar.fillAmount = 0;
                break;
        }
    }
}
