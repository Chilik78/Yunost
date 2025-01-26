

using System.Collections.Generic;
using System.Linq;

namespace ProgressModul
{
    public class Task
    {
        private string _id;
        private string _name;
        private string _flow;
        private List<SubTask> _subTasks;
        private TaskState _state;
        private TaskType _type;

        public Task(TaskModel model)
        {
            _name = model.name;
            _subTasks = model.subTasks.Select(m => new SubTask(m)).ToList();
            _id = model.id;
            _flow = model.flow;
            _state = model.state;
            _type = model.type;
        }

        public TaskModel GetModel { get {
                SubTaskModel[] subTaskModels = _subTasks.Select(s => s.GetModel).ToArray();
                return new TaskModel(_id, _name, _flow, _state, _type, subTaskModels);
            }
        }

        public string Flow
        {
            get => _flow;
            set => _flow = value;
        }

        public string Id
        {
            get => _id;
        }

        public string Name 
        {
            get => _name;
        }

        public TaskState State
        {
            get => _state;
            set => _state = value;
        }

        public TaskType Type
        {
            get => _type;
        }

        public IEnumerable<SubTask> SubTasks => _subTasks;

        public IEnumerable<SubTask> CurrentSubTasks => _subTasks.Where(s => s.StackIndex == 0 && s.Flow == Flow && !s.IsDone);

        public List<SubTask> SubTasksTest => _subTasks;

        public override string ToString()
        {
            return GetModel.ToString();
        }
    }
}
