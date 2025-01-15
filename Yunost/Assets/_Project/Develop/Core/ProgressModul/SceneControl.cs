using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ProgressModul
{
    public enum Scenes
    {
        Menu,
        CampStation,
        MainCamp,
        HubHome,
    }

    public class SceneControl : ISaveLoadObject
    {
        private Dictionary<int, string> _sceneDictionary = new Dictionary<int, string>()
        {
            {0, "Menu" },
            {1, "CampStation" },
            {2,  "MainCamp"},
            {3,  "HubHome"}
        };


        private string _initSceneName;
        private string _lastSceneName;

        public string ComponentSaveId => "SceneControl";

        public delegate void ProgressLoadingHandler(float progress);
        public delegate void LoadingHandler();
        public event ProgressLoadingHandler ProgressLoading;
        public event LoadingHandler StartLoading;
        public event LoadingHandler StoptLoading;

        public SceneControl(string initSceneName)
        {
            _initSceneName = initSceneName; 
        }

        public SceneControl(int initSceneIndex)
        {
            _initSceneName = _sceneDictionary[initSceneIndex];
        }

        public SceneControl(Scenes initScene)
        {
            _initSceneName = _sceneDictionary[(int)initScene];
        }

        public void Init ()
        {
            _lastSceneName = _initSceneName;
            SceneManager.LoadScene(_initSceneName);
        }

        public void InitLast()
        {
            SceneManager.LoadScene(_lastSceneName);
        }

        public void OpenMenu()
        {
            var prev = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Menu");
        }

        public void NextScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index+1);
        }

        public void PreviousScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index - 1);
        }

        public AsyncOperation GoToSceneAsync(int index)
        {
            Debug.Log($"Переход на сцену: {index}");
            return SceneManager.LoadSceneAsync(index);
        }

        public AsyncOperation GoToSceneAsync(string name)
        {
            Debug.Log($"Переход на сцену: {name}");
            return SceneManager.LoadSceneAsync(name);
        }

        public AsyncOperation GoToSceneAsync(Scenes scene)
        {
            Debug.Log($"Переход на сцену: {scene}");
            return SceneManager.LoadSceneAsync((int)scene);
        }

        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void GoToScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void GoToScene(Scenes scene)
        {
            SceneManager.LoadScene((int)scene);
        }

        public AsyncOperation UnloadSceneAsync(int index)
        {
            return SceneManager.UnloadSceneAsync(index);
        }

        public AsyncOperation UnloadSceneAsync(string name)
        {
            return SceneManager.UnloadSceneAsync(name);
        }

        private IEnumerator _loadNewSceneAsync(AsyncOperation loadSceneOp)
        {
            if (StartLoading != null)
                StartLoading();
            yield return null;

            loadSceneOp.allowSceneActivation = false;
            Debug.Log(loadSceneOp.isDone);
            while (!loadSceneOp.isDone)
            {
                if (ProgressLoading != null)
                    ProgressLoading(loadSceneOp.progress / 0.9f);
                Debug.Log(loadSceneOp.progress / 0.9f);
                if (loadSceneOp.progress >= 0.9f)
                {
                    Debug.Log(loadSceneOp.progress / 0.9f);

                    loadSceneOp.allowSceneActivation = true;

                    if (StoptLoading != null)
                        StoptLoading();

                    break;
                }
                Debug.Log(loadSceneOp.isDone);
                yield return null;
            }
        }

        public IEnumerator LoadNewSceneAsync(int index)
        {
            var loadSceneOp = GoToSceneAsync(index);
            _lastSceneName = _sceneDictionary[index];
            return _loadNewSceneAsync(loadSceneOp);
        }

        public IEnumerator LoadNewSceneAsync(string name)
        {
            var loadSceneOp = GoToSceneAsync(name);
            _lastSceneName = name;
            return _loadNewSceneAsync(loadSceneOp);
        }

        public IEnumerator LoadNewSceneAsync(Scenes scene)
        {
            var loadSceneOp = GoToSceneAsync(scene);
            _lastSceneName = _sceneDictionary[(int)scene];
            return _loadNewSceneAsync(loadSceneOp);
        }

        SaveLoadData ISaveLoadObject.GetSaveLoadData()
        {
            return new SceneSaveLoadData(ComponentSaveId, _lastSceneName);
        }

        void ISaveLoadObject.RestoreValues(SaveLoadData loadData)
        {
            if (loadData?.Data == null || loadData.Data.Length < 1)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (field)

            _lastSceneName = loadData.Data[0].ToString();
        }

        void ISaveLoadObject.SetDefault()
        {
            _initSceneName = _sceneDictionary[(int)Scenes.CampStation];
        }
    }
}
