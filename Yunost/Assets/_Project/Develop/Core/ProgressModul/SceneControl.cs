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

        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void GoToScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void UnloadScene(int index)
        {
            SceneManager.UnloadSceneAsync(index);
        }

        public void UnloadScene(string name)
        {
            SceneManager.UnloadSceneAsync(name);
        }
    }
}
