using System;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.MovingPlatformControllers
{
    public class PlatformMovementController : MonoBehaviour
    {   
        [SerializeField]
        private float _moveSpeed = 1f;

        public static PlatformMovementController CurrentMovementController{ get; private set;}
        private void OnEnable()
        {
            CurrentMovementController = this;
        }

        public void StopPlatform()
        {
            _moveSpeed = 0;
            Debug.Log(_moveSpeed);
        }

        private void Update()
        {
            var currentPlatformTransform = transform;
            currentPlatformTransform.position += currentPlatformTransform.right * (Time.deltaTime * _moveSpeed);
        }
    }
}