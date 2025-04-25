

namespace ProgressModul
{
    class TimeControlSaveLoadData : SaveLoadData
    {
        public TimeControlSaveLoadData(string id, int currentTime, int hours, int minutes) : base(id, new object[] { currentTime, hours, minutes }) { }
    }
}
