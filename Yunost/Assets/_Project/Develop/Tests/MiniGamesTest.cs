using MiniGames;
using UnityEngine;

namespace MiniGames
{
    namespace Tests
    {
        public class MiniGamesTest : MonoBehaviour
        {
            public TypesMiniGames typeMiniGame;
            public TypeDifficultMiniGames difficult;
            public int countItems = 0;

            void Start()
            {
                MiniGameContext context = new MiniGameContext(typeMiniGame, difficult, countItems);
                MiniGamesManager manager = transform.gameObject.GetComponent<MiniGamesManager>();
                manager.RunMiniGame(context);
            }
        }
    }
}