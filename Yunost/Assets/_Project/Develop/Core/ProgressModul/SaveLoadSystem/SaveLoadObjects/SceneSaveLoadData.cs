

namespace ProgressModul
{
    class SceneSaveLoadData : SaveLoadData { 
        public string SceneName { get; private set; }
        public SceneSaveLoadData(string id, string sceneName) : base(id, new object[] { sceneName })
        {
            SceneName = sceneName;
        }
    
    }
}
