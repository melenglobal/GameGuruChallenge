using System;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Finish")) return;
            
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();

        }
    }
}