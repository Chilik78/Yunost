using UnityEngine;

namespace MiniGames
{
    public class MiniGamesManager : MonoBehaviour
    {
        private MiniGame _currGame;
        private Object _screen;

        public delegate void MiniGameEndHandler(MiniGameResultInfo resultInfo);

        public event MiniGameEndHandler MiniGameEnd;

<<<<<<< HEAD
=======
        private UniversalTutorialManager universalTutorialManager;

        private void Start()
        {
            universalTutorialManager = FindObjectOfType<UniversalTutorialManager>();
        }

>>>>>>> main
        private void Update()
        {
            if (_currGame != null)
                _currGame.TrackingProgressGame();
        }

        public void RunMiniGame(MiniGameContext context)
        {
<<<<<<< HEAD
            //SystemManager.GetInstance().DisableSystemsToMiniGame();   

            //_screen = Instantiate(GetScreen(context));
=======
            SystemManager.GetInstance().DisableSystemsToMiniGame();   

            _screen = Instantiate(GetScreen(context));
>>>>>>> main
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
<<<<<<< HEAD
            if (_currGame != null)
            {
                _currGame.Init(context);
                _currGame.OnMiniGameEnd += OnMiniGameEnd;
=======
            universalTutorialManager.TriggerTutorial("MiniGame1"); //Триггер на появление окна туториала
            if (_currGame != null)
            {
                
                _currGame.Init(context);
                _currGame.OnMiniGameEnd += OnMiniGameEnd;
               
>>>>>>> main
            }
        }

        private void OnMiniGameEnd(MiniGameResultInfo resultInfo)
        {
            _currGame.OnMiniGameEnd -= OnMiniGameEnd;
<<<<<<< HEAD
            //SystemManager.GetInstance().EnableSystemsToMiniGame();
=======
            SystemManager.GetInstance().EnableSystemsToMiniGame();
>>>>>>> main

            MiniGameEnd?.Invoke(resultInfo);

            if (resultInfo.getResultMiniGame == TypeResultMiniGames.Failed)
            {
                Debug.LogError($"Проигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }
            else
            {
                Debug.Log($"Выигрыш | Кол-во предметов, которые нужно забрать: {resultInfo.getNumLostItems}");
            }

            Destroy(_screen);
        }
    }
}

