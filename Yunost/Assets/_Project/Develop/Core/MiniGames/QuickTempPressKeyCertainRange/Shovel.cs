using System.Collections.Generic;
using UnityEngine;


namespace MiniGames
{
    namespace QuickTempPressKeyCertainRange 
    {
        public class Shovel
        {
            private GameObject _shovel;
            private GameObject _ground;
            private List<GameObject> _pileDirt = new List<GameObject>(new GameObject[3]); // ћассив с кусочками гр€зи,
                                                                                          // котора€ будет лежать на земле

            private float _timeAnimation;

            private Vector3 _startPos; // Ќачальное положение лопаты
            private Quaternion _startRot; // Ќачальное значение поворота лопаты

            private int _stageAnim = 0; // Ётап анимации

            private Vector3 _velMoveToGround = new Vector3(0,0,0);
            private float _smoothTimeMoveToGround = 0.5f; // ¬рем€ перемещение лопаты к земле
            private float _lastYPosMoveToGround = 0; // ѕоследнее положение Y лопаты при перемещении лопаты к земле

            private bool _isFirstEntryDigUp = true;    
            private Vector3 _velDigUp = new Vector3(0, 0, 0);
            private float _smoothTimeDigUp = 0.5f; // ¬рем€ погружени€ в землю при вскапывании
            private float _smoothTimeRotDigUp = 0.005f; // ¬рем€ вскапывани€ земли
            private float _lastYPosDigUp = 0; // ѕоследнее положение Y лопаты при вскапывании земли
            private Vector3 _targetCoordDigUp; //  онечные координаты при вскапывании земли
            private float _lastXPosRotDigUp = -1; // ѕоследнее положение поворота по X лопаты при вскапывании земли
            private const float _targetXAngleDigUp = 40; //  онечное значение поворота по X при вскапывании земли

            private Vector3 _targetYPosMoveDirtOnGround; //  онечное значение Y при перемещени€ гр€зи на землю
            private float _lastYPosMoveDirtOnGround = 0; // ѕоследнее положение Y лопаты при перемещени€ гр€зи на землю
            private float _lastXPosRotMoveDirtOnGround = -1; // ѕоследнее положение поворота по X лопаты при перемещени€ гр€зи на землю
            private float _smoothTimeMoveDirtOnGround = 0.005f; // ¬рем€ перемещени€ гр€зи на землю

            private float _smoothTimeMoveToStartPosition = 0.5f; // ¬рем€ перемещени€ лопаты в исходное состо€ние
            private float _smoothTimeRotMoveToStartPosition = 0.005f; // ¬рем€ поворота лопаты в исходное состо€ние

            public delegate void AnimationEndHandler(bool isAnimate);
            public event AnimationEndHandler OnAnimationChange;

            public delegate void ChangeStageVisualHandler();
            public event ChangeStageVisualHandler OnChangeStageVisual;  

            public Shovel(float timeAnimation)
            {
                _shovel = GameObject.Find("Shovel");
                _ground = GameObject.Find("Ground");
                InitPileDirt();
                _ground.SetActive(false);
                _startPos = _shovel.transform.position;
                _startRot = _shovel.transform.localRotation;
                _targetYPosMoveDirtOnGround = new Vector3(_startPos.x, _startPos.y - 5.02f, _startPos.z);
                InitSmoothTimes(timeAnimation);
            }

            private void InitPileDirt()
            {
                _pileDirt[0] = GameObject.Find("First_pile_dirt");
                _pileDirt[1] = GameObject.Find("Second_pile_dirt");
                _pileDirt[2] = GameObject.Find("Third_pile_dirt");

                foreach (GameObject pileDirt in _pileDirt) 
                {
                    pileDirt.SetActive(false);  
                }
            }

            private void InitSmoothTimes(float timeAnimation)
            {
                _timeAnimation = timeAnimation;
                float timeForAnim = 0.005f * (_timeAnimation / 6f);

                _smoothTimeMoveToGround = timeForAnim / 2f;
                _smoothTimeDigUp = timeForAnim / 2f;
                _smoothTimeRotDigUp = timeForAnim / 10f;
                _smoothTimeMoveDirtOnGround = timeForAnim / 10f;
                _smoothTimeMoveToStartPosition = timeForAnim;
                _smoothTimeRotMoveToStartPosition = timeForAnim / 10f;

                //Debug.Log($@"
                //    MoveToGround: {_smoothTimeMoveToGround},
                //    DigUp: {_smoothTimeDigUp},
                //    RotDigUp: {_smoothTimeRotDigUp},
                //    MoveDirtOnGround: {_smoothTimeMoveDirtOnGround},
                //    MoveToStartPosition: {_smoothTimeMoveToStartPosition},
                //    RotMoveToStartPosition: {_smoothTimeRotMoveToStartPosition}
                //");
            }

            public void Dig(int numStage)
            {
                ChooseAnimation(numStage);
            }

            private void ChooseAnimation(int numStage)
            {
                switch (_stageAnim)
                {
                    case 0:
                        MoveToGround(numStage); break;
                    case 1:
                        DigUp(numStage); break;
                    case 2:
                        MoveDirtOnGround(numStage); break;
                    case 3:
                        MoveToStartPosition(); break;
                }
            }

            private void MoveToGround(int numStage)
            {
                if (_lastYPosMoveToGround == _shovel.transform.position.y)
                {
                    _stageAnim++;
                    return;
                }

                _lastYPosMoveToGround = _shovel.transform.position.y;
                Vector3 targetCoord = getTargetCoordMoveToGround(numStage);
                _shovel.transform.position = Vector3.SmoothDamp(_shovel.transform.position, targetCoord, ref _velMoveToGround, _smoothTimeMoveToGround);
            }

            private Vector3 getTargetCoordMoveToGround(int numStage) 
            {
                //switch (numStage) 
                //{
                //    case 1: return new Vector3(_startPos.x, _startPos.y - 5.02f, _startPos.z);
                //    case 2: return new Vector3(_startPos.x, _startPos.y - 6.3f, _startPos.z);
                //    case 3: return new Vector3(_startPos.x, _startPos.y - 7.87f, _startPos.z);
                //    default: return _startPos;   
                //}
                
                // REMASTERED
                switch (numStage) 
                {
                    case 1: return new Vector3(_startPos.x, _startPos.y - 5.02f, _startPos.z);
                    case 2: return new Vector3(_startPos.x, _startPos.y - 8.54f, _startPos.z);
                    case 3: return new Vector3(_startPos.x, _startPos.y - 11f, _startPos.z);
                    default: return _startPos;
                }
            }

            private void DigUp(int numStage)
            {
                if (_lastYPosDigUp == _shovel.transform.position.y)
                {

                    if (Mathf.Abs(_lastXPosRotDigUp - _shovel.transform.localRotation.x) <= 0.0002)
                    {
                        OnChangeStageVisual?.Invoke();
                        _ground.SetActive(true);   
                        _stageAnim++;
                        return;
                    }

                    RotateShovel();
                }

                DigGround(numStage);    
            }

            private void RotateShovel()
            {
                _lastXPosRotDigUp = _shovel.transform.localRotation.x;
                Quaternion rotate = Quaternion.Euler(new Vector3(_targetXAngleDigUp, 60, 0));
                _shovel.transform.localRotation = Quaternion.Slerp(_shovel.transform.localRotation, rotate, _smoothTimeRotDigUp);
            }

            private void DigGround(int numStage)
            {
                _lastYPosDigUp = _shovel.transform.position.y;

                if (_isFirstEntryDigUp)
                {
                    Vector3 currPos = _shovel.transform.position;
                    _targetCoordDigUp = new Vector3(currPos.x, currPos.y - 2f, currPos.z);
                    _isFirstEntryDigUp = false;
                }

                _shovel.transform.position = Vector3.SmoothDamp(_shovel.transform.position, _targetCoordDigUp, ref _velDigUp, _smoothTimeDigUp);
            }

            private void MoveDirtOnGround(int numStage)
            {

                if (_lastYPosMoveDirtOnGround == _shovel.transform.position.y)
                {
                    if (Mathf.Abs(_lastXPosRotMoveDirtOnGround - _shovel.transform.localRotation.x) <= 0.0002)
                    {
                        _pileDirt[numStage - 1].SetActive(true);
                        _ground.SetActive(false);
                        _stageAnim++;
                        return;
                    }

                    _lastXPosRotMoveDirtOnGround = _shovel.transform.localRotation.x;
                    Quaternion rotate = Quaternion.Euler(new Vector3(40, 80, -30));
                    _shovel.transform.rotation = Quaternion.Slerp(_shovel.transform.rotation, rotate, _smoothTimeMoveDirtOnGround);
                }

                _lastYPosMoveDirtOnGround = _shovel.transform.position.y;
                _shovel.transform.position = Vector3.SmoothDamp(_shovel.transform.position, _targetYPosMoveDirtOnGround, ref _velDigUp, _smoothTimeDigUp);
            }

            private void MoveToStartPosition()
            {
                if (Mathf.Abs(_shovel.transform.position.y - _startPos.y) <= 0.0005)
                {
                    _stageAnim = 0;
                    _isFirstEntryDigUp = true; 
                    OnAnimationChange?.Invoke(false);
                    return;
                }

                _shovel.transform.position = Vector3.SmoothDamp(_shovel.transform.position, _startPos, ref _velMoveToGround, _smoothTimeMoveToStartPosition);
                _shovel.transform.rotation = Quaternion.Slerp(_shovel.transform.localRotation, _startRot, _smoothTimeRotMoveToStartPosition);
            }
        }
    }
}