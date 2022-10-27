
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {   
        [SerializeField] 
        private PlayerMovementController _playerMovementController;
        private void OnTriggerEnter(Collider other)
        {
            if (!_playerMovementController._isReadyToMove)  return;
            
                if (!other.CompareTag("Finish")) return;
            
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();

        }
    }
}