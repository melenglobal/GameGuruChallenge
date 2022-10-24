using System;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _levelID;

        #endregion
        
        #region Serialized Variables

        [Space] 
        [SerializeField] private GameObject levelHolder;
        
        [SerializeField]
        private GameObject[] levelstages;
        
        

        #endregion
        

        #endregion
        
        //Dont create a new level new level platforms must be added to achivedplatfroms front
        //Calculate platformsCount and size of the level
        //Every level must have a start and end platform
        #region Event Subscriptions
        
        private void OnEnable()
        {
           SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            
        }
        
        private void UnsubscribeEvents()
        {
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnNextLevel()
        {
            
        }

        private void OnReset()
        {
            
        }

        private void OnInitializeLevel()
        {
            
        }

        #endregion
       
    }
}