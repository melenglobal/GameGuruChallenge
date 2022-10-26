using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Interfaces;

namespace Case2Folders.Scripts.Data.ValueObjects
{   
    [Serializable]
    public class LevelData : ISaveableEntity
    {
        
        public List<LevelObjectData> Levels = new List<LevelObjectData>();

        private const string LEVEL_DATA_KEY = "LevelData";
        
        public LevelData()
        {
            
        }

        public LevelData(List<LevelObjectData> levels)
        {
            Levels = levels;
        }
 

        public string GetKey()
        {
            return LEVEL_DATA_KEY;
        }
    }
}