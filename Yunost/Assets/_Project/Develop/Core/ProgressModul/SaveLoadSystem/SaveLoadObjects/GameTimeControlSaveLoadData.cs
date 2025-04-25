

namespace ProgressModul
{
    class GameTimeControlSaveLoadData : SaveLoadData
    {
        public GameTimeControlSaveLoadData(string id, int mainTime, int sideTime) : base(id, new object[] {mainTime, sideTime }) { }
    }
}
