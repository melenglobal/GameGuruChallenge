using System;
using UnityEngine;

namespace Case2Folders.Scripts.Controllers.PlayerControllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Debug.Log("Finish");
                //Invoke OnLevelSuccessfull
            }
            
        }
    }
}