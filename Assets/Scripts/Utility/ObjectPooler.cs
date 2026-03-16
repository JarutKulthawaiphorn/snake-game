using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Utility
{
    public class ObjectPooler<T> where T : MonoBehaviour
    {
        #region Public Properties
        public int PoolSize => _poolList.Count;
        #endregion

        #region Private Fields
        private List<T> _poolList;
        private T _prefab;
        private Transform _parentTransform;
        #endregion

        #region Constructors
        public ObjectPooler(T prefab, Transform parentTransform, int initialSize = 0)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            _poolList = new List<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                T instance = Object.Instantiate(_prefab, _parentTransform);
                instance.gameObject.SetActive(false);
                _poolList.Add(instance);
            }
        }
        #endregion

        #region Public Methods
        public T GetObject()
        {
            T targetObject = null;

            if (_poolList.Count == 0)
            {
                targetObject = CreateNewObject();
            }
            else
            {
                if (_poolList[0] == null)
                {
                    targetObject = CreateNewObject();
                }
                else
                {
                    targetObject = _poolList[0];
                    _poolList.RemoveAt(0);
                }
            }

            targetObject.gameObject.SetActive(true);
            return targetObject;
        }

        public List<T> GetAllObjects()
        {
            List<T> objectsToReturn = new List<T>(_poolList);

            for (int i = _poolList.Count - 1; i >= 0; i--)
            {
                objectsToReturn.Add(_poolList[i]);
                _poolList.RemoveAt(i);
            }

            return objectsToReturn;
        }

        public void ReturnObject(T obj)
        {
            if (obj == null) return;
            obj.gameObject.SetActive(false);
            _poolList.Add(obj);
        }

        public void ReturnObjectList(List<T> objList)
        {
            if (objList == null || objList.Count == 0) return;
            foreach (var obj in objList)
            {
                if (obj == null) continue;
                obj.gameObject.SetActive(false);
                _poolList.Add(obj);
            }
        }
        #endregion

        #region Private Methods
        private T CreateNewObject()
        {
            T instance = Object.Instantiate(_prefab, _parentTransform);
            instance.gameObject.SetActive(false);

            return instance;
        }
        #endregion
    }
}