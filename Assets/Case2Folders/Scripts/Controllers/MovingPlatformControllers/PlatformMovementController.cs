using System;
using Case2Folders.Scripts.Signals;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Case2Folders.Scripts.Controllers.MovingPlatformControllers
{
    public class PlatformMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public PlatformMovementController CurrentMovementController;
        
        public PlatformMovementController LastMovementController;

        #endregion

        #region Private Variables
        
        private Color _materialColor;
        private float _moveSpeed = 2f;

        public bool CanCut => _canCut;
        private bool _canCut;

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private MeshRenderer meshRenderer;
        [SerializeField] 
        private Material[] materials;

        #endregion

        #endregion
        
        private void OnEnable()
        {
            _canCut = true;
           SetMovementController();
           
        }

        private void SetMovementController()
        {
            LastMovementController = CoreGameSignals.Instance.onCurrentPlatformChange?.Invoke(this);
            
            if (LastMovementController == null || LastMovementController == this)
            {
                LastMovementController = this;
                _canCut = false;
            }
            
            CurrentMovementController = this;

            _materialColor = GetRandomColor();
            meshRenderer.material.color = _materialColor;

            var localScale = transform.localScale;
            localScale = new Vector3(LastMovementController.transform.localScale.x,localScale.y,localScale.z);
            transform.localScale = localScale;
        }
        
        private Color GetRandomColor() => materials[Random.Range(0, materials.Length)].color;
        
        public void StopPlatform()
        {
            if (!_canCut) return;
            _canCut = false;
            _moveSpeed = 0;
            DOTween.KillAll();
            float stoppingDistanceX = transform.position.x - LastMovementController.transform.position.x;
            if (LastMovementController == this)
            {
                return;
            }
            if (Mathf.Abs(stoppingDistanceX) >= LastMovementController.transform.localScale.x)
            {
                LastMovementController = null;
                CurrentMovementController = null;
                _canCut = false;
                return;
            }
            
            CoreGameSignals.Instance.onPlatformStop.Invoke(transform);

            float edgeDirection = stoppingDistanceX > 0 ? 1f : -1f;
            
            CutPlatformAlongXAxis(stoppingDistanceX,edgeDirection);

        }
        

        private void CutPlatformAlongXAxis(float stoppingDistanceX,float edgeDirection)
        {
            float newXSize = LastMovementController.transform.localScale.x - Mathf.Abs(stoppingDistanceX);

            var currentTransform = transform;
            var localScale = currentTransform.localScale;
            float fallingBlockSize = localScale.x- newXSize;

            float newZPosition = LastMovementController.transform.position.x + (stoppingDistanceX / 2);

            localScale = new Vector3(newXSize, localScale.y, localScale.z);
            currentTransform.localScale = localScale;

            var transformPosition = currentTransform.position;
            transformPosition = new Vector3(newZPosition, transformPosition.y, transformPosition.z);
            currentTransform.position = transformPosition;

            float platformEdge = transformPosition.x + (newXSize / 2f * edgeDirection);

            float fallingBlockXPosition = platformEdge + (fallingBlockSize / 2f * edgeDirection);
            
            SpawnFallingCube(fallingBlockXPosition,fallingBlockSize);

        }

        private void SpawnFallingCube(float fallingBlockXPosition,float fallingBlockSize)
        {   
            var fallingBlock = CoreGameSignals.Instance.onGetFallingBlock?.Invoke(transform.position);
            if (fallingBlock == null) return;
            fallingBlock.SetActive(true);
            var currentTransform = transform;
            var localScale = currentTransform.localScale;
            fallingBlock.transform.localScale = 
                new Vector3(fallingBlockSize,localScale.y,localScale.z);

            var transformPosition = currentTransform.position;
            fallingBlock.transform.position =
                new Vector3(fallingBlockXPosition, transformPosition.y, transformPosition.z);
            
            fallingBlock.GetComponent<Renderer>().material.color = _materialColor;

            DOVirtual.DelayedCall(3f, () =>
            {
                fallingBlock.SetActive(false);
            });
            
        }
    }
}