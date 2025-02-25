using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGames
{
    namespace HoldingObjectInRange 
    {
        public class FishingRod
        {
            private GameObject _fishingRod;
            private FishingLine _fishingLine;
            private Hook _hook;

            private Quaternion _startPosRot;

            private bool _isRotateComplete = false;
            private bool _isReverse = false;
            private GameObject _fishingRodObject;
            private const float _inaccuracyAngles = 0.2f;
            private const float _targetYAngle = 60f;
            private const float _smoothTimeRot = 0.1f;

            private const float _maxMoveHookUp = -18.71f;
            private const float _maxMoveHookDown = -19.8f;
            private DirectionProgressIndicator _lastDirectionProgressIndicator = DirectionProgressIndicator.ToStartPosition;

            public delegate void AnimationEndHandler(bool isAnimate);
            public event AnimationEndHandler OnAnimationChange;

            public FishingRod()
            {
                _fishingRod = GameObject.Find("Fishing rod");
                _fishingRodObject = _fishingRod.transform.GetChild(0).gameObject;
                _startPosRot = _fishingRodObject.transform.localRotation;
                _fishingLine = new FishingLine();
                _hook = new Hook();
                _fishingLine.Build();
            }

            #region DoFish
            public void DoFish()
            {
                if(_isRotateComplete && _hook.isInTargetPos())
                {
                    _hook.ShowFish();
                    OnAnimationChange?.Invoke(false);
                    return;
                }

                if(_isRotateComplete)
                {
                    _hook.DoRandomSpawn();
                    _fishingLine.Update();
                    return;
                }

                Rotate();
            }

            private void Rotate()
            {
                Quaternion rotate = _isReverse ? 
                    Quaternion.Euler(new Vector3(0, _startPosRot.y * 100, 0)) :
                    Quaternion.Euler(new Vector3(0, _targetYAngle, 0));
                _fishingRodObject.transform.localRotation = Quaternion.Slerp(_fishingRodObject.transform.localRotation, 
                    rotate, _smoothTimeRot);

                bool isInRange = Quaternion.Angle(rotate, _fishingRodObject.transform.localRotation) <= _inaccuracyAngles;

                if (isInRange && !_isReverse)
                {
                    _fishingRodObject.transform.localRotation = Quaternion.Euler(new Vector3(0, _targetYAngle, 0));
                    _isReverse = true;
                }
                else if (isInRange && _isReverse)
                {
                    _fishingRodObject.transform.localRotation = Quaternion.Euler(new Vector3(0, _startPosRot.y * 100, 0));
                    _isReverse = false;
                    _isRotateComplete = true;
                }

                if(_isReverse)
                    _hook.DoRandomSpawn();
                else
                    _hook.MoveHookByY(0.01f, _smoothTimeRot / 10);

                _fishingLine.Update();
            }
            
            #endregion

            #region Fishing
            public void Rotate(DirectionProgressIndicator direction, int countSteps, float smoothTime = _smoothTimeRot / 10)
            {
                float rotateStep = Mathf.Abs(_targetYAngle - _startPosRot.eulerAngles.y) / countSteps;
                Quaternion currRotation = _fishingRodObject.transform.localRotation;

                bool isRotateUp = (direction == DirectionProgressIndicator.ToRight || 
                    (_lastDirectionProgressIndicator == DirectionProgressIndicator.ToLeft
                    && direction == DirectionProgressIndicator.ToStartPosition))
                    && currRotation.eulerAngles.y - rotateStep >= _targetYAngle;

                bool isRotateDown = (direction == DirectionProgressIndicator.ToLeft ||
                    (_lastDirectionProgressIndicator == DirectionProgressIndicator.ToRight
                    && direction == DirectionProgressIndicator.ToStartPosition))
                    && currRotation.eulerAngles.y - rotateStep <= _startPosRot.eulerAngles.y;

                if (isRotateUp)
                { 
                    Vector3 newRotation = new Vector3(0, currRotation.eulerAngles.y - rotateStep, 0);
                    _fishingRodObject.transform.localRotation = Quaternion.Euler(newRotation);
                    _fishingLine.Update();
                }
                else if (isRotateDown)
                {
                    Vector3 newRotation = new Vector3(0, currRotation.eulerAngles.y + rotateStep, 0);
                    _fishingRodObject.transform.localRotation = Quaternion.Euler(newRotation);
                    _fishingLine.Update();
                }
            }

            public void MoveHook(DirectionProgressIndicator direction, int countSteps, float smoothTime = _smoothTimeRot / 10)
            {
                float diffY = 0;

                if (direction == DirectionProgressIndicator.ToRight)
                {
                    _lastDirectionProgressIndicator = direction;
                    diffY = Mathf.Abs(_maxMoveHookUp - _maxMoveHookDown) / countSteps;
                }
                else if(direction == DirectionProgressIndicator.ToLeft)
                {
                    _lastDirectionProgressIndicator = direction;
                    diffY = Mathf.Abs(_maxMoveHookUp - _maxMoveHookDown) / -countSteps;
                }
                else if(_lastDirectionProgressIndicator == DirectionProgressIndicator.ToLeft 
                    && direction == DirectionProgressIndicator.ToStartPosition)
                {
                    diffY = Mathf.Abs(_maxMoveHookUp - _maxMoveHookDown) / countSteps;
                }
                else if(_lastDirectionProgressIndicator == DirectionProgressIndicator.ToRight
                    && direction == DirectionProgressIndicator.ToStartPosition)
                {
                    diffY = Mathf.Abs(_maxMoveHookUp - _maxMoveHookDown) / -countSteps;
                }

                bool isCanMove = _hook.getPosY + diffY <= _maxMoveHookUp && _hook.getPosY - diffY >= _maxMoveHookDown;
                if(isCanMove)
                {
                    _hook.MoveHookByY(diffY, smoothTime);
                    _fishingLine.Update();
                }
            }
            
            #endregion

            public void GetFishOut()
            {
                _hook.MoveToStartPosition();
                Quaternion newRotation = Quaternion.Euler(new Vector3(0, _targetYAngle - 5, 0));
                _fishingRodObject.transform.localRotation = Quaternion.Slerp(_fishingRodObject.transform.localRotation, newRotation, 0.1f);
                _fishingLine.Update();

                bool isInRangeRot = Quaternion.Angle(newRotation, _fishingRodObject.transform.localRotation) <= _inaccuracyAngles;
                if (isInRangeRot && _hook.isInStartPos())
                {
                    OnAnimationChange?.Invoke(false);
                }
            }
        }
    }
}