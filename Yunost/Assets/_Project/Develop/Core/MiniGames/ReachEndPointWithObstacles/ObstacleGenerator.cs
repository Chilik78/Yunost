using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGames
{
    namespace ReachEndPointWithObstacles
    {
        public enum WallType
        {
            Default,
            Small,
            Medium,
            Big
        }

        public class ObstacleCell
        {
            public int idxRow;
            public int idxCol;

            public bool wallLeft = true;
            public bool wallBottom = true;

            public WallType typeLeftWall = WallType.Default;
            public WallType typeBottomWall = WallType.Default;

            public bool isVisited = false;
            public bool isDangerous = false;
        }

        public class ObstacleGenerator : MonoBehaviour
        {
            [SerializeField, Header("Префаб препятствия")]
            private GameObject _obstaclePrefab;

            private GameObject _canvas;
            private ObstacleCell[,] _cells;

            public ObstacleCell[,] GetObstacleCells {  get { return _cells; } } 

            public void GenerateObstacle(Vector2 gridSizes, Vector3 cellSizes, int countDangerousCells, Color dangerousColor, int countEndpoints)
            {
                _canvas = GameObject.Find("Canvas MiniGame");
                _cells = GetCells(gridSizes);
                RemoveWallsWithBacktracker(_cells);
                DoFilterCells(_cells, countEndpoints);    
                ChooseDangerousCells(_cells, countDangerousCells);
                GenerateCells(_cells, cellSizes, dangerousColor);
            }

            private ObstacleCell[,] GetCells(Vector2 gridSizes)
            {
                ObstacleCell[,] cells = new ObstacleCell[(int)gridSizes.x, (int)gridSizes.y];

                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                {
                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                    {
                        cells[idxRow, idxCol] = new ObstacleCell { idxRow = idxRow, idxCol = idxCol };
                    }
                }

                return cells;
            }

            #region RemoveWallsWithBacktracker
            private void RemoveWallsWithBacktracker(ObstacleCell[,] cells)
            {
                ObstacleCell currCell = cells[0,0];
                currCell.isVisited = true;

                Stack<ObstacleCell> stackCells = new Stack<ObstacleCell>();
                List<ObstacleCell> unvisitedNeighbours = new List<ObstacleCell>();

                do
                {
                    int currIdxRow = currCell.idxRow;
                    int currIdxCol = currCell.idxCol;

                    if (currIdxRow > 0 && !cells[currIdxRow - 1, currIdxCol].isVisited)
                        unvisitedNeighbours.Add(cells[currIdxRow - 1, currIdxCol]);

                    if (currIdxCol > 0 && !cells[currIdxRow, currIdxCol - 1].isVisited)
                        unvisitedNeighbours.Add(cells[currIdxRow, currIdxCol - 1]);

                    if (currIdxRow < cells.GetLength(0) - 2 && !cells[currIdxRow + 1, currIdxCol].isVisited)
                        unvisitedNeighbours.Add(cells[currIdxRow + 1, currIdxCol]);
                    

                    if (currIdxCol < cells.GetLength(1) - 2 && !cells[currIdxRow, currIdxCol + 1].isVisited)
                        unvisitedNeighbours.Add(cells[currIdxRow, currIdxCol + 1]);
                    

                    if (unvisitedNeighbours.Count > 0)
                    {
                        ObstacleCell choosenCell = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                        RemoveWall(currCell, choosenCell);
                        choosenCell.isVisited = true;
                        currCell = choosenCell;
                        stackCells.Push(choosenCell);
                        unvisitedNeighbours.Clear();
                    }
                    else
                    {
                        currCell = stackCells.Pop();
                    }

                } while (stackCells.Count > 0);
            }

            private void RemoveWall(ObstacleCell currCell, ObstacleCell neighbourCell)
            {
                if (currCell.idxCol == neighbourCell.idxCol)
                {
                    if (currCell.idxRow > neighbourCell.idxRow) 
                        currCell.wallBottom = false;
                    else
                        neighbourCell.wallBottom = false;
                }
                else
                {
                    if (currCell.idxCol > neighbourCell.idxCol)
                        currCell.wallLeft = false;
                    else
                        neighbourCell.wallLeft = false;
                }
            }
            #endregion

            #region FilterCells
            /// <summary>
            /// Выполняет фильтрование ячеек. На данный момент удаляет ячейки на границе (по краям)
            /// </summary>
            private void DoFilterCells(ObstacleCell[,] cells, int countEndpoints)
            {
                RemoveBoundaryWalls(cells);
                CreatingEmptyCells(cells, countEndpoints);
            }

            private void RemoveBoundaryWalls(ObstacleCell[,] cells)
            {
                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                {
                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                    {
                        if (idxRow == 0 || idxRow + 1 == cells.GetLength(0))
                            cells[idxRow, idxCol].wallBottom = false;

                        if (idxCol == 0 || idxCol + 1 == cells.GetLength(1))
                            cells[idxRow, idxCol].wallLeft = false;
                    }
                }
            }

            /// <summary>
            /// Выполняет гарантию того, что у нас будет минимум 3 свободной ячейки для спавна эндпоинтов и 1 ячейка для спавна игрока
            /// </summary>
            private void CreatingEmptyCells(ObstacleCell[,] cells, int countEndpoints)
            {
                int neededCountEmptyCells = countEndpoints + 1;
                int countEmptyCells = GetCountEmptyCells(cells);
                bool isBreak = false;

                while (countEmptyCells < neededCountEmptyCells)
                {
                    if(isBreak)
                        break;

                    for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                    {
                        if(isBreak)
                            break;

                        for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                        {
                            if(UnityEngine.Random.Range(0,2) == 1)
                            {
                                cells[idxRow, idxCol].wallLeft = false;
                                cells[idxRow, idxCol].wallBottom = false;
                                countEmptyCells++;
                            }

                            if(countEmptyCells >= neededCountEmptyCells)
                            {
                                isBreak = true;
                                break;
                            }
                        }
                    }
                }
            }

            private int GetCountEmptyCells(ObstacleCell[,] cells)
            {
                int countEmptyCells = 0;

                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                {
                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                    {
                        if (!cells[idxRow, idxCol].wallLeft && !cells[idxRow, idxCol].wallBottom)
                        {
                            countEmptyCells++;
                        }
                    }
                }

                return countEmptyCells;
            }
            #endregion

            private void ChooseDangerousCells(ObstacleCell[,] cells, int countDangerousCells)
            {
                int countChoosenDangerousCells = 0;
                
                while (countChoosenDangerousCells != countDangerousCells)
                {
                    for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                    {
                        for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                        {
                            if (countChoosenDangerousCells == countDangerousCells)
                                break;   

                            bool isDangerousCell = UnityEngine.Random.Range(0, 2) == 1;
                            
                            if(isDangerousCell && (cells[idxRow, idxCol].wallBottom || cells[idxRow, idxCol].wallLeft))
                            {
                                countChoosenDangerousCells++;
                                cells[idxRow, idxCol].isDangerous = isDangerousCell;
                            }
                        }
                    }
                }
            }

            #region GenerateCells
            private void GenerateCells(ObstacleCell[,] cells, Vector3 cellSizes, Color dangerousColor) 
            {
                for (int idxRow = 0; idxRow < cells.GetLength(0); idxRow++)
                {
                    for (int idxCol = 0; idxCol < cells.GetLength(1); idxCol++)
                    {
                        GameObject cell = Instantiate(_obstaclePrefab, _canvas.transform);
                        cell.GetComponent<RectTransform>().localPosition = GetNewPositionCell(idxRow, idxCol, cellSizes);
                        cell.name = $"Obstacle {idxRow}|{idxCol}";
                        ChangeCell(cell, cells[idxRow, idxCol], cellSizes, dangerousColor);
                    }
                }
            }

            private Vector3 GetNewPositionCell(int idxRow, int idxCol, Vector3 cellSizes)
            {
                Vector3 obstaclePos = _obstaclePrefab.GetComponent<RectTransform>().localPosition;

                float newX = obstaclePos.x + (cellSizes.x * idxCol);
                float newY = obstaclePos.y + (cellSizes.y * idxRow);

                return new Vector3(newX, newY, obstaclePos.z);
            }

            #region ChangeCell
            private void ChangeCell(GameObject gameObjCell, ObstacleCell cellInfo, Vector3 cellSizes, Color dangerousColor)
            {
                RemoveWalls(gameObjCell, cellInfo);
                ChangeColor(gameObjCell, cellInfo, dangerousColor);
                ChangeSizes(gameObjCell, cellInfo, cellSizes);

                if (cellInfo.isDangerous)
                {
                    for(int i=0; i<gameObjCell.transform.childCount; i++)
                    {
                        gameObjCell.transform.GetChild(i).gameObject.tag = "MiniGame ReachEndPointWithObstacles Dangerous Obstacle";
                    }
                }    
            }

            private void RemoveWalls(GameObject gameObjCell, ObstacleCell cellInfo)
            {
                if (!cellInfo.wallBottom)
                    gameObjCell.transform.GetChild(1).gameObject.SetActive(false);

                if (!cellInfo.wallLeft)
                    gameObjCell.transform.GetChild(0).gameObject.SetActive(false);
            }

            private void ChangeColor(GameObject gameObjCell, ObstacleCell cellInfo, Color dangerousColor)
            {
                if(cellInfo.isDangerous)
                {
                    for(int i = 0; i < gameObjCell.transform.childCount; i++)
                    {
                        GameObject child = gameObjCell.transform.GetChild(i).gameObject; 
                        child.GetComponent<Renderer>().material.color = dangerousColor;
                    }  
                }
            }

            private void ChangeSizes(GameObject gameObjCell, ObstacleCell cellInfo, Vector3 cellSizes)
            {
                if(cellInfo.wallLeft)
                    cellInfo.typeLeftWall = GetRandomTypeWall();
                
                if(cellInfo.wallBottom)
                    cellInfo.typeBottomWall = GetRandomTypeWall();

                for (int i = 0; i < gameObjCell.transform.childCount; i++)
                {
                    bool isLeftWall = i == 0;
                    GameObject wall = gameObjCell.transform.GetChild(i).gameObject; 
                    WallType wallType = isLeftWall ? cellInfo.typeLeftWall : cellInfo.typeBottomWall;
                    ChangeSizeWall(wall, wallType, cellSizes);
                }
            }

            private WallType GetRandomTypeWall()
            {
                int randomIdx = UnityEngine.Random.Range(0,4);

                switch (randomIdx)
                {
                    case 1: return WallType.Small;
                    case 2: return WallType.Big;
                    case 3: return WallType.Medium; 
                    default: return WallType.Default;
                }
            }

            private void ChangeSizeWall(GameObject wall, WallType typeWall, Vector3 cellSizes)
            {
                RectTransform rectWall = wall.GetComponent<RectTransform>();
                Vector3 currentScale = rectWall.localScale; 

                if (typeWall == WallType.Medium)
                {
                    float diff = cellSizes.y / 4;
                    Vector3 newSize = new Vector3(currentScale.x, currentScale.y - (2 * diff), currentScale.z);
                    rectWall.localScale = newSize;
                }
                else if (typeWall == WallType.Big)
                {
                    float diffX = cellSizes.x / 4;
                    float diffY = cellSizes.y / 4;
                    Vector3 newSize = new Vector3(currentScale.x + diffX, currentScale.y + diffY, currentScale.z);
                    rectWall.localScale = newSize;
                }
                else if(typeWall == WallType.Small)
                {
                    float diff = cellSizes.y / 4;
                    Vector3 newSize = new Vector3(currentScale.x, currentScale.y - (3 * diff), currentScale.z);
                    rectWall.localScale = newSize;
                }
            }
            #endregion
            #endregion
        }
    }
}