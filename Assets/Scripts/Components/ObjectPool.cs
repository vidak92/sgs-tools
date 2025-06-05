using System.Collections.Generic;
using MijanTools.Common;
using UnityEngine;

namespace MijanTools.Components
{
    public class ObjectPool<T> where T : Component
    {
        private T _prefab;
        private int _initialCapacity;
        private int _count;
        private List<T> _pool;
        private List<T> _activeObjects;

        public Transform Parent { get; private set; }
        public int ActiveObjectCount => _activeObjects.Count;

        public static ObjectPool<T> CreateWithGameObject(T prefab, int initialCapacity, string gameObjectName)
        {
            var gameObject = new GameObject(gameObjectName);
            var pool = new ObjectPool<T>(prefab, initialCapacity, gameObject.transform);
            return pool;
        }

        public ObjectPool(T prefab, int initialCapacity, Transform parent)
        {
            _prefab = prefab;
            _initialCapacity = initialCapacity;
            _count = 0;
            _pool = new List<T>();
            _activeObjects = new List<T>();
            Parent = parent;
            for (int i = 0; i < _initialCapacity; i++)
            {
                AddNewObject();
            }
        }

        public void Return(T poolObject)
        {
            if (_pool.Contains(poolObject))
            {
                Debug.LogWarning($"Object pool already contains object {poolObject}, skipping return.");
                return;
            }
            
            poolObject.gameObject.SetActive(false);
            poolObject.transform.SetParent(Parent, false);
            _pool.Add(poolObject);
            _activeObjects.Remove(poolObject);
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                AddNewObject();
            }

            if (_pool.Count > 0)
            {
                var poolObject = _pool[0];
                _pool.RemoveAt(0);
                _activeObjects.Add(poolObject);
                poolObject.gameObject.SetActive(true);
                poolObject.gameObject.transform.SetParent(null, false);
                return poolObject;
            }
            else
            {
                Debug.LogWarning($"{nameof(ObjectPool<T>)}: Pool is empty, cannot get new object.");
                return null;
            }
        }

        private void AddNewObject()
        {
            var poolObject = Object.Instantiate(_prefab, Parent);
            if (poolObject != null)
            {
                poolObject.gameObject.name = $"{poolObject.gameObject.name}_{_count}";
                poolObject.gameObject.SetActive(false);
                _pool.Add(poolObject);
                _count++;
            }
            else
            {
                Debug.LogError($"{nameof(ObjectPool<T>)}: Cannot instantiate prefab.");
            }
        }

        public void ReturnAllActiveObjects()
        {
            for (int i = _activeObjects.Count - 1; i >= 0; i--)
            {
                Return(_activeObjects[i]);
            }
        }
        
        public T GetActiveObject(int index)
        {
            return _activeObjects.ContainsIndex(index) ? _activeObjects[index] : null;
        }
    }
}