using UnityEngine;
using MiniGames.ReachEndPointWithObstacles;

namespace MiniGames
{
    /// <summary>
    /// Мини-игра сделана с процедурной генерацией лабиринта алгоритмом Recusive Backtracker
    /// </summary>
    public class ReachEndPointWithObstaclesMiniGame : MiniGame
    {
        private Vector3 _cellSizes;
        private Vector2 _gridSizes; // Менять по сложности
        private Color _dangerousColor = Color.yellow;
        private int _countDangerousCells; // Менять по сложности

        private ObstacleGenerator _obstacleGenerator;
        private BorderGenerator _borderGenerator;
        private PlayerController _playerController;
        private EndPointController _endpointController;
        private TimeController _timeController;
        private CheckBoxController _checkBoxController;

        private const float _speedPlayer = 30f;
        private int _distanceToSpawnEnpoint; // Менять по сложности

        private float _timeInSeconds; // Менять по сложности
        private Color[] _stateTimeColors = { new Color(51, 195, 35), Color.yellow, Color.red };
        private const float _taikingAwayTimeValueInSeconds = 0.01f;

        private int _countEndpoints; // Менять по сложности
        private int _currCountPickupEndpoint = 0;

        private const float _freezeTimeInSeconds = 1.00f;
        private KeyCode[] _keysForEndpoint = { KeyCode.Q, KeyCode.R, KeyCode.T, KeyCode.Z, KeyCode.X };

        #region Init
        public override void Init(MiniGameContext context)
        {
            _cellSizes = new Vector3(300f, 300f, 100f);
            InitVariablesByDifficult(context.getCurrentDifficult);
            FindControllers();
            InitControllers();
            BuildUI();
        }

        private void InitVariablesByDifficult(TypeDifficultMiniGames difficult)
        {
            switch (difficult)
            {
                case TypeDifficultMiniGames.Easy:
                    _gridSizes = new Vector2(4, 4);
                    _countDangerousCells = 3;
                    _distanceToSpawnEnpoint = 2;
                    _timeInSeconds = 25f;
                    _countEndpoints = 1;
                    break;
                case TypeDifficultMiniGames.Medium:
                    _gridSizes = new Vector2(4, 5);
                    _countDangerousCells = 4;
                    _distanceToSpawnEnpoint = 2;
                    _timeInSeconds = 45f;
                    _countEndpoints = 2;
                    break;
                case TypeDifficultMiniGames.Hard:
                    _gridSizes = new Vector2(4, 6);
                    _countDangerousCells = 6;
                    _distanceToSpawnEnpoint = 2;
                    _timeInSeconds = 65f;
                    _countEndpoints = 3;
                    break;
            }
        }

        private void FindControllers()
        {
            GameObject mapMiniGame = GameObject.Find("Map MiniGame");
            _obstacleGenerator = mapMiniGame.GetComponent<ObstacleGenerator>();
            _borderGenerator = mapMiniGame.GetComponent<BorderGenerator>();
            _playerController = GameObject.Find("Player MiniGame").GetComponent<PlayerController>();
            _endpointController = GameObject.Find("EndPoint MiniGame").GetComponent<EndPointController>();
            _timeController = GameObject.Find("Time Indicator MiniGame").GetComponent<TimeController>();
            _checkBoxController = GameObject.Find("CheckBoxes MiniGame").GetComponent<CheckBoxController>();
        }

        private void InitControllers()
        {
            _playerController.Init(_speedPlayer, _freezeTimeInSeconds, _keysForEndpoint);
            _playerController.OnPressKey += OnPressKey;
            _endpointController.Init(_distanceToSpawnEnpoint);
            _timeController.Init(_stateTimeColors, _timeInSeconds, _taikingAwayTimeValueInSeconds);
            _timeController.OnTimeEnd += OnTimeEnd;
            _checkBoxController.Init(_countEndpoints);
        }

        private void OnPressKey()
        {
            _currCountPickupEndpoint++;
            _checkBoxController.TurnOnCheckBox(_currCountPickupEndpoint);

            if (_currCountPickupEndpoint == _countEndpoints)
            {
                FinishGame(new MiniGameResultInfo(TypeResultMiniGames.Сompleted, 0));
                return;
            }

            _endpointController.SpawnEnpoint(_endpointController.GetCurrentCoord, _obstacleGenerator.GetObstacleCells);
        }

        private void OnTimeEnd()
        {
            // TODO: Потом Расскоментить
            //FinishGame(new MiniGameResultInfo(TypeResultMiniGames.Failed, 0));
        }

        protected override void BuildUI()
        {
            _obstacleGenerator.GenerateObstacle(_gridSizes, _cellSizes, _countDangerousCells, _dangerousColor, _countEndpoints);
            _borderGenerator.GenerateBorder();
            _playerController.SpawnPlayer(_obstacleGenerator.GetObstacleCells);
            _endpointController.SpawnEnpoint(_playerController.GetSpawnCoord, _obstacleGenerator.GetObstacleCells);
        }
        #endregion

        public override void TrackingProgressGameOnUpdate()
        {
            _playerController.Move();
            _playerController.CheckPressKey();
        }

        private void FinishGame(MiniGameResultInfo result)
        {
            _playerController.OnPressKey -= OnPressKey; 
            _timeController.OnTimeEnd -= OnTimeEnd; 
            CalculateResult(result);
        }
    }
}