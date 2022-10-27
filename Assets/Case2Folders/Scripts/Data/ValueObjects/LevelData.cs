using System;
using System.Collections.Generic;
using Case2Folders.Scripts.Interfaces;
using UnityEngine;

namespace Case2Folders.Scripts.Data.ValueObjects
{   
    [Serializable]
    public class LevelData : ISaveableEntity
    {
        
        public List<LevelObjectData> Levels = new List<LevelObjectData>();
        
        public int LevelID;
        

        private const string LEVEL_DATA_KEY = "LevelData";
        
        public LevelData()
        {
            
        }

        public LevelData(int levelID)
        {
            LevelID = levelID;
        }


        public string GetKey()
        {
            return LEVEL_DATA_KEY;
        }
    }
}