using System;
using Case2Folders.Scripts.Commands;
using Case2Folders.Scripts.Data.UnityObjects;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Interfaces;
using Case2Folders.Scripts.Signals;
using UnityEngine;

namespace Case2Folders.Scripts.Managers
{
    public class LevelManager : MonoBehaviour,ICanSave,ICanLoad
    {
        #region Self Variables

        #region Private Variables
        
        private LevelLoaderCommand _levelLoaderCommand;
        private LevelClearerCommand _levelClearerCommand;
        private LevelData _levelData;
        private int _levelID;
        private const string _levelPath = "Data/CD_Level";
        private const float _finishLineOffsetGap = .5f;
        private float _spawnPositionOffsetZ = 0f;
        private float _FinishLinePositionZ = 0f;
        private float _offsetZ = 0f;

        private int _uniqueID = 99;

        #endregion
        
        #region Serialized Variables

        [Space] 
        [SerializeField] 
        private GameObject levelHolder;
        [SerializeField] 
        private GameObject finishLineObject;
        [SerializeField] 
        private Vector3 nextFinishLinePosition;
        [SerializeField] 
        private Vector3 currentFinishLinePosition;
        [SerializeField]
        private Vector3 spawnPosition;

        #endregion
        
        #endregion

        private void Awake()
        {   
            SyncData();
            GetData();
            Init();
        }
        private void Init()
        {
            _levelLoaderCommand = new LevelLoaderCommand(ref levelHolder);
            _levelClearerCommand = new LevelClearerCommand(ref levelHolder);
        }

        private void SyncData()
        {
            _levelData = GetLevelData();
            _levelID = _levelData.LevelID;
        }
        private void GetData()
        {   
            if (!ES3.FileExists($"LevelData{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists($"LevelData{_uniqueID}"))
                {
                    SyncData();
                    Save(_uniqueID);
                }
            }
            Load(_uniqueID);

            _levelData = GetLevelData();
        }
        
        private LevelData GetLevelData() => Resources.Load<CD_Level>(_levelPath).Data;

        #region Event Subscriptions
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccess;
            CoreGameSignals.Instance.onGetSpawnPosition += OnSetLevelPlatformSpawnPosition;
            CoreGameSignals.Instance.onCheckCanSpawnPlatform += OnPlatformCanSpawn;
            CoreGameSignals.Instance.onGetCurrentPlatformPosition += OnSetCurrentPlatformPosition;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccess;
            CoreGameSignals.Instance.onGetSpawnPosition -= OnSetLevelPlatformSpawnPosition;
            CoreGameSignals.Instance.onCheckCanSpawnPlatform -= OnPlatformCanSpawn;
            CoreGameSignals.Instance.onGetCurrentPlatformPosition += OnSetCurrentPlatformPosition;
        }

        private void OnDisable() => UnsubscribeEvents();
        
        #endregion

        private void Start()
        {
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        } 

        private void OnNextLevel()
        {
            _levelID++;
            Save(_uniqueID);
            UISignals.Instance.onSetLevelText?.Invoke(_levelID +1);
            var obj = Instantiate(finishLineObject,CalculateFinishLineObjectPosition(_levelID),Quaternion.identity);
        }

        private void OnPlay()
        {
            if (_levelID!=0)
            {
                var obj = Instantiate(finishLineObject,CalculateFinishLineObjectPosition(_levelID),Quaternion.identity);
            }
            
        }
        
        private void OnLevelSuccess() => currentFinishLinePosition = nextFinishLinePosition;
        
        private bool OnPlatformCanSpawn(Transform lastPlatform) => lastPlatform.position.z < nextFinishLinePosition.z - _levelData.Levels[_levelID].PlatformZScale;
        
        private Vector3 OnSetCurrentPlatformPosition() 
        {
            if (_levelID == 0) return new Vector3(0,.5f,-1.3f);
            
            return currentFinishLinePosition;
        }

        private Vector3 OnSetLevelPlatformSpawnPosition()  
        {
            if (_levelID <= 0)
            {   
                return Vector3.zero;
            }
            _spawnPositionOffsetZ = currentFinishLinePosition.z + _levelData.Levels[_levelID].PlatformZScale - _finishLineOffsetGap;
            spawnPosition = new Vector3(0,0,_spawnPositionOffsetZ);
            return spawnPosition;
        }
        private Vector3 CalculateFinishLineObjectPosition(int levelID)
        {
            var levelObjectData = _levelData.Levels[levelID];
            var levelObjectCount = levelObjectData.PlatformCount;
            var levelObjectSize = levelObjectData.PlatformZScale;
            if (_levelID == 0)
            {
                var finishLinePositionZ = levelObjectCount * levelObjectSize - _finishLineOffsetGap;
                _FinishLinePositionZ += finishLinePositionZ;
            }
            else
            {
                var finishLinePositionZ =  levelObjectCount * levelObjectSize - _finishLineOffsetGap*2;
                _FinishLinePositionZ += finishLinePositionZ;
            }
            nextFinishLinePosition = new Vector3(nextFinishLinePosition.x,nextFinishLinePosition.y,_FinishLinePositionZ);
            return nextFinishLinePosition;
        }
        
        private void InitializeFinishLineObject()
        {
            var obj = Instantiate(finishLineObject,CalculateFinishLineObjectPosition(_levelID),Quaternion.identity);
            currentFinishLinePosition = obj.transform.position;
        }

        private void OnInitializeLevel()
        { 
          var newLevelData = _levelID % Resources.Load<CD_Level>(_levelPath).Data.Levels.Count; 
          _levelLoaderCommand.Execute(newLevelData);
          InitializeFinishLineObject();
          UISignals.Instance.onSetLevelText?.Invoke(_levelID + 1);
          CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
          CoreGameSignals.Instance.onLevelReady?.Invoke();
        }
        private void OnClearActiveLevel() => _levelClearerCommand.Execute();

        public void Save(int uniqueId)
        {
            LevelData levelData = new LevelData(_levelID);
            SaveLoadSignals.Instance.onSaveData?.Invoke(levelData,uniqueId);
        }

        public void Load(int uniqueId)
        {
            LevelData levelData = SaveLoadSignals.Instance.onLoadData?.Invoke(_levelData.GetKey(),uniqueId);

            _levelID = levelData.LevelID;
            
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Save(_uniqueID);
        }

        private void OnApplicationQuit()
        {
            Save(_uniqueID);
        }
    }
}