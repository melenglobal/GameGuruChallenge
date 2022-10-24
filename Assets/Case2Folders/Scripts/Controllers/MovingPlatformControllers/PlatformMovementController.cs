﻿using System;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Signals;
using DG.Tweening;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.MovingPlatformControllers
{
    public class PlatformMovementController : MonoBehaviour
    {   
        
        private float _moveSpeed = 2f;

        [SerializeField] 
        private Material material;

        public PlatformMovementController CurrentMovementController;
        
        public PlatformMovementController LastMovementController;
        
        public PlatformMoveDirectionType MoveDirectionType;
        
        
        private void OnEnable()
        {
           SetMovementController();
        }

        private void SetMovementController()
        {
            LastMovementController = CoreGameSignals.Instance.onCurrentPlatformChange?.Invoke(this);
            
            if (LastMovementController == null || LastMovementController == this)
            {
                LastMovementController = this;
            }
            CurrentMovementController = this;

            GetComponent<Renderer>().material.color = GetRandomColor();

            transform.localScale = new Vector3(LastMovementController.transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
        
        private Color GetRandomColor() // Create color list and get random color from list
        {
            return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f),
                UnityEngine.Random.Range(0, 1f));
        }
        
        public void StopPlatform()
        {
            _moveSpeed = 0;
            DOTween.KillAll();
            float stoppingDistanceX = transform.position.x - LastMovementController.transform.position.x;

            if (Mathf.Abs(stoppingDistanceX) >= LastMovementController.transform.localScale.x)
            {
                LastMovementController = null;
                CurrentMovementController = null; 
            }
            float edgeDirection = stoppingDistanceX > 0 ? 1f : -1f;
            CutPlatformAlongXAxis(stoppingDistanceX,edgeDirection);                                                      
        }
        

        private void CutPlatformAlongXAxis(float stoppingDistanceX,float edgeDirection)
        {
            float newXSize = LastMovementController.transform.localScale.x - Mathf.Abs(stoppingDistanceX);
            
            float fallingBlockSize = transform.localScale.x- newXSize;

            float newZPosition = LastMovementController.transform.position.x + (stoppingDistanceX / 2);

            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);

            transform.position = new Vector3(newZPosition, transform.position.y, transform.position.z);

            float platformEdge = transform.position.x + (newXSize / 2f * edgeDirection);

            float fallingBlockXPosition = platformEdge + (fallingBlockSize / 2f * edgeDirection);
            
            SpawnFallingCube(fallingBlockXPosition,fallingBlockSize);

        }

        private void SpawnFallingCube(float fallingBlockXPosition,float fallingBlockSize)
        {
            var fallingBlock = GameObject.CreatePrimitive(PrimitiveType.Cube); // Do not create cube,use pool for that

            fallingBlock.transform.localScale = 
                new Vector3(fallingBlockSize,transform.localScale.y,transform.localScale.z);

            fallingBlock.transform.position =
                new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z);

            fallingBlock.AddComponent<Rigidbody>(); // Do not add rigidbody,use pool for that
            // Create Pool
            fallingBlock.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color; //it's very bad way to handle.
            
            Destroy(fallingBlock.gameObject,1f); //Release object to pool
            
        }
    }
}