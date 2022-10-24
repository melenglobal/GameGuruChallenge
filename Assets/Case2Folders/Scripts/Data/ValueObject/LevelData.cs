using System;
using Case2Folders.Scripts.Interfaces;

namespace Case2Folders.Scripts.Data.ValueObject
{   
    [Serializable]
    public class LevelData : ISaveableEntity
    {
        public int PlatformCount;
        
        private const string LEVEL_DATA_KEY = "LevelData";
        
        public LevelData()
        {
            
        }
        
        public LevelData(int platformCount)
        {
            PlatformCount = platformCount;
        }

        public string GetKey()
        {
            return LEVEL_DATA_KEY;
        }
    }
}