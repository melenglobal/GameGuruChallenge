using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Interfaces;
using UnityEngine;


namespace Case2Folders.Scripts.Extentions
{   
    //This ObjectPool is created by OneWheelStudio and can be found here: https://www.youtube.com/watch?v=x6jFZvvOGgk
    //I have made some changes to it to make it work with my GameGuruChallenge Case Project.
    public class ObjectPool<T> : IPool<T> where T : MonoBehaviour,IPoolable<T>
    {
        public ObjectPool(GameObject pooledObject, int numToSpawn = 0)
        {
            prefab = pooledObject;
            Spawn(numToSpawn);
        }

        public ObjectPool(GameObject pooledObject, Action<T> pullObject, Action<T> pushObject, int numToSpawn = 0)
        {
            prefab = pooledObject;
            this.pullObject = pullObject;
            this.pushObject = pushObject;
            Spawn(numToSpawn);
        }

        private Action<T> pullObject;
        private Action<T> pushObject;
        private Stack<T> pooledObjects = new Stack<T>();
        private GameObject prefab;
        private int PooledCount => pooledObjects.Count;

        public T Pull()
        {
            T t;
            t = PooledCount > 0 ? pooledObjects.Pop() : GameObject.Instantiate(prefab).GetComponent<T>();

            t.gameObject.SetActive(true); //ensure the object is on
            t.Initialize(Push);

            //allow default behavior and turning object back on
            pullObject?.Invoke(t);

            return t;
        }

        public T Pull(Vector3 position)
        {
            T t = Pull();
            t.transform.position = position;
            return t;
        }

        public T Pull(Vector3 position, Quaternion rotation)
        {
            T t = Pull();
            t.transform.position = position;
            t.transform.rotation = rotation;
            return t;
        }

        public GameObject PullGameObject()
        {
            return Pull().gameObject;
        }

        public GameObject PullGameObject(Vector3 position)
        {
            GameObject go = Pull().gameObject;
            go.transform.position = position;
            return go;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation)
        {
            GameObject go = Pull().gameObject;
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }

        public void Push(T t)
        {
            pooledObjects.Push(t);

            //create default behavior to turn off objects
            pushObject?.Invoke(t);

            t.gameObject.SetActive(false);
        }

        private void Spawn(int number)
        {
            T t;

            for (int i = 0; i < number; i++)
            {
                t = GameObject.Instantiate(prefab).GetComponent<T>();
                pooledObjects.Push(t);
                t.gameObject.SetActive(false);
            }
        }
        
    }
}