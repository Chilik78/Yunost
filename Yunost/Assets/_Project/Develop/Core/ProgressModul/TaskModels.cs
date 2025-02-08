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

        public TaskModel(string id, string name, bool isDone, SubTaskModel[] subTasks)
        {
            this.id = id;
            this.name = name;
            this.isDone = isDone;
            this.subTasks = subTasks;
        }
    }

    [Serializable]
    public class SubTaskModel
    {
        public string id;
        public string description;
        public bool isDone;

        public SubTaskModel(string id, string description, bool isDone)
        {
            this.id = id;
            this.description = description;
            this.isDone = isDone;
        }
    }
}


