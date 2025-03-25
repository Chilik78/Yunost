using UnityEngine;

namespace MiniGames
{
    namespace HoldingObjectInRange
    {
        public class PlayerIndicatorController : MonoBehaviour
        {
            private RectTransform _playerIndicatorRectTr;
            private float _halfWidthSlideArea;
            private float _stepMove;
            private float _halfWidthPlayerIndicator;

            public void Init(float widthSlideArea, float stepMove)
            {
                _playerIndicatorRectTr = transform.gameObject.GetComponent<RectTransform>();
                _halfWidthSlideArea = (widthSlideArea / 2f);  
                _stepMove = stepMove;
                _halfWidthPlayerIndicator = (_playerIndicatorRectTr.sizeDelta[0] / 2f);
            }

            #region Move_Indicator
            public void MoveIndicator()
            {
                float horizontalValue = Input.GetAxis("Mouse X");
                bool isCanMove = GetIsPlayerCanMove(horizontalValue);

                if (isCanMove)
                    Move(horizontalValue);
            }

            private bool GetIsPlayerCanMove(float mouseHorizontalValue)
            {
                /* bool isMouseMove = mouseHorizontalValue != 0;
                 bool isPlayerIndicatorInArea = true;

                 if (isMouseMove)
                 {
                     float newXPos = _playerIndicatorRectTr.anchoredPosition.x + Mathf.Sign(mouseHorizontalValue) * _stepMove;
                     float newXPosForCheck = newXPos + _halfWidthPlayerIndicator * Mathf.Sign(newXPos);
                     isPlayerIndicatorInArea = newXPosForCheck >= -_halfWidthSlideArea && newXPosForCheck <= _halfWidthSlideArea;
                 }

                 return isMouseMove && isPlayerIndicatorInArea;
                */

                bool isPlayerIndicatorInArea = true;
                float newXPos = _playerIndicatorRectTr.anchoredPosition.x + Mathf.Sign(mouseHorizontalValue) * _stepMove;
                float newXPosForCheck = newXPos + _halfWidthPlayerIndicator * Mathf.Sign(newXPos);
                isPlayerIndicatorInArea = newXPosForCheck >= -_halfWidthSlideArea && newXPosForCheck <= _halfWidthSlideArea;

                return isPlayerIndicatorInArea;
            }

            private void Move(float mouseHorizontalValue)
            {
                float newXPos = _playerIndicatorRectTr.anchoredPosition.x + Mathf.Sign(mouseHorizontalValue) * _stepMove;
                float randomShift;

                if(mouseHorizontalValue == 0)
                {
                    randomShift = Random.Range(-2f, -1f);
                    newXPos += _stepMove * randomShift;
                }
                else
                {
                    randomShift = Random.Range(1f, Mathf.Abs(mouseHorizontalValue));
                    newXPos += _stepMove * randomShift * Mathf.Sign(mouseHorizontalValue);
                }

                Vector2 newPos = new Vector2(newXPos, _playerIndicatorRectTr.anchoredPosition.y);
                _playerIndicatorRectTr.anchoredPosition = newPos;
            }

            #endregion
        }
    }
}