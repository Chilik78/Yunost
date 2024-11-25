using UnityEngine;

namespace MiniGames
{
    public class MiniGamesManager : MonoBehaviour
    {
        private MiniGame _currGame;

        private void Start()
        {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, 0f);
            RunMiniGame(testContext);
        }

        private void Update()
        {
            if (_currGame != null)
                _currGame.TrackingProgressGame();
        }

        public void RunMiniGame(MiniGameContext context)
        {
            ChooseDifficultMiniGame(ref(context));
            ChooseMiniGame(context);
        }

        void ChooseDifficultMiniGame(ref MiniGameContext context)
        {
            context.ÑurrentDifficult = TypeDifficultMiniGames.Easy;
        }

        void ChooseMiniGame(MiniGameContext context)
        {
            switch (context.TypeMiniGame)
            {
                case TypesMiniGames.HoldingObjectInRange: break;
                case TypesMiniGames.AdvancePathEachStage: break;
                case TypesMiniGames.QuickPressKeyCertainTime: break;
                case TypesMiniGames.GameWolfConsole: break;
                case TypesMiniGames.QuickTempPressKeyCertainRange: break;
                case TypesMiniGames.ConnectElements: break;
                case TypesMiniGames.ReachEndPointWithObstacles: break;
                case TypesMiniGames.BreakingLock: _currGame = new BreakingLockMiniGame(); break;
                default: break;
            }

            if (_currGame != null)
                _currGame.Init(context);
        }
    }
}

