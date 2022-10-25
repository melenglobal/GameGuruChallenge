using Case2Folders.Scripts.Enums;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerAnimationCommand
    {   
        private readonly Animator _animator;

        public PlayerAnimationCommand(Animator animator) => _animator = animator;
        
        public void ChangeAnimationState(PlayerAnimationType animationType) => _animator.SetTrigger(animationType.ToString());
    }
}