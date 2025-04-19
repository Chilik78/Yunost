using TMPro;
using UnityEngine;


namespace MiniGames
{
    namespace Maze
    {
        public class MazeCell
        {
            public int idxRow;
            public int idxCol;

            public MazeCell(int ID, int indexRow=0, int indexCol=0)
            {
                idxRow = indexRow;
                idxCol = indexCol;
                setID = ID;
            }

            public int setID;

            public bool isVisited = false;
            public int distanceFromExit = -1;

            public bool wallTop = true;
            public bool wallRight = true;
            public bool wallBottom = true;
            public bool wallLeft = true;
        }

        public class MazeGenerator : MonoBehaviour
        {
            [SerializeField, Header("Префаб клетки лабиринта")]
            GameObject _cellPrefab;

            GameObject _canvas;

            private Vector2 _gridSize;
            private Vector2 _spawnCoods;
            private MazeCell[,] _mazeGrid;
            private Vector2 _exitCoord;

            public MazeCell[,] GetMaze { get => _mazeGrid; }

            public Vector2 GetExitCoord { get => _exitCoord; }

            public void Init(Vector2 gridSize, Vector2 spawnCoords)
            {
                _canvas = GameObject.Find("Canvas MiniGame");
                _gridSize = gridSize;
                _spawnCoods = spawnCoords;  
                InitGrid();
                GenerateMaze();
            }

            private void InitGrid()
            {
                _mazeGrid = new MazeCell[(int)_gridSize.x, (int)_gridSize.y];

                for (int idxRow = 0; idxRow < _gridSize.x; idxRow++)
                {
                    for (int idxCol = 0; idxCol < _gridSize.y; idxCol++)
                    {
                        _mazeGrid[idxRow, idxCol] = new MazeCell(idxCol, idxRow, idxCol);
                    }
                }
            }

            private void GenerateMaze()
            {
                for (int i = 0; i < _mazeGrid.GetLength(0) - 1; i++)
                {
                    CreateRow(i);
                    CreateVerticalConnections(i);
                }
                CreateLastRow();
            }

            private void CreateRow(int rowNum)
            {
                for (int i = 0; i < _mazeGrid.GetLength(1) - 1; i++)
                {
                    var cell = _mazeGrid[rowNum, i];
                    var nextCell = _mazeGrid[rowNum, i + 1];
                    if (cell.setID != nextCell.setID)
                    {
                        bool isRemove = UnityEngine.Random.Range(0, 2) == 1;
                        if (isRemove)
                        {
                            RemoveVerticalWallBetweenCells(cell, nextCell);
                        }
                    }
                }
            }

            private void RemoveVerticalWallBetweenCells(MazeCell leftCell, MazeCell rightCell)
            {
                leftCell.wallRight = false;
                rightCell.wallLeft = false;
                rightCell.setID = leftCell.setID;
            }

            private void CreateVerticalConnections(int rowNum)
            {
                bool isAdded = false;

                for (int i = 0; i < _mazeGrid.GetLength(1) - 1; i++)
                {
                    MazeCell cell = _mazeGrid[rowNum, i];
                    MazeCell nextCell = _mazeGrid[rowNum, i + 1];

                    if (cell.setID != nextCell.setID && !isAdded)
                    {
                        RemoveHorizontalWall(cell);
                        isAdded = false;
                    }
                    else if (cell.setID != nextCell.setID && isAdded)
                    {
                        isAdded = false;
                    }
                    else if(cell.setID == nextCell.setID && !isAdded)
                    {
                        bool isRemove = Random.Range(0, 2) == 1;
                        if (isRemove)
                        {
                            RemoveHorizontalWall(cell);
                            isAdded = true;
                        }
                    }
                }
                CheckLastVertical(rowNum, isAdded);
            }

            private void CheckLastVertical(int rowNum, bool isAdded)
            {
                MazeCell lastCell = _mazeGrid[rowNum, _mazeGrid.GetLength(1) - 1];
                MazeCell preLastCell = _mazeGrid[rowNum, _mazeGrid.GetLength(1) - 2];

                if (!isAdded)
                {
                    RemoveHorizontalWall(lastCell);
                }
                /*else if (preLastCell.setID == lastCell.setID && isAdded)
                {
                    bool isRemove = Random.Range(0, 2) == 1;
                    if (isRemove)
                    {
                        RemoveHorizontalWall(lastCell);
                    }
                }*/
            }

            private void RemoveHorizontalWall(MazeCell cell)
            {
                //if(cell.idxRow != 0)
                //    cell.wallTop = false;
                cell.wallBottom = false;
                _mazeGrid[cell.idxRow + 1, cell.idxCol].wallTop = false;
            }

            private void CreateLastRow()
            {
                int lastRowNum = _mazeGrid.GetLength(0) - 1;

                for (int i = 0; i < _mazeGrid.GetLength(1) - 1; i++)
                {
                    var cell = _mazeGrid[lastRowNum, i];
                    var nextCell = _mazeGrid[lastRowNum, i + 1];

                    if (cell.setID != nextCell.setID)
                    {
                        RemoveVerticalWallBetweenCells(cell, nextCell);
                    }
                }
            }

            public void BuildMaze()
            {
                Vector2 cellSizes = _cellPrefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

                for (int idxRow = 0; idxRow < _mazeGrid.GetLength(0); idxRow++)
                {
                    for (int idxCol = 0; idxCol < _mazeGrid.GetLength(1); idxCol++)
                    {
                        GameObject cell = Instantiate(_cellPrefab, _canvas.transform);
                        cell.name = $"Maze Grid {idxRow}|{idxCol}";
                        cell.GetComponent<RectTransform>().localPosition = GetNewPositionCell(idxRow, idxCol, cellSizes);
                        MazeCell cellInfo = _mazeGrid[idxRow, idxCol];

                        if (!cellInfo.wallTop)
                            cell.transform.GetChild(0).gameObject.SetActive(false);

                        if (!cellInfo.wallRight)
                            cell.transform.GetChild(1).gameObject.SetActive(false);

                        if (!cellInfo.wallBottom)
                            cell.transform.GetChild(2).gameObject.SetActive(false);

                        if (!cellInfo.wallLeft)
                            cell.transform.GetChild(3).gameObject.SetActive(false);

                        //cell.GetComponent<TMP_Text>().text = $"{cellInfo.setID}";
                    }
                }

                SpawnExit();
            }

            private Vector3 GetNewPositionCell(int idxRow, int idxCol, Vector2 cellSizes)
            {
                Vector3 cellPos = _cellPrefab.GetComponent<RectTransform>().localPosition;

                float newX = _spawnCoods.x + (cellSizes.x * idxCol);
                float newY = _spawnCoods.y - (cellSizes.y * idxRow);

                return new Vector3(newX, newY, cellPos.z);
            }

            private void SpawnExit()
            {
                //int[] arrIdxRow = new int[2] { 0, _mazeGrid.GetLength(0) - 1 };
                int[] arrIdxCol = new int[2] { 0, _mazeGrid.GetLength(1) - 1 };

                //int randIdxRow = arrIdxRow[UnityEngine.Random.Range(0, 2)];
                int randIdxRow = UnityEngine.Random.Range(0, _mazeGrid.GetLength(0));
                int randIdxCol = arrIdxCol[UnityEngine.Random.Range(0, 2)];

                if (randIdxRow == 0 || randIdxRow == _mazeGrid.GetLength(0) - 1)
                {
                    randIdxCol = UnityEngine.Random.Range(0, _mazeGrid.GetLength(1));
                }

                _exitCoord = new Vector2(randIdxRow, randIdxCol);

                GameObject cell = GameObject.Find($"Maze Grid {randIdxRow}|{randIdxCol}");
                RemoveWallForExit(cell, randIdxRow, randIdxCol);
            }

            private void RemoveWallForExit(GameObject cell, int idxRow, int idxCol)
            {
                bool isInCorner = (idxRow == 0 || idxRow == _mazeGrid.GetLength(0) - 1) 
                    && (idxCol == 0 || idxCol == _mazeGrid.GetLength(1) - 1);
                
                if (isInCorner)
                {
                    int indexRemoveWall = idxRow == 0 ? 0 : 2;
                    bool isRemoveVerticalWall = UnityEngine.Random.Range(0, 2) == 1;

                    if (idxCol == 0 && isRemoveVerticalWall)
                    {
                        indexRemoveWall = 3;
                        _mazeGrid[idxRow, idxCol].wallLeft = false;
                    }
                    else if(idxCol == _mazeGrid.GetLength(1) - 1 && isRemoveVerticalWall)
                    {
                        indexRemoveWall = 1;
                        _mazeGrid[idxRow, idxCol].wallRight = false;
                    }
                    else if(indexRemoveWall == 0)
                    {
                        _mazeGrid[idxRow, idxCol].wallTop = false;
                    }
                    else
                    {
                        _mazeGrid[idxRow, idxCol].wallBottom = false;
                    }

                    cell.transform.GetChild(indexRemoveWall).gameObject.SetActive(false);
                }
                else
                {
                    int indexRemoveWall = -1;

                    if(idxRow == 0)
                    {
                        indexRemoveWall = 0;
                        _mazeGrid[idxRow, idxCol].wallTop = false;
                    }
                    else if(idxRow == _mazeGrid.GetLength(0) - 1)
                    {
                        indexRemoveWall = 2;
                        _mazeGrid[idxRow, idxCol].wallBottom = false;
                    }
                    else
                    {
                        if(idxCol == 0)
                        {
                            indexRemoveWall = 3;
                            _mazeGrid[idxRow, idxCol].wallLeft = false;
                        }
                        else
                        {
                            indexRemoveWall = 1;
                            _mazeGrid[idxRow, idxCol].wallRight = false;
                        }
                    }
                    
                    cell.transform.GetChild(indexRemoveWall).gameObject.SetActive(false);
                }
            }
        }
    }
}