using System;
using Case2Folders.Scripts.Data.ValueObject;
using Case2Folders.Scripts.Extentions;
using Cinemachine;
using UnityEngine.Events;

namespace Case2Folders.Scripts.Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<LevelData,int> onSaveData = delegate { };
        
        public Func<string, int, LevelData> onLoadData = delegate { return default; };
    }
}