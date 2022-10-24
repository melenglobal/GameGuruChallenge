using System;
using Case2Folders.Scripts.Controllers;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Signals;
using TMPro;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] 
        private UIPanelController uiPanelController;
        
        [SerializeField]
        private TextMeshProUGUI levelText;
        
        [SerializeField] 
        private TextMeshProUGUI diamondScoreText;

        [SerializeField]
        private TextMeshProUGUI coinScoreText;
        
        #endregion
        
        #endregion
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        }
        private void UnsubscribeEvents()
        {   
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onResetLevel -= OnResetLevel;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        }

        private void OnDisable() => UnsubscribeEvents();

        public void Play() => CoreGameSignals.Instance.onPlay?.Invoke();

        public void NextLevel() => CoreGameSignals.Instance.onNextLevel?.Invoke();
        
        public void Reset() => CoreGameSignals.Instance.onResetLevel?.Invoke();

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }
        
        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }
        
        private void OnResetLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }
        
        private void OnOpenPanel(UIPanels panel) => uiPanelController.OpenPanel(panel);
        private void OnClosePanel(UIPanels panel) => uiPanelController.ClosePanel(panel);
        private void OnSetLevelText(int level) => levelText.text = $"Level {level}";
        private void OnUpdateCoinScore(int coinScoreValue) => coinScoreText.text = coinScoreValue.ToString();
        private void OnUpdateDiamondScore(int diamondScoreValue) => diamondScoreText.text = diamondScoreValue.ToString();
        
    }
}