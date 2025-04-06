using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace MiniGames
{ 
    namespace ReachEndPointWithObstacles
    {
        public class EndPointController : MonoBehaviour
        {
            private Vector2 _spawnCoord;
            private GameObject _endPoint;
            private int _distanceCoordToSpawn;

            public Vector2 GetCurrentCoord { get => _spawnCoord; }

            public void Init(int distanceCoordToSpawn=2)
            {
                _endPoint = GameObject.Find("EndPoint MiniGame");
                _distanceCoordToSpawn = distanceCoordToSpawn;
            }

            public void SpawnEnpoint(Vector2 currentCoord, ObstacleCell[,] cells)
            {
                while(_spawnCoord == currentCoord)
                {
                    GetNewRespawnCoord(currentCoord, cells);
                }

                GameObject choosenCell = GameObject.Find($"Obstacle {_spawnCoord.x}|{_spawnCoord.y}");
                Vector3 spawnPosition = choosenCell.transform.GetChild(0).transform.position;
                spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z);
                _endPoint.transform.position = spawnPosition;
            }

            private void GetNewRespawnCoord(Vector2 currentCoord, ObstacleCell[,] cells)
            {
                _spawnCoord = new Vector2(0f, 0f);
                bool isBreak = false;

                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                {
                    if (isBreak)
                        break;

                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                    {
                        if ((idxRow == currentCoord.x && idxCol == currentCoord.y)
                            || (Mathf.Abs(currentCoord.y - idxCol) <= _distanceCoordToSpawn
                            && Mathf.Abs(currentCoord.x - idxRow) <= _distanceCoordToSpawn))
                        {
                            continue;
                        }

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
            }
        }
    }
}