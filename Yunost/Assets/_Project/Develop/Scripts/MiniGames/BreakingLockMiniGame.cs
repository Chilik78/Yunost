using MiniGames.BreakingLock;
using System;
using TMPro;
using UnityEngine;

namespace MiniGames
{
    public class BreakingLockMiniGame : MiniGame
    {
        private bool isGameEnd = false;

        private float _unlockAngle; // ����� ���� ������� �����
        private Vector2 _unlockRange; // �������� �����, ��� ������� ����� ����� ��������� ��������

        private float _maxRotationAngle = 90; // ��� ������ ����� �������������� ��������� ����� ����� (����� � ������)
        private float _lockRange; // ��������� �����

        private Lock _lock; // �������� ��������� �����
        private Pick _pick; // �������
        private Screwdriver _screwdriver; // GameObject �������

        private TMP_Text _textCountPicks;

        private float _maxDiffBetweenUnlocAngleAndCurrPosPick = 3; // ����������� ��������� ������� ����� ������� ����� ������� � �����, ������� ������� �����

        private int _countPicks;

        public override void Init(MiniGameContext context)
        {
            _currentContext = context;
            TypeDifficultMiniGames difficult = _currentContext.�urrentDifficult;
            _countPicks = context.getCountItems;

            _pick = new Pick(_maxRotationAngle);
            _screwdriver = new Screwdriver();
            _lock = new Lock(_unlockAngle, _maxDiffBetweenUnlocAngleAndCurrPosPick, _maxRotationAngle, _pick);

            _pick.OnPickBroken += OnBrokenPick;

            _textCountPicks = GameObject.Find("Count LockPicks Text").GetComponent<TMP_Text>();

            ChooseLockRangeByDifficult(difficult);
            GenerateUnlockAngle();
            BuildUI();
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
            if (!isGameEnd && _pick.getSwap)
            {
                _pick.Rotate();
                _screwdriver.Rotate(_pick.getCurrPosPick);
                _lock.Rotate();
                CheckEndGame();
            }
            else if (!_pick.getSwap)
            {
                _pick.SwapPick();
            }
        }

        private void CheckEndGame()
        {
            if (Math.Abs(_maxRotationAngle - _lock.getRotateAngle) <= _maxDiffBetweenUnlocAngleAndCurrPosPick && _countPicks != 0)
            {
                isGameEnd = true;
                _pick.OnPickBroken -= OnBrokenPick;
                CalculateResult(new MiniGameResultInfo(TypeResultMiniGames.�ompleted, _currentContext.getCountItems - _countPicks));
            }
            else if(_countPicks == 0)
            {
                isGameEnd = true;
                _pick.OnPickBroken -= OnBrokenPick;
                CalculateResult(new MiniGameResultInfo(TypeResultMiniGames.Failed, _currentContext.getCountItems));
            }
        }

        private void OnBrokenPick()
        {
            _countPicks--;
            Debug.LogWarning($"Count Picks {_countPicks}");
            BuildUI();
            CheckEndGame();
        }

        protected override void BuildUI()
        {
            _textCountPicks.text = $"�������� �������: {_countPicks}";
        }
    }
}

