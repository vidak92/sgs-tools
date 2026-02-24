using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SGSTools.Util
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Clear()
        {
            _services.Clear();
        }

        public static void Add<T>(T service)
        {
            var typeKey = typeof(T);
            if (_services.ContainsKey(typeKey))
            {
                Debug.LogWarning($"ServiceLocator: Cannot add service for type: {typeKey}, it's already been registered.");
                return;
            }

            _services[typeKey] = service;
        }

        public static void Remove<T>(T service)
        {
            var typeKey = typeof(T);
            if (!_services.ContainsKey(typeKey))
            {
                Debug.LogWarning($"ServiceLocator: Cannot remove service for type: {typeKey}, it hasn't been registered.");
                return;
            }

            _services.Remove(typeKey);
        }

        public static T Get<T>()
        {
            var typeKey = typeof(T);
            if (!_services.ContainsKey(typeKey))
            {
                Debug.LogWarning($"ServiceLocator: Cannot get service for type: {typeKey}, it hasn't been registered. Returning default value.");
                return default;
            }

            return (T)_services[typeKey];
        }
    }
}