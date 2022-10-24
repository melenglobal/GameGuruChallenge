using System;
using Case2Folders.Scripts.Extentions;
using UnityEngine.Events;

namespace Case2Folders.Scripts.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate {  };
        
        public UnityAction onDisableInput = delegate {  };
        
        public UnityAction onPlatformStop = delegate {  };
    }
}