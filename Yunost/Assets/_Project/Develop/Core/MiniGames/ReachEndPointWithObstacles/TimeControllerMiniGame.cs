using System.Collections;
using TMPro;
using UnityEngine;

namespace MiniGames
{
    namespace ReachEndPointWithObstacles
    {
        public class TimeControllerMiniGame : MonoBehaviour
        {
            private Color[] _stateColors;
            private float _takingAwayVal;
            private TMP_Text _textTime;
            private float _startTime;
            private float _currentTime;
            private bool _isPlaying = false;

            public delegate void OnTimeEndHandler();
            public event OnTimeEndHandler OnTimeEnd;    

            public void Init(Color[] stateColors, float timeInSeconds, float takingAwayValInSeconds)
            {
                _textTime = GameObject.Find("Time Indicator MiniGame").GetComponent<TMP_Text>();  
                _textTime.color = stateColors[0];
                _stateColors = stateColors;
                _startTime = timeInSeconds;
                _currentTime = timeInSeconds;
                _takingAwayVal = takingAwayValInSeconds;
                _isPlaying = true;
            }

            private void Update()
            {
                if(_isPlaying)
                    StartCoroutine(ChangeTime());
            }

            IEnumerator ChangeTime()
            {
                yield return new WaitForSecondsRealtime(_takingAwayVal);
                _currentTime -= _takingAwayVal;

                if (_currentTime < 0)
                {
                    _textTime.text = $"0.00 ñ";
                    _isPlaying = false;
                    OnTimeEnd?.Invoke();
                }
                else
                {
                    string timeInString = _currentTime.ToString("0.00").Replace(",", ".");
                    _textTime.text = $"{timeInString} ñ";
                }

                ChangeColor();
            }

            private void ChangeColor()
            {
                if (_currentTime <= 2)
                {
                    _textTime.color = _stateColors[2];
                }
                else if (_currentTime <= _startTime / 2)
                {
                    _textTime.color = _stateColors[1];
                }
            }
        }
    }
}