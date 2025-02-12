using System;

namespace ProgressModul
{
    [Serializable]
    public class SubTaskModel
    {
        public readonly string id;
        public readonly string description;
        public readonly bool isDone;
        public readonly string flow;
        public readonly int stackIndex;
        public readonly string[] friends;
        public readonly string type;

        public SubTaskModel(string id, string description, string flow, int stackIndex, bool isDone = false, string[] friends = null, string type = "base")
        {
            this.id = id;
            this.description = description;
            this.isDone = isDone;
            this.flow = flow;
            this.stackIndex = stackIndex;
            this.friends = friends;
            this.type = type;
        }

        public override string ToString()
        {
            return $"{id} : {description} : {isDone} : {flow} : {stackIndex} : [{(friends == null ? null : string.Join(", ", friends))}]";
        }
    }
}
