using Case2Folders.Scripts.Commands;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadCommand _loadCommand;

        private SaveCommand _saveCommand;
        
        #endregion

        #endregion
        
        private void Awake()
        {
            Init();
        }
        
        private void Init()
        {
            _loadCommand = new LoadCommand();
            _saveCommand = new SaveCommand();
        }

        #region Event Subscriptions
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveData += _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadData += _loadCommand.Execute<LevelData>;
        }
        
        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveData -= _saveCommand.Execute;
            SaveLoadSignals.Instance.onLoadData -= _loadCommand.Execute<LevelData>;
        }
        
        private void OnDisable() => UnsubscribeEvents();

        #endregion


    
    }
}