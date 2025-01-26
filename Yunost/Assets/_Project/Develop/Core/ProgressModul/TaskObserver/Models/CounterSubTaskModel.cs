using System;

namespace ProgressModul
{
    [Serializable]
    public class CounterSubTaskModel : SubTaskModel
    {
        public readonly int finalCount;
        public readonly int currentCount;
        public CounterSubTaskModel(string id, string description, string flow, int stackIndex, int finalCount, int currentCount, bool isDone = false, string[] friends = null) : 
            base(id, description, flow, stackIndex, isDone, friends)
        {
            this.finalCount = finalCount;
            this.currentCount = currentCount;
        }

        public override string ToString()
        {
            return base.ToString() + " : " + $"{finalCount} : {currentCount}";
        }
    }
}
