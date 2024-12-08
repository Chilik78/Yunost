using System;
using UnityEngine;

namespace MiniGames
{
    namespace BreakingLock 
    {
        public class Lock
        {
            private GameObject _lock; // GameObject крутящего основания замка
            private Pick _pick; // Отмычка
            private float _unlockAngle; // Какой угол откроет замок
            private float _maxRotationAngle = 90; // Как далеко может поворачиваться внутреняя часть замка (влево и вправо)
            private float _maxDiffBetweenUnlocAngleAndCurrPosPick = 3; // Максимально возможная разница между
                                                                       // текущим углом отмычки и углом, который откроет замок

            //private float _speedRotateLock = 0.001f; // Скорость поворота замка
            private float _speedRotateLock = 0.07f; // Скорость поворота замка

            private float _speedShake = 1f; // Скорость тряски

            public float getRotateAngle { get => _lock.transform.localEulerAngles.y; }

            public Lock(float unlockAngle, float maxDiffBetweenUnlocAngleAndCurrPosPick, float maxRotationAngle, Pick pick)
            {
                _lock = GameObject.Find("sm_lock_02");
                _unlockAngle = unlockAngle;
                _maxDiffBetweenUnlocAngleAndCurrPosPick = maxDiffBetweenUnlocAngleAndCurrPosPick;
                _maxRotationAngle = maxRotationAngle;
                _pick = pick;
            }

            public void Rotate()
            {
                float moveHorizontalKeyboard = Input.GetAxis("Horizontal");
                float diffBetweenUnlocAngleAndCurrPosPick = Math.Abs(_unlockAngle - _pick.getCurrPosPick);

                if (moveHorizontalKeyboard > 0)
                {
                    RotateForward(diffBetweenUnlocAngleAndCurrPosPick);
                }
                else if (_lock.transform.localRotation.y != 0 && diffBetweenUnlocAngleAndCurrPosPick > _maxDiffBetweenUnlocAngleAndCurrPosPick && moveHorizontalKeyboard == 0)
                {
                    RotateBack();
                }
            }

            private void RotateForward(float diffBetweenUnlocAngleAndCurrPosPick)
            {
                Quaternion rotate = getRotateForward(diffBetweenUnlocAngleAndCurrPosPick);
                _lock.transform.localRotation = Quaternion.Slerp(_lock.transform.localRotation, rotate, _speedRotateLock);

                if (rotate.y != _maxRotationAngle && _lock.transform.localRotation == rotate)
                {
                    Shake();
                }
            }

            private Quaternion getRotateForward(float diffBetweenUnlocAngleAndCurrPosPick)
            {
                Quaternion rotate = Quaternion.Euler(new Vector3(0, diffBetweenUnlocAngleAndCurrPosPick > _maxRotationAngle ? 5 : _maxRotationAngle - diffBetweenUnlocAngleAndCurrPosPick, 0));

                if (diffBetweenUnlocAngleAndCurrPosPick <= _maxDiffBetweenUnlocAngleAndCurrPosPick)
                {
                    rotate = Quaternion.Euler(new Vector3(0, _maxRotationAngle, 0));
                }

                return rotate;
            }

            private void RotateBack()
            {
                var rotate = Quaternion.Euler(new Vector3(0, 0, 0));
                _lock.transform.localRotation = Quaternion.Slerp(_lock.transform.localRotation, rotate, _speedRotateLock);
            }

            private void Shake()
            {
                float yAngle = (_lock.transform.localRotation.y * 100 - 0.1f);
                int sign = Math.Sign(yAngle) >= 0 ? 1 : -1;
                Quaternion rotate = Quaternion.Euler(new Vector3(0, sign * yAngle, 0));
                _lock.transform.localRotation = Quaternion.Slerp(_lock.transform.localRotation, rotate, _speedShake);
                _pick.Break();  
            }
        }
    }
}