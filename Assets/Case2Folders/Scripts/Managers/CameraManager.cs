using System;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Signals;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private Transform _playerTransform;

        
        private CinemachineOrbitalTransposer _orbitalTransposer;

        #endregion
        #region Serialized Variables

        [SerializeField] 
        private CinemachineStateDrivenCamera stateDrivenCamera;
        
        [SerializeField] 
        private CinemachineVirtualCamera finishCamera;

        [SerializeField]
        private Animator cameraAnimator;
        
        private bool _isLevelSuccess;

        #endregion

        #endregion

        private void Awake()
        {
            _orbitalTransposer = finishCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        }


        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
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
        private void OnNextLevel()
        {
            _isLevelSuccess = false;
            ChangeCamera(CameraTypes.Level);
            ResetCameraRotation();
        }
        private void OnLevelSuccessful()
        {
            _isLevelSuccess = true;
            _orbitalTransposer.m_XAxis.m_InputAxisName = "";
            ChangeCamera(CameraTypes.Finish);
        }

        private void ResetCameraRotation()
        {
            _orbitalTransposer.m_XAxis.Value = 0;
        }

        private void RotateCameraAroundPlayer()
        {
            _orbitalTransposer.m_XAxis.Value += Time.deltaTime * _orbitalTransposer.m_XAxis.m_MaxSpeed;
        }

        private void Update()
        {
            if (_isLevelSuccess)
            {
                RotateCameraAroundPlayer();
            }
        }

        private void ChangeCamera(CameraTypes cameraType) => cameraAnimator.Play(cameraType.ToString());


    }
}