using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Controllers.MovingPlatformControllers;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Extentions;
using Case2Folders.Scripts.Signals;
using DG.Tweening;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class MovingPlatformManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private ObjectPool<PoolObject> _platformObjectPool;
        
        private ObjectPool<PoolObject> _fallingBlockObjectPool;

        private readonly List<PoolObject> _pooledObjects = new List<PoolObject>();

        private bool _canSpawnPlatform = true;

        #endregion
        #region Serialized Variables
        
        [SerializeField] 
        private GameObject movingPlatformPrefab;
        [SerializeField]
        private GameObject fallingBlockPrefab;
        [SerializeField]
        private PlatformMovementController currentPlatform;
        [SerializeField] 
        private PlatformMovementController lastPlatform;
        [SerializeField] 
        private PlatformMoveDirectionType platformMoveDirectionType;
        
        #endregion

        #endregion

        private void Awake()
        {
            _platformObjectPool = new ObjectPool<PoolObject>(movingPlatformPrefab, 20);
            _fallingBlockObjectPool = new ObjectPool<PoolObject>(fallingBlockPrefab, 5);
        }

        #region Event Subscriptions
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop += OnPlatformStop;
            CoreGameSignals.Instance.onCurrentPlatformChange += OnPlatformChange;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onPlay += OnStartSpawnPlatform;
            CoreGameSignals.Instance.onResetLevel += OnReset;
            CoreGameSignals.Instance.onGetFallingBlock += OnGetFallingBlockFromPool;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onPlatformStop -= OnPlatformStop;
            CoreGameSignals.Instance.onCurrentPlatformChange -= OnPlatformChange;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onPlay -= OnStartSpawnPlatform;
            CoreGameSignals.Instance.onResetLevel -= OnReset;
            CoreGameSignals.Instance.onGetFallingBlock += OnGetFallingBlockFromPool;
        }
        private void OnDisable() => UnsubscribeEvents();
        
        #endregion
        
        private void Start()
        {
            SetSpawnPosition();
            GetInitPlatformFromPool();
        }
        private void SetSpawnPosition() => transform.position = CoreGameSignals.Instance.onGetSpawnPosition.Invoke();
        private void OnNextLevel()
        {
            ResetPlatforms();
            transform.position = CoreGameSignals.Instance.onGetSpawnPosition.Invoke();
            GetInitPlatformFromPool();
            DOVirtual.DelayedCall(.5f, SpawnPlatform);
        }
        private void OnPlatformStop()
        {
            if (!currentPlatform.CanCut) return;
            currentPlatform.StopPlatform();
            _canSpawnPlatform = CoreGameSignals.Instance.onCheckCanSpawnPlatform.Invoke(currentPlatform.transform);
            if (_canSpawnPlatform)
            {
                SpawnPlatform();
            }
        }
        
        private void OnStartSpawnPlatform()
        {
            SpawnPlatform();
        }
        private PlatformMovementController OnPlatformChange(PlatformMovementController currentMovementController)
        {
            if (lastPlatform == null)
            {   
                currentPlatform = currentMovementController;
                lastPlatform = currentPlatform;
                return lastPlatform;
            }
            lastPlatform = currentPlatform;
            currentPlatform = currentMovementController;
            return lastPlatform;
        }
        private void GetInitPlatformFromPool()
        {
            GetPlatformFromPool().position = transform.position;
            ChangeSpawnPositionX();
        }
        private void SpawnPlatform()
        {
            var platformTransform = GetPlatformFromPool();
            platformTransform.position = 
                    new Vector3(transform.position.x, transform.position.y,
                        lastPlatform.transform.position.z + platformTransform.localScale.z);
            MovePlatform(platformTransform.gameObject);
            ChangeSpawnPositionX();
        }

        private Transform GetPlatformFromPool()
        {
            PoolObject newPlatform = _platformObjectPool.Pull();
            _pooledObjects.Add(newPlatform);
            return newPlatform.transform;
        }
        private GameObject OnGetFallingBlockFromPool(Vector3 edgePosition)
        {
            PoolObject newFallingBlock = _fallingBlockObjectPool.Pull();
            newFallingBlock.transform.position = edgePosition;
            return _pooledObjects.Count < 1 ? null : newFallingBlock.gameObject;
        }
        private void ChangeSpawnPositionX()
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
        private void OnReset()
        {
            _canSpawnPlatform = false;
            ResetPlatforms();
            currentPlatform = null;
            lastPlatform = null;
            transform.position = CoreGameSignals.Instance.onGetSpawnPosition.Invoke();
            GetInitPlatformFromPool();
        }
        private void ResetPlatforms()
        {   
            
            foreach (var pooledObject in _pooledObjects)
            {
                pooledObject.gameObject.transform.localScale = new Vector3(3, 1, 3);
                pooledObject.gameObject.SetActive(false);
            }
            _pooledObjects.Clear();
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