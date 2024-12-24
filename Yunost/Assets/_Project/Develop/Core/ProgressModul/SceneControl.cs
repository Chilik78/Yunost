using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ProgressModul
{
    public enum Scenes
    {
        Menu,
        CampStation,
        MainCamp,
        MyHome,
    }

    public class SceneControl
    {
        private string _initSceneName;
        private int _initSceneIndex;
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
            _initSceneIndex = initSceneIndex;
        }

        public void Init ()
        {
            if (_initSceneName == null)
                SceneManager.LoadScene(_initSceneIndex);
            else
                SceneManager.LoadScene(_initSceneName);
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
            return SceneManager.LoadSceneAsync(index);
        }

        public AsyncOperation GoToSceneAsync(string name)
        {
            return SceneManager.LoadSceneAsync(name);
        }

        public AsyncOperation GoToSceneAsync(Scenes scene)
        {
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

            loadSceneOp.allowSceneActivation = false;
            while (!loadSceneOp.isDone)
            {
                if (ProgressLoading != null)
                    ProgressLoading(loadSceneOp.progress / 0.9f);

                if (loadSceneOp.progress >= 0.9f)
                {
                    break;
                }
                yield return null;
            }
            loadSceneOp.allowSceneActivation = true;
            
            if (StoptLoading != null)
                StoptLoading();
        }

        public IEnumerator LoadNewSceneAsync(int index)
        {
            var loadSceneOp = GoToSceneAsync(index);
            return _loadNewSceneAsync(loadSceneOp);
        }

        public IEnumerator LoadNewSceneAsync(string name)
        {
            var loadSceneOp = GoToSceneAsync(name);
            return _loadNewSceneAsync(loadSceneOp);
        }

        public IEnumerator LoadNewSceneAsync(Scenes scene)
        {
            var loadSceneOp = GoToSceneAsync(scene);
            return _loadNewSceneAsync(loadSceneOp);
        }
    }
}
