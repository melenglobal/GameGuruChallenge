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
        public UnityAction<GameStates> onChangeGameStates = delegate {  };
        
        public UnityAction onLevelInitialize = delegate { };
        
        public UnityAction onClearActiveLevel = delegate { };
        
        public UnityAction onLevelFailed = delegate { };
        
        public UnityAction onLevelSuccessful = delegate {  };
        
        public UnityAction onNextLevel = delegate {  };
        
        public UnityAction onPlay = delegate {  };
        
        public UnityAction onResetLevel = delegate {  };
        
        public UnityAction onSetCameraTarget = delegate {  };
        
        public UnityAction onStageAreaReached = delegate {  };
        
        public Func<PlatformMovementController,PlatformMovementController> onCurrentPlatformChange = controller => default;
        
        public Func<Vector3> onGetSpawnPosition = () => default;
        
        public UnityAction onPerfectClick = delegate {  };
        
        public Func<Transform,bool> onCheckCanSpawnPlatform = transform => default;
        
        public Func<Vector3> onGetCurrentPlatformPosition = () => default;
        
        public Func<Vector3,GameObject> onGetFallingBlock = vector3 => default;
        
        public UnityAction<Transform> onPlatformStop = delegate {  };
    }
}