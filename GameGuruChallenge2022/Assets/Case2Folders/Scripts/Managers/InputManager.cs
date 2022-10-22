using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Case2Folders.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables
        
        [SerializeField] 
        private bool isReadyForTouch;

        #endregion
        

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }
        
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }
        
        private void OnDisable() => UnsubscribeEvents();

        private void Update()
        {
            if (!isReadyForTouch) return;
            
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {   
                Debug.Log("Click!");
                InputSignals.Instance.onPlatformStop?.Invoke();
            }
        }

        private void OnEnableInput() =>  isReadyForTouch = true;

        private void OnDisableInput() => isReadyForTouch = false;
        
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

    }
}