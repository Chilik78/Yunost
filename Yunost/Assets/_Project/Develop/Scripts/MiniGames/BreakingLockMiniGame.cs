using System;
using UnityEngine;

namespace MiniGames
{
    public class BreakingLockMiniGame : MiniGame
    {
        private float _unlockAngle; // ����� ���� ������� �����
        private Vector2 _unlockRange; // �������� �����, ��� ������� ����� ����� ��������� ��������

        private float _maxRotationAngle = 90; // ��� ������ ����� �������������� ��������� ����� ����� (����� � ������)
        private float _forConvertToAngle; // ��� ����������� � ���������� ���� ��������
        private float _lockRange; // ��������� �����

        private float _currPosPick = 0f; // ������� ���� �������

        private GameObject _lock; // GameObject ��������� ��������� �����
        private GameObject _pick; // GameObject �������
        private GameObject _screwdriver; // GameObject �������

        private float _timeCount = 0.0f;
        private float _maxDiffBetweenUnlocAngleAndCurrPosPick = 2; // ����������� ��������� ������� ����� ������� ����� ������� � �����, ������� ������� �����
        private float _speedRotateLock = 0.001f; // �������� �������� �����

        public override void Init(MiniGameContext context)
        {
            _currentContext = context;
            TypeDifficultMiniGames difficult = _currentContext.�urrentDifficult;
            _lock = GameObject.Find("sm_lock_02");
            _pick = GameObject.Find("LockPick");
            _screwdriver = GameObject.Find("Screwdriver_Tool");
            _forConvertToAngle = 1 / _maxRotationAngle;

            ChooseLockRangeByDifficult(difficult);
            GenerateUnlockAngle();
        }

        private void ChooseLockRangeByDifficult(TypeDifficultMiniGames difficult)
        {
            switch (difficult)
            {
                case TypeDifficultMiniGames.Easy: _lockRange = 10; return;
                case TypeDifficultMiniGames.Medium: _lockRange = 5; return;
                case TypeDifficultMiniGames.Hard: _lockRange = 2; return;
            }
        }

        private void GenerateUnlockAngle()
        {
            _unlockAngle = UnityEngine.Random.Range(-_maxRotationAngle, _maxRotationAngle);
            _unlockRange = new Vector2(_unlockAngle - _lockRange, _unlockAngle + _lockRange);
        }

        public override void TrackingProgressGame()
        {
            RotatePick();
            RotateScrewdriver();
            RotateLock();
        }

        private void RotatePick()
        {
            float moveHorizontalMouse = Input.GetAxis("Mouse X");
            float newPos = _currPosPick + moveHorizontalMouse * (_maxRotationAngle * _forConvertToAngle);

            if (Mathf.Abs(newPos) < _maxRotationAngle)
            {
                _currPosPick = newPos;
                _pick.transform.localEulerAngles = new Vector3(0, _currPosPick, 0); 
            }
        }

        private void RotateScrewdriver()
        {
            if(_currPosPick < 0)
            {
                _screwdriver.transform.localEulerAngles = new Vector3(0, 20, 0);
            }
            else
            {
                _screwdriver.transform.localEulerAngles = new Vector3(0, -10, 0);
            }
        }

        private void RotateLock()
        {
            float moveHorizontalKeyboard = Input.GetAxis("Horizontal");
            float diffBetweenUnlocAngleAndCurrPosPick = Math.Abs(_unlockAngle - _currPosPick);

            if (moveHorizontalKeyboard > 0)
            {
                var rotate = Quaternion.Euler(new Vector3(0, diffBetweenUnlocAngleAndCurrPosPick > _maxRotationAngle ? 5 : _maxRotationAngle - diffBetweenUnlocAngleAndCurrPosPick, 0));

                if (diffBetweenUnlocAngleAndCurrPosPick <= _maxDiffBetweenUnlocAngleAndCurrPosPick)
                {
                    rotate = Quaternion.Euler(new Vector3(0, _maxRotationAngle, 0));
                }

                _lock.transform.localRotation = Quaternion.Slerp(_lock.transform.localRotation, rotate, _speedRotateLock * _timeCount);
            }
            else if (_lock.transform.localRotation.y != 0 && diffBetweenUnlocAngleAndCurrPosPick > _maxDiffBetweenUnlocAngleAndCurrPosPick && moveHorizontalKeyboard == 0)
            {
                var rotate = Quaternion.Euler(new Vector3(0, 0, 0));
                _lock.transform.localRotation = Quaternion.Slerp(_lock.transform.localRotation, rotate, _speedRotateLock * _timeCount);
            }

            if(_maxRotationAngle - _lock.transform.localEulerAngles.y <= 1)
            {
                Debug.LogError("���� ��������");
            }

            _timeCount += Time.deltaTime;
        }

    }
}

