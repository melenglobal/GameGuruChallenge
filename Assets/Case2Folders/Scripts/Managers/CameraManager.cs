using System;
using Case2Folders.Scripts.Enums;
using Cinemachine;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private Transform _playerTransform;

        #endregion
        #region Serialized Variables

        [SerializeField] 
        private CinemachineStateDrivenCamera stateDrivenCamera;
        
        [SerializeField]
        private Animator cameraAnimator;

        #endregion

        #endregion

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            
        }
        
        private void UnsubscribeEvents()
        {
            
        }
        private void OnDisable() =>  UnsubscribeEvents();
        
        private void OnPlayerInitialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            stateDrivenCamera.Follow = _playerTransform;
        }

        private void OnSetCameraTarget()
        {
            
        }

        private void ChangeCamera(CameraTypes cameraType) => cameraAnimator.Play(cameraType.ToString());


    }
}