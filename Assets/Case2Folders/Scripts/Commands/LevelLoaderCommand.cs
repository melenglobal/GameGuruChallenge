using UnityEngine;

namespace Case2Folders.Scripts.Commands
{
    public class LevelLoaderCommand
    {
        private readonly GameObject _levelHolder;
        public LevelLoaderCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }
        public void Execute(int levelID)
        {
            Object.Instantiate(Resources.Load<GameObject>($"LevelPrefabs/Level"), _levelHolder.transform);
        }
    }
}