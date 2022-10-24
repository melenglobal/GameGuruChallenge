using System;
using UnityEngine;

namespace Case2Folders.Scripts.Data.ValueObject
{   
    [Serializable]
    public struct ScoreData
    {
        public int starScoreValue;
        
        public int coinScoreValue;
        
        public int diamondScoreValue;
    }
}