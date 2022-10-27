using System;
using Case2Folders.Scripts.Interfaces;
using UnityEngine;

namespace Case2Folders.Scripts.Extentions
{
    public class PoolObject : MonoBehaviour,IPoolable<PoolObject>
    {
        private Action<PoolObject> returnToPool;

        public PoolObject()
        {
            returnToPool = null;
        }

        public void Initialize(Action<PoolObject> returnAction)
        {
            //cache reference to return action
            this.returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            //invoke and return this object to pool
            returnToPool?.Invoke(this);
        }
        private void OnBecameInvisible()
        {
            ReturnToPool();
        }
    }
}