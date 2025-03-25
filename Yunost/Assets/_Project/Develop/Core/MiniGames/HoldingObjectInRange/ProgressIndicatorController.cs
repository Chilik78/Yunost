using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames
{
    namespace HoldingObjectInRange
    {
        public enum DirectionProgressIndicator 
        { 
            ToLeft, // Движение от центра влево Slide Area
            ToStartPosition, // Движение к центру Slide Area
            ToRight, // Движение от центра вправо Slide Area
            None, // Движение никуда, в случае, когда индикатор не двигается
        }

        public class ProgressIndicatorController : MonoBehaviour
        {
            private RectTransform _playerIndicatorRectTr;
            private RectTransform _progressIndicatorRect;
            private float _widthSlideArea;
            private float _stepProgressIndicator;
            private float _stepVich;
            private int _countIgnoreFrames;
            private float _startWidth;
            private float _expectedWidth;

            private int _countFrames = 0;
            private bool _playerInRange = false;
            private float _lastDistance;
            private Vector2 _middleSlideAreaPos;
            public DirectionProgressIndicator lastDirection;

            public delegate void GameEndHandler(TypeResultMiniGames result);
            public event GameEndHandler OnGameEnd;

            public float getProgressValue { get => _progressIndicatorRect.anchoredPosition.x; }
            
            public DirectionProgressIndicator getDirectionIndicator { 
                get {
                    float distance = Vector2.Distance(_middleSlideAreaPos, _progressIndicatorRect.anchoredPosition);

                    if(distance < _lastDistance)
                    {
                        lastDirection = DirectionProgressIndicator.ToStartPosition;
                        return DirectionProgressIndicator.ToStartPosition;
                    }

                    if(distance != _lastDistance)
                    {
                        lastDirection = _progressIndicatorRect.anchoredPosition.x > 0 ? DirectionProgressIndicator.ToRight
                        : DirectionProgressIndicator.ToLeft;

                        return _progressIndicatorRect.anchoredPosition.x > 0 ? DirectionProgressIndicator.ToRight
                        : DirectionProgressIndicator.ToLeft;
                    }

                    return DirectionProgressIndicator.None;
                }
            }

            public void Init(float widthSlideArea, float stepMove, float stepTakingAwayWidth, 
                int countIgnoreFrames, float startWidth, float expectedWidth)
            {
                _playerIndicatorRectTr = GameObject.Find("Player Indicator").GetComponent<RectTransform>();
                _progressIndicatorRect = transform.gameObject.GetComponent<RectTransform>();
                _widthSlideArea = widthSlideArea;
                _stepProgressIndicator = stepMove;
                _stepVich = stepTakingAwayWidth;
                _countIgnoreFrames = countIgnoreFrames;
                _startWidth = startWidth;
                _expectedWidth = expectedWidth;

                _progressIndicatorRect.sizeDelta = new Vector2(_startWidth, _progressIndicatorRect.sizeDelta[1]);
                _lastDistance = 0;
                _middleSlideAreaPos = new Vector2(0, _progressIndicatorRect.anchoredPosition.y);
            }

            #region Move_Indicator
            public void MoveIndicator()
            {
                bool isProgressIndicatorInArea = GetProgressIndicatorInArea();

                if (isProgressIndicatorInArea)
                {
                    Vector2 range = GetRange();
                    bool isPlayerInRangeNow = GetPlayerInRange(range);

                    if (isPlayerInRangeNow)
                    {
                        if(_countFrames >= _countIgnoreFrames)
                        {
                            _playerInRange = true;
                        }

                        /*if (_countFrames >= _countIgnoreFrames)
                        {
                            _countFrames = 0;
                            Move(true);
                            ChangeWidth();
                            return;
                        }*/
                        _countFrames++;
                    }
                    else
                    {
                        _playerInRange = false;
                    }

                    if(_playerInRange)
                    {
                        _countFrames = 0;
                        Move(true);
                        ChangeWidth();
                        return;
                    }
                    
                    Move(false);
                    ChangeWidth();
                }                
            }

            private bool GetProgressIndicatorInArea()
            {
                float posXProgress = _progressIndicatorRect.anchoredPosition.x;
                float halfWidthProgressIndicator = (_progressIndicatorRect.sizeDelta[0] / 2f);
                float posBegPoint = posXProgress - halfWidthProgressIndicator;
                float posEndPoint = posXProgress + halfWidthProgressIndicator;
                float halfWidthSlideArea = _widthSlideArea / 2f;

                if(posBegPoint <= -halfWidthSlideArea)
                {
                    OnGameEnd?.Invoke(TypeResultMiniGames.Failed);
                }
                else if(posEndPoint >= halfWidthSlideArea)
                {
                    OnGameEnd?.Invoke(TypeResultMiniGames.Сompleted);
                }

                return posBegPoint > -halfWidthSlideArea && posEndPoint < halfWidthSlideArea;
            }

            private Vector2 GetRange()
            { 
                float posX = _progressIndicatorRect.anchoredPosition.x;
                float width = _progressIndicatorRect.sizeDelta[0];
                float beginPos = posX - (width / 2);
                float endPos = posX + (width / 2);

                return new Vector2(beginPos, endPos);
            }

            private bool GetPlayerInRange(Vector2 range)
            {
                float posXPlayer = _playerIndicatorRectTr.anchoredPosition.x;
                float halfWidthPlayerIndicator = (_playerIndicatorRectTr.sizeDelta[0] / 2f);
                float posEndPoint = posXPlayer + halfWidthPlayerIndicator;
                float posBegPoint = posXPlayer - halfWidthPlayerIndicator; 
                bool isEndPointInRange = posEndPoint >= range.x && posEndPoint <= range.y;
                bool isBegPointInRange = posBegPoint >= range.x && posBegPoint <= range.y;

                return isEndPointInRange && isBegPointInRange;
            }

            private void Move(bool isForward)
            {
                float direction = isForward ? 1 : -1; 
                float newXPos = _progressIndicatorRect.anchoredPosition.x + _stepProgressIndicator * direction;
                Vector2 newPos = new Vector2(newXPos, _progressIndicatorRect.anchoredPosition.y);
                _progressIndicatorRect.anchoredPosition = newPos;
            }

            private void ChangeWidth()
            {
                //float newDistance = Vector2.Distance(_middleSlideAreaPos, _progressIndicatorRect.anchoredPosition);
                float widthProgressIndicator = _progressIndicatorRect.sizeDelta[0];

                /*if(newDistance > _lastDistance && widthProgressIndicator > _expectedWidth)
                {
                    _progressIndicatorRect.sizeDelta = new Vector2(widthProgressIndicator - _stepVich, _progressIndicatorRect.sizeDelta[1]);
                }
                else if(_lastDistance > newDistance && widthProgressIndicator < _startWidth)
                {
                    _progressIndicatorRect.sizeDelta = new Vector2(widthProgressIndicator + _stepVich, _progressIndicatorRect.sizeDelta[1]);
                }

                _lastDistance = newDistance;*/


                DirectionProgressIndicator direction = getDirectionIndicator;
                
                if (direction != DirectionProgressIndicator.ToStartPosition && direction != DirectionProgressIndicator.None 
                    && widthProgressIndicator > _expectedWidth)
                {
                    _progressIndicatorRect.sizeDelta = new Vector2(widthProgressIndicator - _stepVich, _progressIndicatorRect.sizeDelta[1]);
                }
                else if (direction == DirectionProgressIndicator.ToStartPosition && widthProgressIndicator < _startWidth)
                {
                    _progressIndicatorRect.sizeDelta = new Vector2(widthProgressIndicator + _stepVich, _progressIndicatorRect.sizeDelta[1]);
                }

                _lastDistance = Vector2.Distance(_middleSlideAreaPos, _progressIndicatorRect.anchoredPosition);
            }

            #endregion
        }
    }
}