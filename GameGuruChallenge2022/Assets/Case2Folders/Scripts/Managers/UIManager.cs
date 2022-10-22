using System;
using Case2Folders.Scripts.Controllers;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] 
        private UIPanelController uiPanelController;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            
        }
    }
}