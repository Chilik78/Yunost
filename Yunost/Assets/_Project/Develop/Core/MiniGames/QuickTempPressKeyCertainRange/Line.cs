using System;
using UnityEngine;

namespace MiniGames
{
    namespace QuickTempPressKeyCertainRange
    {
        public class Line
        {
            private GameObject _line;
            private float _speed;
            public Line(TypeDifficultMiniGames difficult)
            {
                _line = GameObject.Find("Line");
                ChooseSpeed(difficult);
            }

            private void ChooseSpeed(TypeDifficultMiniGames difficult)
            {
                switch (difficult)
                {
                    case TypeDifficultMiniGames.Easy: _speed = 1f; break;
                    case TypeDifficultMiniGames.Medium: _speed = 2f; break;
                    case TypeDifficultMiniGames.Hard: _speed = 2.5f; break;
                    default: _speed = 1f; break;
                }
            }

            public void Rotate()
            {
                _line.transform.localEulerAngles -= new Vector3(0f,0f, _speed);
            }

            public float getAngleLine
            {
                get => Math.Abs(360 - _line.transform.localEulerAngles.z);
            }
        }
    }
}
