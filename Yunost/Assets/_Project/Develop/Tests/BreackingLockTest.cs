using MiniGames;
using UnityEngine;

public class BreackingLockTest : MonoBehaviour
{
    public TypeDifficultMiniGames difficult;
    public int countItems;
    void Start()
    {
        MiniGameContext context = new MiniGameContext(TypesMiniGames.BreakingLock, difficult, 0, countItems);
        MiniGamesManager manager = transform.gameObject.GetComponent<MiniGamesManager>();
        manager.RunMiniGame(context);
    }
}
