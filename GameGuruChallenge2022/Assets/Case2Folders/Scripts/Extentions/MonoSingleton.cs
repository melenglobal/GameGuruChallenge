using UnityEngine;

namespace Case2Folders.Scripts.Extentions
{
    public class MonoSingleton<T>: MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                _instance = FindObjectOfType<T>();
                
                if (_instance != null) return _instance;
                
                GameObject newGo = new GameObject();
                _instance = newGo.AddComponent<T>();

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }
}