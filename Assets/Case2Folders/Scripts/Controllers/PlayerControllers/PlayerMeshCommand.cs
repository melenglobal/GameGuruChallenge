using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerMeshCommand
    {
        private readonly SkinnedMeshRenderer _meshRenderer;

        public PlayerMeshCommand(SkinnedMeshRenderer meshRenderer)
        {
            _meshRenderer = meshRenderer;
        }
  
        public void SetMeshVisible(bool visible) => _meshRenderer.enabled = visible;
    }
}