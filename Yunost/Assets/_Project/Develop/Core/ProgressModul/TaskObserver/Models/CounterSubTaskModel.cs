using System;

namespace ProgressModul
{
    [Serializable]
    public class CounterSubTaskModel : SubTaskModel
    {
        public int FinalCount { get; private set; }
        public int CurrentCount { get; set; }
        public CounterSubTaskModel(string id, string description, string flow, int stackIndex, int finalCount, int currentCount = 0, bool isDone = false, string[] friends = null) : 
            base(id, description, flow, stackIndex, isDone, friends)
        {
            FinalCount = finalCount;
            CurrentCount = currentCount;
        }

        public override string ToString()
        {
            return base.ToString() + " : " + $"{FinalCount} : {CurrentCount}";
        }
    }
}
