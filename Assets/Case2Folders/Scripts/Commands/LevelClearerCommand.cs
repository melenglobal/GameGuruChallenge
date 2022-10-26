using UnityEngine;

namespace Case2Folders.Scripts.Commands
{
    public class LevelClearerCommand
    {
        private readonly GameObject _levelHolder;
        public LevelClearerCommand(ref GameObject levelHolder) => _levelHolder = levelHolder;

        public void Execute() =>  Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
    }
}