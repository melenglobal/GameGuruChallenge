using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Extentions;
using UnityEngine.Events;

namespace Case1Folders.Scripts.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public Func<GridElement, List<GridElement>> onGetNeighbours = delegate { return null; };
        
        public UnityAction onUpdateMatchCount = delegate {  };
        public static UnityAction onGridClicked  = delegate {  };
    }
}