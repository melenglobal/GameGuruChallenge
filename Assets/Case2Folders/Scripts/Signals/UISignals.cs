using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Extentions;
using UnityEngine.Events;

namespace Case2Folders.Scripts.Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel = delegate {  };
        public UnityAction<UIPanels> onClosePanel = delegate {  };
        
    }
}