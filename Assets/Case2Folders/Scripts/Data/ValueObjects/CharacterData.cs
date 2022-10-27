using System;

namespace Case2Folders.Scripts.Data.ValueObjects
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
        public float LerpSpeed;
        public float RotationSpeed;
    }
}