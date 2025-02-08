using UnityEngine;

namespace MiniGames
{
    public class MiniGamesManager : MonoBehaviour
    {
        private MiniGame _currGame;
        private Object _screen;

        public delegate void MiniGameEndHandler(MiniGameResultInfo resultInfo);

        public event MiniGameEndHandler MiniGameEnd;

        private UniversalTutorialManager universalTutorialManager;

        private void Start()
        {
            universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
        }

        private void Update()
        {
            if (_currGame != null)
                _currGame.TrackingProgressGame();
        }

        public void RunMiniGame(MiniGameContext context)
        {
            //SystemManager.GetInstance().SetSystemsToMiniGame(false);   

            //_screen = Instantiate(GetScreen(context));
            ChooseMiniGame(context);
            Run(context);
        }

        private void ChooseMiniGame(MiniGameContext context)
        {
            switch (context.TypeMiniGame)
            {
                case TypesMiniGames.HoldingObjectInRange: break;
                case TypesMiniGames.AdvancePathEachStage: break;
                case TypesMiniGames.QuickPressKeyCertainTime: break;
                case TypesMiniGames.GameWolfConsole: break;
                case TypesMiniGames.QuickTempPressKeyCertainRange: _currGame = new QuickTempPressKeyCertainRangeMiniGame(); break;
                case TypesMiniGames.ConnectElements: break;
                case TypesMiniGames.ReachEndPointWithObstacles: break;
                case TypesMiniGames.BreakingLock: _currGame = new BreakingLockMiniGame(); break;
                default: break;
            }
        }

        private Object GetScreen(MiniGameContext context)
        {
            string path = "MiniGameScreens/";
            Debug.Log(context.TypeMiniGame);
            switch (context.TypeMiniGame)
            {
                case TypesMiniGames.HoldingObjectInRange: break;
                case TypesMiniGames.AdvancePathEachStage: break;
                case TypesMiniGames.QuickPressKeyCertainTime: break;
                case TypesMiniGames.GameWolfConsole: break;
                case TypesMiniGames.QuickTempPressKeyCertainRange: path += "QuickTempPressKeyCertainRange"; break;
                case TypesMiniGames.ConnectElements: break;
                case TypesMiniGames.ReachEndPointWithObstacles: break;
                case TypesMiniGames.BreakingLock: path += "BreakingLock"; break;
                default: break;
            }
            return Resources.Load(path);
        }

        private void Run(MiniGameContext context)
        {
            //ShowTutorial(context.TypeMiniGame);
            
            if (_currGame != null)
            {
                
                _currGame.Init(context);
                _currGame.OnMiniGameEnd += OnMiniGameEnd;
            }
        }

        private void ShowTutorial(TypesMiniGames type)
        {
            switch (type) {
                case TypesMiniGames.HoldingObjectInRange: break;
                case TypesMiniGames.AdvancePathEachStage: break;
                case TypesMiniGames.QuickPressKeyCertainTime: break;
                case TypesMiniGames.GameWolfConsole: break;
                case TypesMiniGames.QuickTempPressKeyCertainRange: universalTutorialManager.TriggerTutorial("MiniGameDigging"); break;
                case TypesMiniGames.ConnectElements: break;
                case TypesMiniGames.ReachEndPointWithObstacles: break;
                case TypesMiniGames.BreakingLock: universalTutorialManager.TriggerTutorial("MiniGameBreakingLock"); break;
                default: break;
            }
        }

        private void OnMiniGameEnd(MiniGameResultInfo resultInfo)
        {
            _currGame.OnMiniGameEnd -= OnMiniGameEnd;
            //SystemManager.GetInstance().SetSystemsToMiniGame(true);

            MiniGameEnd?.Invoke(resultInfo);

            if (resultInfo.getResultMiniGame == TypeResultMiniGames.Failed)
            {
                Debug.LogError($"Проигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
            else
            {
                Debug.Log($"Выигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }

            //Destroy(_screen);
            _currGame = null;
        }
    }
}

