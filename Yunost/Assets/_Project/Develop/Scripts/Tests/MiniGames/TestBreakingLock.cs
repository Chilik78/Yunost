using MiniGames;
using UnityEngine;

public class TestBreakingLock : MonoBehaviour
{

    private MiniGamesManager minigameManager;

    void Start()
    {
        minigameManager = gameObject.GetComponent<MiniGamesManager>();  
        MiniGameContext context = new MiniGameContext(TypesMiniGames.BreakingLock, 0, 5);
        minigameManager.RunMiniGame(context);
        minigameManager.MiniGameEnd += OnMiniGameEnd;
    }

    void OnMiniGameEnd(MiniGameResultInfo result)
    {
        Debug.Log("Игра завершена");
        minigameManager.MiniGameEnd -= OnMiniGameEnd;
    }
}
