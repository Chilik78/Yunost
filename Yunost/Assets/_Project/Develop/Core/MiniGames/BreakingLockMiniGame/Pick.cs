using System;
using System.Collections;
using UnityEngine;

namespace MiniGames
{
    namespace BreakingLock
    {
        public class Pick
        {
            private float _maxRotationAngle; // Как далеко может поворачиваться внутреняя часть замка (влево и вправо)
            private float _forConvertToAngle; // Для конвертации в нормальный угол поворота
            private float _currPosPick = 0f; // Текущий угол отмычки
            private GameObject _pick; // GameObject отмычки
            private int _currentDurability;
            //private float _swapSpeed = 2f * 0.005f;
            private float _swapSpeed = 2f;
            private bool _isSwapped = true;
            private bool _isForwardSwap = true; 
            private Vector3 _posFromSwap;
            private Vector3 _posToSwap;

            public float getCurrPosPick { get => _currPosPick; }
            public bool getSwap { get => _isSwapped; }

            public delegate void BrokenPickHandler();

            public event BrokenPickHandler OnPickBroken;

            public Pick(float maxRotationAngle)
            {
                _maxRotationAngle = maxRotationAngle;
                _forConvertToAngle = 1 / _maxRotationAngle;
                _pick = GameObject.Find("LockPick");
                _currentDurability = 100;
            }

            public void Rotate()
            {
                float moveHorizontalMouse = Input.GetAxis("Mouse X");
                float newPos = _currPosPick + moveHorizontalMouse * (_maxRotationAngle * _forConvertToAngle);

                if (Mathf.Abs(newPos) < _maxRotationAngle && newPos != _currPosPick)
                {
                    _currPosPick = newPos;
                    _pick.transform.localEulerAngles = new Vector3(0, _currPosPick, 0);
                }
            }

            public void Break()
            {
                _currentDurability -= 25;

                if (_currentDurability <= 0) 
                {
                    _currentDurability = 100;
                    _posFromSwap = _pick.transform.position;
                    _posToSwap = new Vector3(_pick.transform.position.x, _pick.transform.position.y + 10, _pick.transform.position.z - 5);
                    _isSwapped = false;
                    OnPickBroken(); 
                }
            }

            public void SwapPick()
            {
                if(_isForwardSwap)
                {
                    _pick.transform.position = Vector3.Lerp(_pick.transform.position, _posToSwap, _swapSpeed * Time.deltaTime);
                }
                else
                {
                    _pick.transform.position = Vector3.Lerp(_pick.transform.position, _posFromSwap, _swapSpeed);
                }


                if (Math.Truncate(_pick.transform.position.z * 10) == Math.Truncate(_posToSwap.z * 10))
                {
                    _isForwardSwap = false;
                }
                else if (Math.Truncate(_pick.transform.position.z * 10) == Math.Truncate(_posFromSwap.z * 10))
                { 
                    _isForwardSwap = true;
                    _isSwapped = true;
                }
            }
        }
    }
}

