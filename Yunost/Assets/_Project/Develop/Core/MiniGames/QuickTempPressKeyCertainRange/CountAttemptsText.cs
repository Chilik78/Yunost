using TMPro;
using UnityEngine;

namespace MiniGames 
{ 
    namespace QuickTempPressKeyCertainRange
    {
        public class CountAttemptsText : MonoBehaviour 
        {
            private TMP_Text _text;
            private float _startFontSize;
            private Color _startTextColor;
            private bool _isAnimate = false;
            private int _countAttempts; // Кол-во оставшийхся ошибок
            private Color _animTextColor = Color.red;
            private const float _animFontSize = 48f;
            private const float _animSmoothTime = 0.2f;

            private void Start()
            {
                _text = GameObject.Find("CountAttempts").GetComponent<TMP_Text>();
                _countAttempts = 3; 
                _text.text = $"Осталось попыток: {_countAttempts}";
                _startTextColor = _text.color;
                _startFontSize = _text.fontSize;
            }

            private void FixedUpdate()
            {
                if (_isAnimate) 
                {
                    Animate();
                }
            }

            private void Animate()
            {
                float step = (_animFontSize / _startFontSize) * _animSmoothTime;


                if(_text.fontSize <= _animFontSize)
                {
                    _text.fontSize += step;
                }
                else
                {
                    _isAnimate = false;
                    _text.fontSize = _startFontSize;
                    _text.color = _startTextColor;
                }
                
            }

            public void TakeAwayAttempt()
            {
                _countAttempts--;
                _text.text = $"Осталось попыток: {_countAttempts}";
                _isAnimate = true;
                _text.color = _animTextColor;   
            }
        }
    }
}