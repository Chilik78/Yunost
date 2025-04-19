using MiniGames.Maze;
using MiniGames.ReachEndPointWithObstacles;
using UnityEngine;

namespace MiniGames
{
    /// <summary>
    /// Мини-игра сделана с процедурной генерацией лабиринта алгоритмом Эллера (для создания "идеальных" лабиринтов)
    /// </summary>
    
    public class MazeMiniGame : MiniGame
    {
        private MazeGenerator _mazeGenerator;
        private Maze.PlayerController _playerController;
        private Vector2 _gridSize; // Менять по сложности
        private Vector2 _spawnCoods; // Менять по сложности
        private const float _widthLine = 0.2f;


        public override void Init(MiniGameContext context)
        {
            InitVariablesByDifficult(context.getCurrentDifficult);

            _mazeGenerator = GameObject.Find("Paper MiniGame").GetComponent<MazeGenerator>();
            _mazeGenerator.Init(_gridSize, _spawnCoods);
            
            _playerController = GameObject.Find("Player MiniGame").GetComponent<Maze.PlayerController>();
            _playerController.Init(_mazeGenerator.GetMaze, _widthLine);
            _playerController.OnExit += FinishGame;
            BuildUI();
        }
        private void InitVariablesByDifficult(TypeDifficultMiniGames difficult)
        {
            switch (difficult)
            {
                case TypeDifficultMiniGames.Easy:
                    _gridSize = new Vector2(6f, 6f);
                    _spawnCoods = new Vector2(-250f, 367f);
                    break;
                case TypeDifficultMiniGames.Medium:
                    _gridSize = new Vector2(10f, 10f);
                    _spawnCoods = new Vector2(-450f, 460f);
                    break;
                case TypeDifficultMiniGames.Hard:
                    _gridSize = new Vector2(10f, 15f);
                    _spawnCoods = new Vector2(-650f, 460f);
                    break;
            }
        }

        protected override void BuildUI()
        {
            _mazeGenerator.BuildMaze();  
            _playerController.SpawnPlayer(_mazeGenerator.GetExitCoord);
        }

        public override void TrackingProgressGameOnUpdate()
        {
            _playerController.Move();
        }

        private void FinishGame(MiniGameResultInfo result)
        {
            _playerController.OnExit -= FinishGame;
            CalculateResult(result);
        }
    }
}