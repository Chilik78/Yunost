
using Global;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgressModul.Test;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ProgressModul
{
    public class TaskObserver : ISaveLoadObject
    {
        private List<Task> _inProgressTasks = new(1);
        private List<Task> _doneTasks = new(1);
        public delegate void TaskHandler(Task task);
        public event TaskHandler HaveDoneTask;
        public event TaskHandler HaveNewTask;
        public event TaskHandler HaveNewSubTask;
        public string ComponentSaveId => "TaskObserver";

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

        public TaskObserver()
        {

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

        public void SetDoneSubTaskByIds(string taskId, string subTaskId)
        {
            Task task = _inProgressTasks.Where(t => t.Id == taskId).First();
            if(task == null) return;

            bool isAllDone = task.SetDoneSubTaskById(subTaskId);
            if (isAllDone)
            {
                SetDoneTask(task);
            }
            else
            {
                if (HaveNewSubTask != null) HaveNewSubTask(task);
            }
        }

        public SaveLoadData GetSaveLoadData()
        {
            return new TaskSaveLoadData(ComponentSaveId, _inProgressTasks.Select(x => x.GetModel).ToList(), _doneTasks.Select(x => x.GetModel).ToList());
        }

        public void RestoreValues(SaveLoadData loadData)
        {
            _inProgressTasks.Clear();
            _doneTasks.Clear();

            if (loadData?.Data == null || loadData.Data.Length < 2)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (JArray) with items
            // [1] - (JArray) with items

            var items = ((JArray)loadData.Data[0]).ToObject<List<TaskModel>>();
            _inProgressTasks.AddRange(items.Select(x => new Task(x)));

            var items2 = ((JArray)loadData.Data[1]).ToObject<List<TaskModel>>();
            _doneTasks.AddRange(items2.Select(x => new Task(x)));
        }

        private const string SaveFileName = "Tasks.json";
        private static string SaveDataFolder = Application.streamingAssetsPath;
        public static string SaveFilePath => Path.Combine(SaveDataFolder, SaveFileName);

        public void SaveTasks()
        {
            var saveLoadData = GetSaveLoadData();
            var serializedSaveFile = JsonConvert.SerializeObject(saveLoadData);
            Debug.Log(serializedSaveFile.ToString());

            //todo: make async
            File.WriteAllText(SaveFilePath, serializedSaveFile);
        }

        public void LoadTasks()
        {
            var serializedFile = File.ReadAllText(SaveFilePath);
            if (string.IsNullOrEmpty(serializedFile))
            {
                Debug.LogError($"Loaded file {SaveFilePath} is empty.");
            }
            else
            {
                Debug.Log($"Save to {SaveFilePath}");
                var saveLoadData = JsonConvert.DeserializeObject<SaveLoadData>(serializedFile);
                RestoreValues(saveLoadData);
            }
        }

        public Task GetLastDoneTask
        {
            get => _doneTasks.Last();
        }
    }
}


