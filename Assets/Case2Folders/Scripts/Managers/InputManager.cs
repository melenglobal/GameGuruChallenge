using System.Collections.Generic;
using Case2Folders.Scripts.Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Case2Folders.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private bool isReadyForTouch;

        #endregion
        
        #region Private Variables
        
        private bool _hasTouched;
        
        #endregion
        
        #endregion

        #region Event Subscriptions
        
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnEnableInput;
        }
        
        private void UnsubscribeEvents()
        {  
            CoreGameSignals.Instance.onPlay -= OnEnableInput;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }
        private void OnDisable() => UnsubscribeEvents();
        
        #endregion
        
        private void Update()
        {
            if (!isReadyForTouch) return;

            if (Input.GetMouseButton(0) && !_hasTouched)
            {
                _hasTouched = true;
            }
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
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