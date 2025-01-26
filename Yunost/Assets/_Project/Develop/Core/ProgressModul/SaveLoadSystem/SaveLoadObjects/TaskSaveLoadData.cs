using System.Collections.Generic;

namespace ProgressModul
{
    class TaskSaveLoadData : SaveLoadData
    {

        public TaskSaveLoadData(string id, List<TaskModel> Tasks) : base(id, new object[] { Tasks })
        {

        }
    }
}
