using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.CollectableControllers
{
    public class CollectableDiamondController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CoreGameSignals.Instance.onUpdateDiamondScoreValue?.Invoke();
            gameObject.SetActive(false);
        }
    }
}