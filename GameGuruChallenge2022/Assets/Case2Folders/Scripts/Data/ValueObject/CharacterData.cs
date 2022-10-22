using System;

namespace Case2Folders.Scripts.Data.ValueObject
{   
    [Serializable]
    public struct CharacterData
    {
        public CharacterMovementData MovementData;
    }

    [Serializable]
    public struct CharacterMovementData
    {
        public float ForwardSpeed;
    }
}