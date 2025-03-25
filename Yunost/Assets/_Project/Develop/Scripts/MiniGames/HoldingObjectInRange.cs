using UnityEngine;
using MiniGames.HoldingObjectInRange;

namespace MiniGames
{
    public class HoldingObjectInRangeMiniGame : MiniGame
    {
        private FishingRod _fishingRod;
        private bool _isAnimate = false;
        private bool _isIntro = true;
        private bool _isGameEnd = false;
        private TypeResultMiniGames _resultGame;

        private const int _countSteps = 500;

        private ProgressIndicatorController _progressIndicatorController;
        private float _startWidthProgressIndicator;
        private float _expectedWidthProgressIndicator;
        private int _countIgnoreFrames;

        private PlayerIndicatorController _playerIndicatorController;

        #region Init
        public override void Init(MiniGameContext context)
        {
            _fishingRod = new FishingRod();
            _fishingRod.OnAnimationChange += OnAnimationChange;
            _isAnimate = true;
            InitVariablesByDifficult(context.getCurrentDifficult);
            BuildUI();
            _progressIndicatorController.OnGameEnd += OnGameEnd;
        }

        private void OnAnimationChange(bool isAnimate)
        {
            if (_isIntro)
            {
                _isIntro = false;
            }
            _isAnimate = isAnimate;
        } 

        private void InitVariablesByDifficult(TypeDifficultMiniGames difficult)
        {
            switch(difficult)
            {
                case TypeDifficultMiniGames.Easy:  
                    _startWidthProgressIndicator = 400f; 
                    _expectedWidthProgressIndicator = 145f; 
                    _countIgnoreFrames = 20; 
                    break;
                case TypeDifficultMiniGames.Medium:
                    _startWidthProgressIndicator = 350f;
                    _expectedWidthProgressIndicator = 155f;
                    _countIgnoreFrames = 20;
                    break;
                case TypeDifficultMiniGames.Hard:
                    _startWidthProgressIndicator = 300f;
                    _expectedWidthProgressIndicator = 120f;
                    _countIgnoreFrames = 5;
                    break;    
            }
        }

        protected override void BuildUI()
        {
            GameObject slideArea = GameObject.Find("Slide Area");
            float widthSlideArea = slideArea.GetComponent<RectTransform>().sizeDelta[0];

            BuildPlayerIndicator(widthSlideArea);
            BuildProgressIndicator(widthSlideArea);
        }

        private void BuildPlayerIndicator(float widthSlideArea)
        {
            GameObject playerIndicator = GameObject.Find("Player Indicator");
            float width = playerIndicator.GetComponent<RectTransform>().sizeDelta[0];
            float stepMove = (widthSlideArea - width) / (float) (_countSteps - 100);

            _playerIndicatorController = playerIndicator.GetComponent<PlayerIndicatorController>();
            _playerIndicatorController.Init(widthSlideArea, stepMove);
        }

        private void BuildProgressIndicator(float widthSlideArea)
        {
            float stepMove = (widthSlideArea - _startWidthProgressIndicator) / (float)_countSteps;
            float stepTakingAwayWidth = (_startWidthProgressIndicator - _expectedWidthProgressIndicator) / 
                ((widthSlideArea / 2) / stepMove);

            GameObject progressIndicator = GameObject.Find("Progress Indicator");
            _progressIndicatorController = progressIndicator.GetComponent<ProgressIndicatorController>();
            _progressIndicatorController.Init(widthSlideArea, stepMove, stepTakingAwayWidth, _countIgnoreFrames,
                _startWidthProgressIndicator, _expectedWidthProgressIndicator);
        }

        #endregion

        public override void TrackingProgressGameOnUpdate()
        {
            if (_isAnimate && _isIntro)
            {
                _fishingRod.DoFish();
            }

            if(!_isAnimate && !_isIntro)
            {
                _playerIndicatorController.MoveIndicator();
                _progressIndicatorController.MoveIndicator();
                _fishingRod.Rotate(_progressIndicatorController.lastDirection, _countSteps, 0);
                _fishingRod.MoveHook(_progressIndicatorController.lastDirection, _countSteps, 0);
            }

            if(!_isAnimate && _isGameEnd)
            {
                FinishGame();
            }
            else if(_isAnimate && _isGameEnd)
            {
                _fishingRod.GetFishOut();
            }
        }

        private void OnGameEnd(TypeResultMiniGames result)
        {
            _resultGame = result;
            _isGameEnd = true;

            if (result == TypeResultMiniGames.Ñompleted)
            {
                OnAnimationChange(true);
                _fishingRod.GetFishOut();
            }
        }

        private void FinishGame()
        {
            _progressIndicatorController.OnGameEnd -= OnGameEnd;
            CalculateResult(new MiniGameResultInfo(_resultGame, 0));
        }
    }
}