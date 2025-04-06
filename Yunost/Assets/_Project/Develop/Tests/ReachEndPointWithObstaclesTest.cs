using MiniGames;
using UnityEngine;

public class ReachEndPointWithObstaclesTest : MonoBehaviour
{
    public TypeDifficultMiniGames difficult;

    void Start()
    {
        MiniGameContext context = new MiniGameContext(TypesMiniGames.ReachEndPointWithObstacles, difficult, 0);
        MiniGamesManager manager = transform.gameObject.GetComponent<MiniGamesManager>();
        manager.RunMiniGame(context);
    }
}
