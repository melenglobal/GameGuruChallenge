using System.Collections.Generic;
using Case2Folders.Scripts.Enums;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> panels = new List<GameObject>();

        public void OpenPanel(UIPanels panelParam) => panels[(int)panelParam].SetActive(true);

        public void ClosePanel(UIPanels panelParam) => panels[(int)panelParam].SetActive(false);

    }
    
}