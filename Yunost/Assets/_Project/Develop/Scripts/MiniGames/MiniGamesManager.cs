using UnityEngine;

namespace MiniGames
{
    public class MiniGamesManager : MonoBehaviour
    {
        private MiniGame _currGame;

        private GameObject _mainCamera;
        private GameObject _canvases;
        private Object _screen;

        private void Update()
        {
            if (_currGame != null)
                _currGame.TrackingProgressGame();
        }

        public void RunMiniGame(MiniGameContext context)
        {
            _mainCamera = GameObject.Find("Canvases");
            _canvases = GameObject.Find("Main Camera");

            _mainCamera.SetActive(false);
            _canvases.SetActive(false);
            _screen = Instantiate(GetScreen(context));
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

        private Object GetScreen(MiniGameContext context)
        {
            string path = "MiniGameScreens/";
            switch (context.TypeMiniGame)
            {
                case TypesMiniGames.HoldingObjectInRange: break;
                case TypesMiniGames.AdvancePathEachStage: break;
                case TypesMiniGames.QuickPressKeyCertainTime: break;
                case TypesMiniGames.GameWolfConsole: break;
                case TypesMiniGames.QuickTempPressKeyCertainRange: break;
                case TypesMiniGames.ConnectElements: break;
                case TypesMiniGames.ReachEndPointWithObstacles: break;
                case TypesMiniGames.BreakingLock: path += "BreakingLock"; break;
                default: break;
            }
            return Resources.Load(path);
        }

        private void Run(MiniGameContext context)
        {
            if (_currGame != null)
            {
                _currGame.Init(context);
                _currGame.OnMiniGameEnd += OnMiniGameEnd;
            }
        }

        private void OnMiniGameEnd(MiniGamesResultInfo resultInfo)
        {
            _mainCamera.SetActive(true);
            _canvases.SetActive(true);
            if (resultInfo.getResultMiniGame == TypeResultMiniGames.Failed)
            {
                Debug.LogError($"Проигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
            else
            {
                Debug.Log($"Выигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
            Destroy(_screen);
            _currGame.OnMiniGameEnd -= OnMiniGameEnd;
        }
    }
}

