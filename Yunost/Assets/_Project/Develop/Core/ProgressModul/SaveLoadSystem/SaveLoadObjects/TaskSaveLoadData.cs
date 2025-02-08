using System.Collections.Generic;

namespace ProgressModul
{
    class TaskSaveLoadData : SaveLoadData
    {

        public TaskSaveLoadData(string id, List<TaskModel> InProgressTasks, List<TaskModel> DoneTasks) : base(id, new object[] { InProgressTasks, DoneTasks })
        {

        }
    }
}
