

using System.Collections.Generic;
using System.Linq;

namespace ProgressModul
{
    public class Task
    {
        private TaskModel _model;
        private List<SubTask> _subTasks;

        public Task(TaskModel model)
        {
            _model = model;
            _subTasks = model.SubTasks.Select(m => typeof(CounterSubTaskModel) == m.GetType() ? new CounterSubTask((CounterSubTaskModel)m) : new SubTask(m)).ToList();
        }

        public TaskModel GetModel { get {
                _model.SubTasks = _subTasks.Select(s => s.GetModel).ToArray();
                return _model;
            }
        }

        public string Flow
        {
            get => _model.Flow;
            set => _model.Flow = value;
        }

        public string Id
        {
            get => _model.Id;
        }

        public string Name 
        {
            get => _model.Name;
        }

        public TaskState State
        {
            get => _model.State;
            set => _model.State = value;
        }

        public TaskType Type
        {
            get => _model.Type;
        }

        public int StartTime
        {
            get => _model.StartTime;
        }

        public int DeadTime
        {
            get => _model.DeadTime;
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
