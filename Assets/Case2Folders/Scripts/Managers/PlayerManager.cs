using Case2Folders.Scripts.Controllers.PlayerControllers;
using Case2Folders.Scripts.Data.UnityObjects;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private PlayerAnimationCommand _playerAnimationCommand;
        
        private PlayerMeshCommand _playerMeshCommand;
        
        private CharacterData _characterData;
        
        #endregion
        #region Serialized Variables

        [SerializeField] 
        private PlayerMovementController playerMovementController;
        
        [SerializeField] 
        private Animator playerAnimator;
        
        [SerializeField]
        private SkinnedMeshRenderer playerMeshRenderer;

        #endregion

        #endregion

        private CharacterData GetCharacterData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _playerAnimationCommand = new PlayerAnimationCommand(playerAnimator);

            _playerMeshCommand = new PlayerMeshCommand(playerMeshRenderer);
            
            _playerMeshCommand.SetMeshVisible(true);
                
            _playerAnimationCommand.ChangeAnimationState(PlayerAnimationType.Idle);
            
            _characterData = GetCharacterData();
            
            playerMovementController.SetMovementData(_characterData.MovementData);
        }

        #region Event Subscriptions
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            CoreGameSignals.Instance.onResetLevel += OnReturnBase;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onPlatformStop += OnPlatformStop;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onResetLevel -= OnResetLevel;
            CoreGameSignals.Instance.onResetLevel -= OnReturnBase;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onPlatformStop -= OnPlatformStop;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion
        
        private void OnPlay() => ChangePlayerBehaviour(true, PlayerAnimationType.Run);
        private void OnLevelFailed() => ChangePlayerBehaviour(false, PlayerAnimationType.Fall);
        private void OnResetLevel() => ChangePlayerBehaviour(false, PlayerAnimationType.Idle);
        private void OnLevelSuccessful() => ChangePlayerBehaviour(false, PlayerAnimationType.Dance);
        private void OnNextLevel() => ChangePlayerBehaviour(true, PlayerAnimationType.Run);
        private void ChangePlayerBehaviour(bool isReadyToMove, PlayerAnimationType playerAnimationType)
        {
            _playerAnimationCommand.ChangeAnimationState(playerAnimationType);
            playerMovementController.ReadyToMove(isReadyToMove);
        }
        private void OnPlatformStop(Transform lastPlatform) => playerMovementController.SetLastPlatformTransform(lastPlatform);
        private void OnReturnBase() =>
            transform.position = CoreGameSignals.Instance.onGetCurrentPlatformPosition.Invoke();
    }
}