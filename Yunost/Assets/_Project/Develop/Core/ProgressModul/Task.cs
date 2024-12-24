

using Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgressModul
{
    public class SubTask
    {
        private string _id;
        private string _description;
        private bool _isDone;

        public SubTask(SubTaskModel model)
        {
            _id = model.id;
            _isDone = model.isDone;
            _description = model.description;
        }

        public void SetDone()
        {
            _isDone = true;
        }

        public string Id
        {
            get => _id;
        }

        public bool isDone
        {
            get => _isDone;
        }

        public string Description
        {
            get => _description;
        }
    }

    public class Task
    {
        private string _id;
        private string _name;
        private List<SubTask> _subTasks;
        private List<SubTask> _inProgressSubTasks;
        private List<SubTask> _doneSubTasks;
        private bool _isDone = false;

        public Task(TaskModel model)
        {
            _name = model.name;
            _subTasks = model.subTasks.Select(m => new SubTask(m)).ToList();
            _inProgressSubTasks = _subTasks.Where(m => !m.isDone).ToList();
            _doneSubTasks = _subTasks.Where(m => m.isDone).ToList();
            _id = model.id;
            _isDone = model.isDone;
        }

        public Task(string name, string id) {
            _name = name;
            _id = id;
        }

        public void SetDone()
        {
            _isDone = true;
            foreach (var subtask in _subTasks)
            {
                subtask.SetDone();
                _inProgressSubTasks.Clear();
                _doneSubTasks = _subTasks;
            }
        }

        public List<SubTask> GetInProgressSubTasks
        {
            get => _inProgressSubTasks;
        }

        public SubTask GetFirstInProgressSubTask
        {
            get
            {
                if (_inProgressSubTasks.Count() > 0) return _inProgressSubTasks.First();
                return null;
            }
        }

        public List<SubTask> GetDoneSubTasks
        {
            get => _doneSubTasks;
        }

        public bool SetDoneSubTask(SubTask subTask)
        {
            if (subTask == null) return true;

            subTask.SetDone();
            _inProgressSubTasks.Remove(subTask);
            _doneSubTasks.Add(subTask);

            return _subTasks.Count == _doneSubTasks.Count;
        }

        public bool SetDoneFirstSubTask()
        {
            return SetDoneSubTask(GetFirstInProgressSubTask);
        }

        public bool SetDoneSubTaskById(string id)
        {
            SubTask task = _inProgressSubTasks.Where(t => t.Id == id).First();
            if (task != null)
            {
                return SetDoneSubTask(task);
            }
            return false;
        }

        public SubTask GetLastDoneSubTask
        {
            get => _doneSubTasks.Last();
        }

        public string Id
        {
            get => _id;
        }

        public string Name 
        {
            get => _name;
        }

        public bool isDone
        {
            get => _isDone;
        }

        public string Description
        {
            get => GetFirstInProgressSubTask.Description;
        }
    }
}
