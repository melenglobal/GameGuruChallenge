using Case2Folders.Scripts.Data.ValueObjects;
using UnityEngine;

namespace Case2Folders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Score", menuName = "Score/CD_Score", order = 0)]
    public class CD_Score : ScriptableObject
    {
        public ScoreData ScoreData;
    }
}