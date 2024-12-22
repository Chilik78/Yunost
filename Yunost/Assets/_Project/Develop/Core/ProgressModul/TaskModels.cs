using System;

namespace ProgressModul
{
    [Serializable]
    public class TaskModel
    {
        public string id;
        public string name;
        public bool isDone;
        public SubTaskModel[] subTasks;
    }

    [Serializable]
    public class SubTaskModel
    {
        public string id;
        public string description;
        public bool isDone;
    }

    [Serializable]
    public class TaskArrayModel
    {
        public TaskModel[] tasks;
    }
}


