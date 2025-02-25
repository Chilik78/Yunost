using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    namespace HoldingObjectInRange
    {
        public class Hook
        {
            private GameObject _hook;
            private GameObject _spawnPoint;
            private Vector3 _startPosition;
            private const float _spreadX = 4f;
            private const float _spreadZ = 3f;
            private List<GameObject> _fishs = new List<GameObject>();

            private Vector3 _targetPosition;
            private Vector3 _velTargetPosition = new Vector3(0, 0, 0);
            private bool _isTargetPosInitialized = false;
            private const float _smoothTime = 1f;

            public float getPosY { get => _hook.transform.localPosition.y; }

            public Hook()
            {
                _hook = GameObject.Find("Hook");
                _spawnPoint = GameObject.Find("Spawn Point");
                _startPosition = _hook.transform.position;
                InitFishs();
            }

            private void InitFishs()
            {
                int i = 1;
                GameObject fish = GameObject.Find($"Fish {i}");
                
                while (fish != null)
                {
                    _fishs.Add(fish);
                    fish.SetActive(false);  
                    i++;
                    fish = GameObject.Find($"Fish {i}");
                }
            }

            public void DoRandomSpawn()
            {
                if (!_isTargetPosInitialized)
                {
                    GeneratePosition();
                    return;
                }

                _hook.transform.position = Vector3.SmoothDamp(_hook.transform.position, _targetPosition,
                    ref _velTargetPosition, _smoothTime);
            }

            private void GeneratePosition()
            {
                Vector2 posSP = new Vector2(_spawnPoint.transform.position.x, _spawnPoint.transform.position.z);

                float randX = UnityEngine.Random.Range(posSP.x - _spreadX, posSP.x + _spreadX);
                float randZ = UnityEngine.Random.Range(posSP.y, posSP.y + _spreadZ);
                _targetPosition = new Vector3(randX, _spawnPoint.transform.position.y, randZ);
                _isTargetPosInitialized = true;
            }

            public void MoveHookByY(float diffPosY, float smoothTime)
            { 
                Vector3 currPos = _hook.transform.position;
                Vector3 targetCoords = new Vector3(currPos.x, currPos.y + diffPosY, currPos.z);

                if (smoothTime != 0)
                    _hook.transform.position = Vector3.SmoothDamp(currPos, targetCoords, ref _velTargetPosition, smoothTime);
                else
                    _hook.transform.position = targetCoords;
            }

            public bool isInTargetPos()
            {
                return Vector3.Distance(_targetPosition, _hook.transform.position) < 0.1f;
            }

            public bool isInStartPos()
            {
                return Vector3.Distance(_startPosition, _hook.transform.position) < 0.1f;
            }

            public void ShowFish()
            {
                int indexFish = Random.Range(0, _fishs.Count - 1);
                _fishs[indexFish].SetActive(true);  
            }

            public void MoveToStartPosition()
            {
                _hook.transform.position = Vector3.SmoothDamp(_hook.transform.position, _startPosition,
                    ref _velTargetPosition, _smoothTime);
            }
        }
    }
}