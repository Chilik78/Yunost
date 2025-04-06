using System.Collections;
using TMPro;
using UnityEngine;

namespace MiniGames
{
    namespace ReachEndPointWithObstacles
    {
        public class PlayerController : MonoBehaviour
        {
            private GameObject _player;
            private Color _playerColor;
            private GameObject _pressKey;
            private Rigidbody _playerRB; 
            private float _speed;
            private Vector2 _spawnCoord;
            private KeyCode[] _keysForEndpoint;
            private KeyCode _currentPressKey = KeyCode.None;

            public delegate void OnPressKeyHandler();
            public OnPressKeyHandler OnPressKey;

            private float _freezeTime;
            private bool _isFreeze;

            public Vector2 GetSpawnCoord { get => _spawnCoord; }

            public void Init(float speedPlayer, float freezeTimeInSeconds, KeyCode[] keysForEndpoint)
            {
                _player = GameObject.Find("Player MiniGame");
                _playerColor = _player.GetComponent<Renderer>().material.color;
                _pressKey = _player.transform.GetChild(0).gameObject;
                _playerRB = _player.GetComponent<Rigidbody>();
                _speed = speedPlayer;
                _keysForEndpoint = keysForEndpoint;
                _freezeTime = freezeTimeInSeconds;   
                _isFreeze = false;

                _pressKey.SetActive(false);
            }

            public void Move()
            {
                if (!_isFreeze)
                {
                    float horInpt = Input.GetAxis("Horizontal");
                    float verInpt = Input.GetAxis("Vertical");

                    if (horInpt != 0)
                    {
                        Vector3 moveDirection = horInpt > 0 ? transform.right : transform.right * -1;
                        _playerRB.AddForce(moveDirection * _speed * Mathf.Abs(horInpt));
                    }

                    if (verInpt != 0)
                    {
                        Vector3 moveDirection = verInpt > 0 ? transform.up : transform.up * -1;
                        _playerRB.AddForce(moveDirection * _speed * Mathf.Abs(verInpt));
                    }
                }
            }

            public void SpawnPlayer(ObstacleCell[,] cells)
            {
                _spawnCoord = new Vector2(0f, 0f);
                bool isBreak = false;

                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow += cells.GetLength(0) - 1) 
                {
                    if (isBreak)
                        break;  

                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++) 
                    {
                        if (UnityEngine.Random.Range(0, 2) == 1 
                            && !cells[idxRow, idxCol].wallLeft 
                            && !cells[idxRow, idxCol].wallBottom)
                        {
                            _spawnCoord = new Vector2(idxRow, idxCol);
                            isBreak = true;
                            break;
                        }
                    }
                }

                GameObject choosenCell = GameObject.Find($"Obstacle {_spawnCoord.x}|{_spawnCoord.y}");
                Vector3 spawnPosition = choosenCell.transform.GetChild(0).transform.position;
                float shiftY = 10f;
                spawnPosition = new Vector3(spawnPosition.x, _spawnCoord.x > 0 ? spawnPosition.y - shiftY : spawnPosition.y + shiftY, _player.transform.position.z);
                _player.transform.position = spawnPosition;
            }

            private void OnTriggerEnter(Collider collider)
            {
                GameObject gameObj = collider.gameObject;

                if (gameObj.tag == "MiniGame ReachEndPointWithObstacles Dangerous Obstacle")
                {
                    PushAwayPlayer(gameObj);
                    FreezePlayer();
                }
                else if (gameObj.tag != "MiniGame ReachEndPointWithObstacles Endpoint")
                {
                    PushAwayPlayer(gameObj);
                }
                else
                {
                    GetRandomPressKey();
                }
            }

            private void OnTriggerExit(Collider collider)
            {
                GameObject gameObj = collider.gameObject;

                if (gameObj.tag == "MiniGame ReachEndPointWithObstacles Endpoint")
                {
                    _pressKey.SetActive(false);
                    _currentPressKey = KeyCode.None;
                }
            }

            private void PushAwayPlayer(GameObject obstacle)
            {
                Vector3 obstaclePos = obstacle.transform.localPosition;
                Vector3 playerPos = _player.transform.localPosition; 
                Vector3 diff = obstaclePos - playerPos;
                Vector3 signs = new Vector3(Mathf.Sign(diff.x), Mathf.Sign(diff.y), 0);

                const float coefficient = 1f;
                Vector3 pushDirection = new Vector3(playerPos.x * coefficient, playerPos.y * coefficient, 0);
                pushDirection = new Vector3(pushDirection.x * signs.x, pushDirection.y * signs.y, 0);
                //pushDirection.Normalize();

                //Debug.Log(pushDirection);
                _playerRB.AddForce(pushDirection * (_speed / 4f));
            }

            private void FreezePlayer()
            {
                _isFreeze = true;
                _player.GetComponent<Renderer>().material.color = Color.yellow;
                StartCoroutine(Freeze());
            }

            private IEnumerator Freeze()
            {
                yield return new WaitForSecondsRealtime(_freezeTime);
                _isFreeze = false;
                _player.GetComponent<Renderer>().material.color = _playerColor;
            }

            private void GetRandomPressKey()
            {
                int idxPressKey = UnityEngine.Random.Range(0, _keysForEndpoint.Length);
                _currentPressKey = _keysForEndpoint[idxPressKey];
                _pressKey.GetComponent<TMP_Text>().text = $"{_currentPressKey.ToString()}";
                _pressKey.SetActive(true);
            }

            public void CheckPressKey()
            {
                if (_currentPressKey != KeyCode.None && Input.GetKeyDown(_currentPressKey) && !_isFreeze)
                {
                    OnPressKey?.Invoke();
                }
            }
        }
    }
}