using System.Collections.Generic;
using UnityEngine;

namespace MiniGames
{
    namespace Maze
    {
        public class PlayerController : MonoBehaviour
        {
            private GameObject _player;
            private MazeCell[,] _mazeGrid;
            private Stack<Vector2> _coordsVisitedCells;
            private Vector2 _exitCoords;
            private Vector2 _currentCoords;
            private LineRenderer _line;

            public delegate void OnExitHandler(MiniGameResultInfo resultInfo);
            public event OnExitHandler OnExit;

            public void Init(MazeCell[,] maze, float widthLine)
            {
                _player = GameObject.Find("Player MiniGame");
                _mazeGrid = maze;
                _coordsVisitedCells = new Stack<Vector2>();
                _line = GameObject.Find("Paper MiniGame").GetComponent<LineRenderer>();
                _line.startWidth = widthLine;
                _line.endWidth = widthLine;
            }

            public void SpawnPlayer(Vector2 exitCoord)
            {
                _exitCoords = exitCoord;    

                int spawnIdxRow = 0;
                int spawnIdxCol = 0;

                int farthestDistance = GetFarthestWay(exitCoord);

                for (int i = 0; i < _mazeGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < _mazeGrid.GetLength(1); j++)
                    {
                        if (_mazeGrid[i, j].distanceFromExit == farthestDistance)
                        {
                            spawnIdxRow = i;
                            spawnIdxCol = j;
                            break;
                        }
                    }
                }

                _currentCoords = new Vector2(spawnIdxRow, spawnIdxCol);
                _coordsVisitedCells.Push(_currentCoords);
                GameObject cell = GameObject.Find($"Maze Grid {spawnIdxRow}|{spawnIdxCol}");
                _player.GetComponent<RectTransform>().localPosition = cell.GetComponent<RectTransform>().localPosition;
                _line.positionCount += 1;
                _line.SetPosition(0, cell.transform.position);
            }

            private int GetFarthestWay(Vector2 exitCoord)
            {
                MazeCell currCell = _mazeGrid[(int)exitCoord.x, (int)exitCoord.y];
                Stack<MazeCell> stackCells = new Stack<MazeCell>();
                List<MazeCell> unvisitedNeighbours = new List<MazeCell>();
                currCell.distanceFromExit = 0;
                currCell.isVisited = true;
                stackCells.Push(currCell);

                do
                {
                    int currIdxRow = currCell.idxRow;
                    int currIdxCol = currCell.idxCol;

                    if (currIdxRow > 0 && !_mazeGrid[currIdxRow - 1, currIdxCol].isVisited && !currCell.wallTop)
                        unvisitedNeighbours.Add(_mazeGrid[currIdxRow - 1, currIdxCol]);

                    if (currIdxCol > 0 && !_mazeGrid[currIdxRow, currIdxCol - 1].isVisited && !currCell.wallLeft)
                        unvisitedNeighbours.Add(_mazeGrid[currIdxRow, currIdxCol - 1]);

                    if (currIdxRow < _mazeGrid.GetLength(0) - 1 && !_mazeGrid[currIdxRow + 1, currIdxCol].isVisited && !currCell.wallBottom)
                        unvisitedNeighbours.Add(_mazeGrid[currIdxRow + 1, currIdxCol]);

                    if (currIdxCol < _mazeGrid.GetLength(1) - 1 && !_mazeGrid[currIdxRow, currIdxCol + 1].isVisited && !currCell.wallRight)
                        unvisitedNeighbours.Add(_mazeGrid[currIdxRow, currIdxCol + 1]);

                    //if (stackCells.Count <= 1 && unvisitedNeighbours.Count <= 0)
                    //{
                    //    Debug.Log($"LOL1 {currCell.idxRow}|{currCell.idxCol}|[{currCell.wallTop},{currCell.wallRight},{currCell.wallBottom},{currCell.wallLeft}]");
                    //}

                    if (unvisitedNeighbours.Count > 0)
                    {
                        if(stackCells.Count == 0)
                        {
                            stackCells.Push(currCell);
                        }

                        MazeCell choosenCell = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                        choosenCell.isVisited = true;
                        choosenCell.distanceFromExit = currCell.distanceFromExit + 1;
                        currCell = choosenCell;
                        stackCells.Push(choosenCell);
                        unvisitedNeighbours.Clear();
                    }
                    else if (stackCells.Count > 0)
                    {
                        currCell = stackCells.Pop();
                    }
                    else if (stackCells.Count == 0)
                    {
                        currCell = null;
                    }

                    
                    /*if(currCell != null)
                    {
                        GameObject cell = GameObject.Find($"Maze Grid {currCell.idxRow}|{currCell.idxCol}");
                        cell.GetComponent<TMP_Text>().fontSize = 150f;
                        cell.GetComponent<TMP_Text>().text = $"{currCell.distanceFromExit}";
                    }

                    if (stackCells.Count <= 0 && currCell != null)
                    {
                        Debug.Log($"LOL2 {currCell.idxRow}|{currCell.idxCol}|[{currCell.wallTop},{currCell.wallRight},{currCell.wallBottom},{currCell.wallLeft}]");
                    }*/

                } while (currCell != null);

                int farthestDistance = 0;

                for (int i = 0; i < _mazeGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < _mazeGrid.GetLength(1); j++)
                    {
                        if (farthestDistance < _mazeGrid[i, j].distanceFromExit)
                            farthestDistance = _mazeGrid[i, j].distanceFromExit;
                    }
                }

                return farthestDistance;
            }

            public void Move()
            {
                Vector2 oldCoords = _currentCoords;
                MoveToCell();
                DrawRoute(oldCoords);
            }

            private void MoveToCell()
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _currentCoords.y + 1 < _mazeGrid.GetLength(1) && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallRight)
                    {
                        _currentCoords.y += 1;
                    }
                    else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _currentCoords.y - 1 >= 0 && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallLeft)
                    {
                        _currentCoords.y -= 1;
                    }
                    else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _currentCoords.x - 1 >= 0 && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallTop)
                    {
                        _currentCoords.x -= 1;
                    }
                    else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _currentCoords.x + 1 < _mazeGrid.GetLength(0) && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallBottom)
                    {
                        _currentCoords.x += 1;
                    }
                    else
                    {
                        bool isExit = false;

                        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _currentCoords.y + 1 == _mazeGrid.GetLength(1) && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallRight)
                        {
                            isExit = true;
                        }
                        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _currentCoords.y - 1 < 0 && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallLeft)
                        {
                            isExit = true;
                        }
                        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _currentCoords.x - 1 < 0 && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallTop)
                        {
                            isExit = true;
                        }
                        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _currentCoords.x + 1 == _mazeGrid.GetLength(0) && !_mazeGrid[(int)_currentCoords.x, (int)_currentCoords.y].wallBottom)
                        {
                            isExit = true;
                        }

                        if(isExit)
                        {
                            OnExit?.Invoke(new MiniGameResultInfo(TypeResultMiniGames.Ñompleted, 0));
                            return;
                        }
                    }
                    
                    GameObject cell = GameObject.Find($"Maze Grid {_currentCoords.x}|{_currentCoords.y}");
                    _player.GetComponent<RectTransform>().localPosition = cell.GetComponent<RectTransform>().localPosition;
                }
            }

            private void DrawRoute(Vector2 oldCoords)
            {
                if(_currentCoords != oldCoords)
                {
                    if (_coordsVisitedCells.Count != 0 && _coordsVisitedCells.Peek() != _currentCoords)
                    {
                        _coordsVisitedCells.Push(oldCoords);
                        _line.positionCount += 1;
                        GameObject cell = GameObject.Find($"Maze Grid {_currentCoords.x}|{_currentCoords.y}");
                        _line.SetPosition(_line.positionCount - 1, cell.transform.position);
                        return;
                    }

                    _coordsVisitedCells.Pop();
                    _line.positionCount -= 1;
                }
            }
        }
    }
}