using System;
using Case2Folders.Scripts.Controllers.PlayerControllers;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers
{
    public class FinishObjectPhysicsController : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _collider.enabled = false;
            }
        }
    }
}