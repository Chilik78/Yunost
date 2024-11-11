using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProgressModul
{
    public class SceneControl
    {
        private string _initSceneName;
        private int _initSceneIndex;

        public SceneControl(string initSceneName)
        {
            _initSceneName = initSceneName; 
        }

        public SceneControl(int initSceneIndex)
        {
            _initSceneIndex = initSceneIndex;
        }

        void Init ()
        {
            if (_initSceneName == null)
                SceneManager.LoadScene(_initSceneIndex);
            else
                SceneManager.LoadScene(_initSceneName);
        }

        public void OpenMenu()
        {
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

        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void GoToScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
