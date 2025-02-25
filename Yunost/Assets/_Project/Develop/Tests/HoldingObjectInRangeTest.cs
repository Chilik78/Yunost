using UnityEngine;
using MiniGames;

public class HoldingObjectInRangeMiniGameTest : MonoBehaviour
{
    public TypeDifficultMiniGames difficult;

    void Start()
    {
        MiniGameContext context = new MiniGameContext(TypesMiniGames.HoldingObjectInRange, difficult, 0);
        MiniGamesManager manager = transform.gameObject.GetComponent<MiniGamesManager>();
        manager.RunMiniGame(context);
    }
}
