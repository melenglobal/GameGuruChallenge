using System;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerMovementController : MonoBehaviour
    {   
        #region Self Variables

        #region Private Variables

        private CharacterMovementData _movementData;

        private Transform playerTransform;
        
        private bool _isReadyToMove;
        private bool _isPlayerFalling => transform.position.y < 0.45f;

        #endregion
        
        #region Serialized Variables

        [SerializeField] 
        private new Rigidbody rigidbody;
        

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
            
            Move();
            
        }
        
        public void ReadyToMove(bool isReady) => _isReadyToMove = isReady;
        private void Move()
        {
            rigidbody.MovePosition(playerTransform.position + playerTransform.forward * (Time.deltaTime * _movementData.ForwardSpeed));
        }
    }
}