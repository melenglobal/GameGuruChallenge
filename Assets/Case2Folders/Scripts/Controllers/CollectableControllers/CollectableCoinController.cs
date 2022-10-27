using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.CollectableControllers
{
    public class CollectableCoinController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CoreGameSignals.Instance.onUpdateCoinScoreValue?.Invoke();
            gameObject.SetActive(false);
        }
    }
}