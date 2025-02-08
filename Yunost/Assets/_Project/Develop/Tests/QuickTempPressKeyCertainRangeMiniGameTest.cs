using UnityEngine;
using MiniGames;

public class QuickTempPressKeyCertainRangeMiniGameTest : MonoBehaviour
{

    public TypeDifficultMiniGames difficult;

    void Start()
    {
        MiniGameContext context = new MiniGameContext(TypesMiniGames.QuickTempPressKeyCertainRange, difficult, 0, 0);
        MiniGamesManager manager = transform.gameObject.GetComponent<MiniGamesManager>();
        manager.RunMiniGame(context);   
    }
}
