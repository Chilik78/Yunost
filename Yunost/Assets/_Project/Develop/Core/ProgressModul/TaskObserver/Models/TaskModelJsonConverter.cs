using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ProgressModul
{

    public class TaskModelJsonConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return typeof(TaskModel) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = JObject.Load(reader);

            string id = jObject["id"]?.ToString();
            string name = jObject["name"]?.ToString();
            string flow = jObject["flow"]?.ToString();
            TaskState state = (TaskState)(jObject["state"]?.Value<int>() ?? default);
            TaskType type = (TaskType)(jObject["type"]?.Value<int>() ?? default);
            int startTime = jObject["startTime"]?.Value<int>() ?? default;
            int deadTime = jObject["deadTime"]?.Value<int>() ?? default;

            List<SubTaskModel> subTasks = new List<SubTaskModel>();

            if (jObject["subTasks"] != null && jObject["subTasks"].HasValues)
            {
                foreach (var subTask in jObject["subTasks"])
                {
                    string sid = subTask["id"]?.ToString();
                    string sdescription = subTask["description"]?.ToString();
                    string sflow = subTask["flow"]?.ToString();
                    int stackIndex = subTask["stackIndex"]?.Value<int>() ?? default;
                    bool isDone = subTask["isDone"]?.Value<bool>() ?? default;
                    string[] friends = subTask["friends"]?.ToObject<string[]>();
                    int currentCount = subTask["currentCount"]?.Value<int>() ?? default;
                    int finalCount = subTask["finalCount"]?.Value<int>() ?? default;

                    if (subTask["finalCount"] != null)
                    {
                        subTasks.Add(
                            new CounterSubTaskModel(sid, sdescription, sflow, stackIndex, finalCount, currentCount, isDone, friends)
                        );
                    }
                    else
                    {
                        subTasks.Add(
                            new SubTaskModel(sid, sdescription, sflow, stackIndex, isDone, friends)
                        );
                    }
                }
            }

            TaskModel taskModel = new TaskModel(id, name, flow, state, type, subTasks.ToArray(), startTime, deadTime);
            return taskModel;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Метод WriteJson не реализован.");
        }
    }
}
