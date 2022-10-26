using Case2Folders.Scripts.Data.ValueObjects;
using UnityEngine;

namespace Case2Folders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "Level/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public LevelData Data;
    }
}