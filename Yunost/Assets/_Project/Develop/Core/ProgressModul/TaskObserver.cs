
using Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressModul
{
    public class TaskObserver
    {
        private List<Task> _inProgressTasks;
        private List<Task> _doneTasks;
        public delegate void TaskHandler(Task task);
        public event TaskHandler HaveDoneTask;
        public event TaskHandler HaveNewTask;
        public event TaskHandler HaveNewSubTask;

        static public List<Task> ParseJsonWithTasks(string json)
        {
            TaskModel[] taskModels = JsonHelper.FromJson<TaskModel>(json);
            List<Task> tasks = new();
            foreach (var model in taskModels)
            {
                tasks.Add(new Task(model));
            }
            return tasks;
        }

        public TaskObserver(string json)
        {
            _inProgressTasks = ParseJsonWithTasks(json);
            _doneTasks = new();
        }

        public TaskObserver(List<Task> initInProgressTasks, List<Task> initDoneTasks)
        {
            _inProgressTasks = initInProgressTasks;
            _doneTasks = initDoneTasks;
        }

        public TaskObserver(List<Task> initInProgressTasks)
        {
            _inProgressTasks = initInProgressTasks;
            _doneTasks = new();
        }

        public List<Task> GetInProgressTasks
        {
            get => _inProgressTasks;
        }

        public Task GetFirstInProgressTask 
        {
            get
            {
                if (_inProgressTasks.Count() > 0) return _inProgressTasks.First();
                return null;
            }
        }

        public List<Task> GetDoneTasks
        {
            get => _doneTasks;
        }

        public void SetDoneNextFirstTaskSubTask(Task task)
        {
            bool isAllDone = task.SetDoneFirstSubTask();

            if (isAllDone)
            {
                SetDoneTask(task);
            }
            else
            {
                if (HaveNewSubTask != null) HaveNewSubTask(task);
            }
        }

        public void SetDoneNextFirstTaskSubTask()
        {
            Task task = GetFirstInProgressTask;
            SetDoneNextFirstTaskSubTask(task);
        }
        public void SetDoneTask(Task task)
        {
            if (task == null) return;
            

            task.SetDone();
            _inProgressTasks.Remove(task);
            _doneTasks.Add(task);

            if(HaveDoneTask != null)
                HaveDoneTask(task);
            
            if(HaveNewTask != null) 
                HaveNewTask(GetFirstInProgressTask);
        }

        public void SetDoneFirstTask()
        {
            SetDoneTask(GetFirstInProgressTask);
        }

        public void SetDoneTaskById(string id)
        {
            Task task = _inProgressTasks.Where(t => t.Id == id).First();
            if(task != null)
            {
                SetDoneTask(task);
            }
        }

        public Task GetLastDoneTask
        {
            get => _doneTasks.Last();
        }
    }
}


