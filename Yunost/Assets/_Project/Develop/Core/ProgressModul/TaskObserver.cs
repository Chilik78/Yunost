
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgressModul
{
    public class TaskObserver
    {
        private List<Task> _inProgressTasks;
        private List<Task> _doneTasks;
        public delegate void DoneTaskHandler(Task task);
        public event DoneTaskHandler HaveDoneTask;

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

        public List<Task> GetDoneTasks
        {
            get => _doneTasks;
        }

        public void setDoneTask(Task task)
        {
            task.setDone();
            _inProgressTasks.Remove(task);
            _doneTasks.Add(task);
            HaveDoneTask(task);
        }

        public Task GetLastDoneTask
        {
            get => _doneTasks.Last();
        }
    }
}


