using System;
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