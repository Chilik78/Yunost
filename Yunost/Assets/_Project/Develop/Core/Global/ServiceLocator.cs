using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public class ServiceLocator
    {
        private readonly static Dictionary<string, object> _services = new Dictionary<string, object>();

        public static void Register<T>(T service)
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Already registered {key}");
                return;
            }

            _services.Add(key, service);
        }

        public static void Unregister<T>() 
        {
            string key = typeof (T).Name;
            if (!_services.ContainsKey(key))
            {

                Debug.LogError($"Already unregistered {key}");
                return;
            }

            _services.Remove(key);
        }

        public static T Get<T>()
        {
            string key = typeof (T).Name;
            if (!_services.ContainsKey(key))
            {
                return default(T);
            }

            return (T)_services.GetValueOrDefault(key);
        }
    }
}

