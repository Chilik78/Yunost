using System;
using System.Dynamic;
using System.Linq;

namespace ProgressModul
{
    public enum TaskType
    {
        Main,
        Side
    }

    public enum TaskState
    {
        InProgress,
        Done,
        Failed
    }

    [Serializable]
    public class TaskModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Flow { get; set;  }
        public SubTaskModel[] SubTasks { get; private set; }
        public TaskType Type { get; private set; }
        public TaskState State { get; set; }
        public int StartTime { get; private set; }
        public int DeadTime { get; private set; }

        public TaskModel(string id, string name, string flow, TaskState state, TaskType type, SubTaskModel[] subTasks, int startTime, int deadTime)
        {
            Id = id;
            Name = name;
            Flow = flow;
            State = state;
            Type = type;
            SubTasks = subTasks;
            StartTime = startTime;
            DeadTime = deadTime;
        }

        public override string ToString()
        {
            return $"{Id} : {Name} : {Flow} : {State} : {Type} : {StartTime} : {DeadTime} : \n[{(SubTasks == null ? null : string.Join("\n", SubTasks.Select(s => s.ToString())))}]";
        }
    }
}
