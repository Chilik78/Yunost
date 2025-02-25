using MiniGames.QuickTempPressKeyCertainRange;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    public class QuickTempPressKeyCertainRangeMiniGame : MiniGame
    {
        private Line _line;
        private Shovel _shovel;
        private CountAttemptsText _textCountAttempts;
        private Vector2 _unlockAngleRange;
        private float _lockRange;
        private int _countAttempts = 3; // Кол-во оставшийхся ошибок
        private int _numCurrStage = 0; // Номер текущего этапа
        private List<GameObject> _stages = new List<GameObject>(new GameObject[4]);
        private bool _isAnimate = false;

        public override void Init(MiniGameContext context)
        {
            _line = new Line(context.getCurrentDifficult);
            _shovel = new Shovel(500);
            _shovel.OnAnimationChange += OnAnimationChange;
            _shovel.OnChangeStageVisual += OnChangeStageVisual;
            _textCountAttempts = GameObject.Find("CountAttempts").GetComponent<CountAttemptsText>();
            InitStages();
            ChooseLockRangeByDifficult(context.getCurrentDifficult);
            GenerateUnlockAngleRange();
            BuildUI();
        }

        private void OnAnimationChange(bool isAnimate)
        {
            _isAnimate = isAnimate;
        }

        private void OnChangeStageVisual()
        {
            _stages[_numCurrStage - 1].SetActive(false);
            _stages[_numCurrStage].SetActive(true);
        }

        #region Init
        private void InitStages()
        {
            _stages[0] = GameObject.Find("Begin_stage");
            _stages[1] = GameObject.Find("First_stage");
            _stages[2] = GameObject.Find("Second_stage");
            _stages[3] = GameObject.Find("Third_stage");

            DisableStages();
        }

        private void DisableStages()
        {
            foreach (var stage in _stages)
            {
                if (stage.name == "Begin_stage")
                    continue;

                stage.SetActive(false);
            }
        }

        private void ChooseLockRangeByDifficult(TypeDifficultMiniGames difficult)
        {
            switch (difficult)
            {
                case TypeDifficultMiniGames.Easy: _lockRange = 40; return;
                case TypeDifficultMiniGames.Medium: _lockRange = 25; return;
                case TypeDifficultMiniGames.Hard: _lockRange = 10; return;
                default: _lockRange = 40; return;
            }
        }

        void GenerateUnlockAngleRange()
        {
            float unlockAngle = UnityEngine.Random.Range(0 + _lockRange, 360 - _lockRange);
            _unlockAngleRange = new Vector2(unlockAngle - _lockRange, unlockAngle + _lockRange);
        }

        protected override void BuildUI()
        {
            DrawIndicator();
        }

        private void DrawIndicator()
        {
            DrawCircle();
            DrawRangeArc();
            DrawLineIndicator();
        }

        private void DrawCircle()
        {
            CircleDrawer circleDrawer = GameObject.Find("Circle").GetComponent<CircleDrawer>();
            circleDrawer.DrawCirle();
        }

        private void DrawRangeArc()
        {
            ArcDrawer arcDrawer = GameObject.Find("Arc").GetComponent<ArcDrawer>();
            arcDrawer.DrawArc(_unlockAngleRange);
        }

        private void DrawLineIndicator()
        {
            LineDrawer lineDrawer = GameObject.Find("Line").GetComponent<LineDrawer>();
            lineDrawer.DrawLine();
        }
        #endregion

        #region TrackingProgressGame
        public override void TrackingProgressGameOnUpdate()
        {
            if(!_isAnimate)
            {
                CheckEndGame();
                _line.Rotate();
                ChangeStage();
            }
            else
            {
                _shovel.Dig(_numCurrStage);  
            }
        }

        private void ChangeStage()
        {
            bool isPressed = Input.GetKeyDown(KeyCode.E);
            bool lineInRange = CheckLineInRange();

            if (isPressed && lineInRange) 
            {
                GenerateUnlockAngleRange(); 
                DrawRangeArc();
                _numCurrStage++;
                OnAnimationChange(true);
            }
            else if(isPressed)
            {
                _countAttempts--;
                _textCountAttempts.TakeAwayAttempt();   
            }
        }

        private bool CheckLineInRange()
        {
            float currAngle = _line.getAngleLine;
            bool lineInRange = currAngle >= _unlockAngleRange[0] && currAngle <= _unlockAngleRange[1];
            return lineInRange;
        }

        private void CheckEndGame()
        {
            if (_numCurrStage == 3 && _countAttempts > 0)
            {
                CalculateResult(new MiniGameResultInfo(TypeResultMiniGames.Сompleted, 0));
            }
            else if (_countAttempts == 0)
            {
                CalculateResult(new MiniGameResultInfo(TypeResultMiniGames.Failed, 0));
            }
        }
    }
    #endregion
}