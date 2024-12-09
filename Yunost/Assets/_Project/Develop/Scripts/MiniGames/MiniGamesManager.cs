using UnityEngine;

namespace MiniGames
{
    public class MiniGamesManager : MonoBehaviour
    {
        private MiniGame _currGame;

        public delegate void MiniGameEndHandler(MiniGameResultInfo resultInfo);

        public event MiniGameEndHandler MiniGameEnd;    

        private void Start()
        {
            MiniGameContext testContext = new MiniGameContext(TypesMiniGames.BreakingLock, 0f, 5);
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
            Run(context);
        }

        private void ChooseDifficultMiniGame(ref MiniGameContext context)
        {
            context.СurrentDifficult = TypeDifficultMiniGames.Easy;
        }

        private void ChooseMiniGame(MiniGameContext context)
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
        }

        private void Run(MiniGameContext context)
        {
            if (_currGame != null)
            {
                _currGame.Init(context);
                _currGame.OnMiniGameEnd += OnMiniGameEnd;
            }
        }

        private void OnMiniGameEnd(MiniGameResultInfo resultInfo)
        {
            _currGame.OnMiniGameEnd -= OnMiniGameEnd;

            if (resultInfo.getResultMiniGame == TypeResultMiniGames.Failed)
            {
                MiniGameEnd?.Invoke(resultInfo);    
                Debug.LogError($"Проигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
            else
            {
                MiniGameEnd?.Invoke(resultInfo);
                Debug.Log($"Выигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
        }
    }
}

