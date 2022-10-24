using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Controllers.MovingPlatformControllers;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Signals;
using DG.Tweening;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class MovingPlatformManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]
        private List<PlatformMovementController> movingPlatforms;
        
        [SerializeField] 
        private GameObject movingPlatformPrefab;
        
        [SerializeField]
        private PlatformMovementController currentPlatform;

        [SerializeField] 
        private PlatformMovementController lastPlatform;

        [SerializeField] 
        private PlatformMoveDirectionType platformMoveDirectionType;
        
        // En sonda bir bitis cizgisi var
        // levelden yonetilmeli
        // o pozisyona ulasinca,
        // bizim spawnerimizi o pozisyonun Vector3.forwardina koymamiz
        
        #endregion

        #endregion

        private void Start()
        {   
            SpawnInitPlatform();
            
        }

        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop += OnPlatformStop;
            CoreGameSignals.Instance.onCurrentPlatformChange += OnPlatformChange; 
            CoreGameSignals.Instance.onPlay += OnStartSpawnPlatform;
        }
        
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop -= OnPlatformStop;
            CoreGameSignals.Instance.onCurrentPlatformChange -= OnPlatformChange;
            CoreGameSignals.Instance.onPlay -= OnStartSpawnPlatform;
        }
        
        private void OnDisable() => UnsubscribeEvents();

        #endregion
       

        private void OnPlatformStop()
        {
            currentPlatform.StopPlatform(); //Current Platform Stop with player input
            SpawnPlatform(); // Spawn new platform
        }
        
        private void OnStartSpawnPlatform()
        {
            SpawnPlatform();
        }

        private PlatformMovementController OnPlatformChange(PlatformMovementController _currentMovementController)
        {
            if (lastPlatform == null)
            {   
                currentPlatform = _currentMovementController;
                lastPlatform = currentPlatform;
                return lastPlatform;
                
            }
            lastPlatform = currentPlatform;
            currentPlatform = _currentMovementController;
            return lastPlatform;
        }
        
        private void SpawnInitPlatform()
        {
            var movingPlatform = Instantiate(movingPlatformPrefab);
            movingPlatform.transform.position = transform.position;
            ChangeSpawnXPosition();
            
        }
        private void SpawnPlatform()
        {
            var movingPlatform = Instantiate(movingPlatformPrefab);
            movingPlatform.transform.position = 
                    new Vector3(transform.position.x, transform.position.y,
                        lastPlatform.transform.position.z + movingPlatform.transform.localScale.z);
            MovePlatform(movingPlatform);
            ChangeSpawnXPosition();
        }
        
        private void ChangeSpawnXPosition()
        {
            if (transform.position == Vector3.zero || transform.position == new Vector3(6, 0, 0))
            {
                transform.position = new Vector3(-6, 0, 0);
                platformMoveDirectionType = PlatformMoveDirectionType.Right;
        
            }
            else
            {
                transform.position = new Vector3(6, 0, 0); 
                platformMoveDirectionType = PlatformMoveDirectionType.Left;
            }
        }

        private void MovePlatform(GameObject movingPlatform)
        {
            if (platformMoveDirectionType == PlatformMoveDirectionType.Right)
            {
                movingPlatform.transform.DOLocalMoveX(6, 3f).SetEase(Ease.Linear);
            }
            else
            {
                movingPlatform.transform.DOLocalMoveX(-6, 3f).SetEase(Ease.Linear);
            }
        }
        private void OnEnableInput() => InputSignals.Instance.onEnableInput?.Invoke();
        
        private void OnDisableInput() =>  InputSignals.Instance.onDisableInput?.Invoke();
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position,movingPlatformPrefab.transform.localScale);
        }

    }
}