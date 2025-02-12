
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressModul
{
    public class TaskObserver : ISaveLoadObject
    {
        private List<Task> _tasks;
        public delegate void TaskHandler(Task task);
        public delegate void SubTaskListHandler(IEnumerable<SubTask> subTasks);
        public event TaskHandler TaskStateChanged;
        public event SubTaskListHandler HaveNewSubTasks;
        
        public string ComponentSaveId => "TaskObserver";

        static public List<Task> ParseJsonWithTasks(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = { new TaskModelJsonConverter() }
            };
            var taskModels = JsonConvert.DeserializeObject<List<TaskModel>>(json, settings);
            List<Task> tasks = taskModels.Select(t => new Task(t)).ToList();
            return tasks;
        }

        public TaskObserver(string json)
        {
            _tasks = ParseJsonWithTasks(json);
        }

        public TaskObserver()
        {
        }

        public IEnumerable<Task> Tasks => _tasks;

        public IEnumerable<Task> GetTasks(TaskState state, TaskType taskType, int leftBorder, int rightBorder)
        {
            return _tasks.Where(
                t => t.State == state && 
                t.Type == taskType && 
                leftBorder <= t.StartTime && 
                rightBorder >= t.DeadTime
            );
        }

        private void _setTaskState(Task task,  TaskState state)
        {
            task.State = state;
            if (TaskStateChanged != null)
                TaskStateChanged(task);
        }

        public void SetTaskStateById(string id, TaskState state)
        {
            var task = _getTaskById(id);
            if (task == null)
            {
                Debug.LogError($"Id: {id} в Tasks нет");
                return;
            }
            _setTaskState(task, state);
        }

        private Task _getTaskById(string id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public void SetDoneSubTaskById(string taskId, string id)
        {
            var task = _getTaskById(taskId);
            if(task == null)
            {
                Debug.LogError($"Id: {id} в Tasks нет");
                return;
            }

            var subTasks = task.SubTasks;

            var finded = subTasks.FirstOrDefault(x => x.Id == id);
            if (finded == null)
            {
                Debug.LogError($"Id: {id} в SubTasks нет");
                return;
            }

            if (finded.SetDone())
            {
                foreach (var subTask in subTasks)
                {
                    subTask.DeacreaseStackIndex();
                    if (finded.Friends != null && finded.Friends.Contains(subTask.Id))
                    {
                        SetDoneSubTaskById(taskId, subTask.Id);
                    }
                }

                if (HaveNewSubTasks != null)
                    HaveNewSubTasks(task.CurrentSubTasks);
            }

            if (subTasks.Where(s => !s.IsDone && s.StackIndex == 0).Count() == 0)
            {
                _setTaskState(task, TaskState.Done);
            }
        }

        public SaveLoadData GetSaveLoadData()
        {
            return new TaskSaveLoadData(ComponentSaveId, _tasks.Select(x => x.GetModel).ToList());
        }

        public void RestoreValues(SaveLoadData loadData)
        {
            _tasks.Clear();

            if (loadData?.Data == null || loadData.Data.Length < 2)
            {
                Debug.LogError($"Can't restore values.");
                return;
            }

            // [0] - (JArray) with items

            var items = ((JArray)loadData.Data[0]).ToObject<List<TaskModel>>();
            _tasks.AddRange(items.Select(x => new Task(x)));
        }


        public void SetDefault()
        {
            string json = Resources.Load<TextAsset>("InitTasks").text;
            _tasks = ParseJsonWithTasks(json);
        }
    }
}