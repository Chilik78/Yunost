using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ProgressModul
{
    public enum Tiles
    {
        CampStation,
        MainCamp,
        HubHome,
    }

    public class SceneControl : ISaveLoadObject
    {
        private Dictionary<int, string> _tileDictionary = new Dictionary<int, string>()
        {
            {0, "CampStation" },
            {1,  "MainCamp"},
            {2,  "HubHome"}
        };


        private string _initSceneName = "Main";
        private string _currentTile;
        public string CurrentTile => _currentTile;
        public IEnumerable<string> GetTileNames => _tileDictionary.Values;
        public string ComponentSaveId => "SceneControl";

        public bool IsNewGame { get; private set; } = true;

        public delegate void TileHandler(string newTile, string prevTile);
        public event TileHandler TileChanged;
        public void Init ()
        {
            Debug.LogWarning(_currentTile);
            SceneManager.LoadScene(_initSceneName);
            IsNewGame = true;
        }

        public void Load()
        {
            SceneManager.LoadScene(_initSceneName);
        }

        public void OpenMenu()
        {
            var prev = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Menu");
        }

        public void ChangeTile(string tile)
        {
            if (!_tileDictionary.ContainsValue(tile))
            {
                Debug.LogError($"Нет такого тайла: {tile}");
            }

            if(TileChanged != null)
            {
                TileChanged(tile, _currentTile);
            }

            _currentTile = tile;
        }
        
        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void GoToScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void GoToScene(Tiles scene)
        {
            SceneManager.LoadScene((int)scene);
        }

       

       
        SaveLoadData ISaveLoadObject.GetSaveLoadData()
        {
            return new SceneSaveLoadData(ComponentSaveId, _currentTile);
        }

        void ISaveLoadObject.RestoreValues(SaveLoadData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 1)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (field)

            _currentTile = loadData.Data[0].ToString();
        }

        void ISaveLoadObject.SetDefault()
        {
            _currentTile = _tileDictionary[(int)Tiles.CampStation];
        }
    }
}
