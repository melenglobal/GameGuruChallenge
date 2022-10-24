using System;
using Case2Folders.Scripts.Controllers.MovingPlatformControllers;
using Case2Folders.Scripts.Enums;
using Case2Folders.Scripts.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Case2Folders.Scripts.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameStates = delegate(GameStates arg0) {  };
        
        public UnityAction onLevelFailed = delegate { };
        
        public UnityAction onLevelSuccessful = delegate {  };
        
        public UnityAction onNextLevel = delegate {  };
        
        public UnityAction onPlay = delegate {  };
        
        public UnityAction onResetLevel = delegate {  };
        
        public UnityAction onSetCameraTarget = delegate {  };
        
        public UnityAction onStageAreaReached = delegate {  };
        
        //public UnityAction<PlatformMovementController> onCurrentPlatformChange = delegate(PlatformMovementController arg0) {  };
        
        public Func<PlatformMovementController,PlatformMovementController> onCurrentPlatformChange;
    }
}