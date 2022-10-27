using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private CharacterMovementData _movementData;

        private Transform playerTransform;

        public bool _isReadyToMove;

        private bool _isBegginToWalkPlatform;

        private bool _isRotateToPlatform;
        private bool _isPlayerFalling => transform.position.y < 0f;
        
        private float _currentXAxisValue;

        private float _platformZScale = 1.5f;
        
        

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private new Rigidbody rigidbody;
        
        [SerializeField]
        private Transform _lastMovingPlatformTransform;


        #endregion

        #endregion

        public void SetMovementData(CharacterMovementData movementData) => _movementData = movementData;

        private void Awake()
        {
            playerTransform = transform;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToMove) return;

            if (_isPlayerFalling)
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                return;
            }
            
            RotatePlayer();
            
            Move();
            
        }

        public void ReadyToMove(bool isReady) => _isReadyToMove = isReady;

        private void Move()
        {
            rigidbody.MovePosition(playerTransform.position +
                                   playerTransform.forward * (Time.fixedDeltaTime* _movementData.ForwardSpeed));
        }
        

        public void SetLastPlatformTransform(Transform lastPlatformTransform)
        {
            _lastMovingPlatformTransform = lastPlatformTransform;
            _isBegginToWalkPlatform =true;
            _isRotateToPlatform = true;
        }
        
        private void RotatePlayer()
        {
            if (_lastMovingPlatformTransform == null) return;

            if (!_isBegginToWalkPlatform) return;

            Vector3 rotateDirection = _lastMovingPlatformTransform.position - playerTransform.position;

            if (rotateDirection == Vector3.zero) return;
            
            if (Vector3.Distance(_lastMovingPlatformTransform.position, playerTransform.position) < _platformZScale)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
                toRotation = quaternion.Euler(0,toRotation.eulerAngles.y, 0);
                rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, toRotation, 1f)); 
                _isBegginToWalkPlatform = false;
            }
            else
            {
                Quaternion toRotation = Quaternion.LookRotation(rotateDirection);
                toRotation = quaternion.Euler(0,toRotation.eulerAngles.y, 0);
                rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation, toRotation,22.5f));
                _isRotateToPlatform = false;
            }

        }
        
    }
}