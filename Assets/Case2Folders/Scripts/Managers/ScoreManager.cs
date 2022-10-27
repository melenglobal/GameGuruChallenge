using Case2Folders.Scripts.Data.UnityObjects;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Interfaces;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour,ICanSave,ICanLoad
    {
        #region Self Variables

        #region Private Variables

        private int _coinScore;

        private int _diamondScore;
        
        private ScoreData _data;
        
        private readonly string _dataPath = "Data/CD_Score";

        #endregion
        

        #endregion
        
        private ScoreData GetScoreData() => Resources.Load<CD_Score>(_dataPath).ScoreData;

        private void Awake()
        {
            _data = GetScoreData();
        }

        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateCoinScoreValue += OnUpdateCoinScoreValue;
            CoreGameSignals.Instance.onUpdateDiamondScoreValue += OnUpdateDiamondScoreValue;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateCoinScoreValue -= OnUpdateCoinScoreValue;
            CoreGameSignals.Instance.onUpdateDiamondScoreValue -= OnUpdateDiamondScoreValue;
        }
        private void OnDisable() => UnsubscribeEvents();
        
        #endregion

        private void OnUpdateCoinScoreValue()
        {   
            _coinScore += _data.coinScoreValue;
            UISignals.Instance.onUpdateCoinScore?.Invoke(_coinScore);
        }

        private void OnUpdateDiamondScoreValue()
        {
            _diamondScore += _data.diamondScoreValue;
            UISignals.Instance.onUpdateDiamondScore?.Invoke(_diamondScore);
        }
        
        private void SetGameScore()
        {
            UISignals.Instance.onUpdateCoinScore?.Invoke(_coinScore);
            UISignals.Instance.onUpdateDiamondScore?.Invoke(_diamondScore);
        }


        public void Save(int uniqueId)
        {
            
        }

        public void Load(int uniqueId)
        {
            
        }
    }
}