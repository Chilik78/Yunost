using Newtonsoft.Json;
using System;
using System.Linq;

namespace ProgressModul
{
    public enum TaskType
    {
        Main,
        Side
    }

    public enum TaskState
    {
        InProgress,
        Done,
        Failed
    }

    [Serializable]
    public class TaskModel
    {
        [JsonProperty("id")]
        public string Id { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("flow")]
        public string Flow { get; set;  }
        [JsonProperty("subTasks")]
        public SubTaskModel[] SubTasks { get; set; }
        [JsonProperty("type")]
        public TaskType Type { get; private set; }
        [JsonProperty("state")]
        public TaskState State { get; set; }
        [JsonProperty("startTime")]
        public int StartTime { get; private set; }
        [JsonProperty("deadTime")]
        public int DeadTime { get; private set; }

        public TaskModel(string id, string name, string flow, TaskState state, TaskType type, SubTaskModel[] subTasks, int startTime, int deadTime)
        {
            Id = id;
            Name = name;
            Flow = flow;
            State = state;
            Type = type;
            SubTasks = subTasks;
            StartTime = startTime;
            DeadTime = deadTime;
        }

        public override string ToString()
        {
            return $"{Id} : {Name} : {Flow} : {State} : {Type} : {StartTime} : {DeadTime} : \n[{(SubTasks == null ? null : string.Join("\n", SubTasks.Select(s => s.ToString())))}]";
        }
    }
}
