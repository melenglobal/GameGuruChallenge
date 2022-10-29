using System;
using Case1Folders.Scripts.Signals;
using TMPro;
using UnityEngine;

namespace Case1Folders.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        [SerializeField] 
        private TextMeshProUGUI matchCountText;
        
        private int _matchCount;

        #endregion


        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents() =>  CoreGameSignals.Instance.onUpdateMatchCount += OnUpdateMatchCount;
        private void UnsubscribeEvents() => CoreGameSignals.Instance.onUpdateMatchCount -= OnUpdateMatchCount;
        private void OnDisable() => UnsubscribeEvents();

        #endregion
        
        private void OnUpdateMatchCount()
        {   
            _matchCount++;
            matchCountText.text = _matchCount.ToString();
        }

        
        
    }
}