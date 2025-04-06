using Newtonsoft.Json;
using System;

namespace ProgressModul
{
    [Serializable]
    public class SubTaskModel
    {
        [JsonProperty("id")]
        public string Id { get; private set; }
        [JsonProperty("description")]
        public string Description { get; private set; }
        [JsonProperty("isDone")]
        public bool IsDone { get; set; }
        [JsonProperty("flow")]
        public string Flow { get; private set; }
        [JsonProperty("stackIndex")]
        public int StackIndex { get; set; }
        [JsonProperty("friends")]
        public string[] Friends { get; private set; }

        public SubTaskModel(string id, string description, string flow, int stackIndex, bool isDone = false, string[] friends = null)
        {
            Id = id;
            Description = description;
            Flow = flow;
            StackIndex = stackIndex;
            IsDone = isDone;
            Friends = friends;
        }

        public override string ToString()
        {
            return $"{Id} : {Description} : {IsDone} : {Flow} : {StackIndex} : [{(Friends == null ? null : string.Join(", ", Friends))}]";
        }
    }
}
