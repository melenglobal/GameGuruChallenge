using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Case1Folders.Scripts.Signals
{
    public class GridSignals : MonoSingleton<GridSignals>
    {
        public Func<GridElement, List<GridElement>> onGetNeighbours = delegate { return null; };
        
        public UnityAction onUpdateMatchCount = delegate {  };
    }
}