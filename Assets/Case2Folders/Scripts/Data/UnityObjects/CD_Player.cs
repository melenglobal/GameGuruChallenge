using Case2Folders.Scripts.Data.ValueObjects;
using UnityEngine;

namespace Case2Folders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "Player/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public CharacterData Data;
    }
}