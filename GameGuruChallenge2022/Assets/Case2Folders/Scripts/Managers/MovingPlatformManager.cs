using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Controllers.MovingPlatformControllers;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class MovingPlatformManager : MonoBehaviour
    {   
        [SerializeField]
        private List<PlatformMovementController> movingPlatforms;
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop += OnPlatformStop;
  
        }
        
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop -= OnPlatformStop;
        }
        
        private void OnDisable() => UnsubscribeEvents();

        private void OnPlatformStop()
        {
            PlatformMovementController.CurrentMovementController.StopPlatform();
            Debug.Log("Moving Platform Manager Receive Call!");
        }
        
        private void OnEnableInput() => InputSignals.Instance.onEnableInput?.Invoke();
        
        private void OnDisableInput() =>  InputSignals.Instance.onDisableInput?.Invoke();
        
    }
}