using System;

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
        public readonly string id;
        public readonly string name;
        public readonly string flow;
        public readonly SubTaskModel[] subTasks;
        public readonly TaskType type;
        public readonly TaskState state;

        public TaskModel(string id, string name, string flow, TaskState state, TaskType type, SubTaskModel[] subTasks)
        {
            this.id = id;   
            this.name = name;
            this.flow = flow;
            this.state = state;
            this.type = type;
            this.subTasks = subTasks;
        }
    }
}
