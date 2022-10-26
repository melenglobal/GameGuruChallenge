using Case2Folders.Scripts.Commands;
using Case2Folders.Scripts.Data.UnityObjects;
using Case2Folders.Scripts.Data.ValueObjects;
using Case2Folders.Scripts.Interfaces;
using Case2Folders.Scripts.Signals;
using UnityEditor;
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

        #endregion
        
        #region Serialized Variables

        [Space] 
        [SerializeField] private GameObject levelHolder;

        [SerializeField] private GameObject finishLineObject;
        
        [SerializeField] private Vector3 finishLinePosition;
        
        private const float _finishLineOffsetGap = 2.4f; // Spawnerposition finisline.position.z + GAP !
        
        private const float _finishlineObjectHeight = 0.5f;
        
        private float _FinishLinePositionZ = 0f;
        #endregion
        

        #endregion

        private void Awake()
        {
            Debug.Log(finishLinePosition);
            _levelID = GetActiveLevel();
            
            _levelData = GetLevelData();
            
            Init();
        }

        private void Init()
        {
            _levelLoaderCommand = new LevelLoaderCommand(ref levelHolder);
            
            _levelClearerCommand = new LevelClearerCommand(ref levelHolder);
        }

        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;

            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }
        
        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>(_levelPath).Data;
        }
        
        #region Event Subscriptions
        
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            CoreGameSignals.Instance.OnGetSpawnPosition += OnSetLevelPlatformSpawnPosition;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onResetLevel -= OnResetLevel;
            CoreGameSignals.Instance.OnGetSpawnPosition -= OnSetLevelPlatformSpawnPosition;
        }

        private void OnDisable() => UnsubscribeEvents();
        
        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }
        
        private void OnLevelSuccessful()
        {
            
        }
        
        private void OnNextLevel()
        {
            _levelID++;
            Instantiate(finishLineObject,CalculateFinishLineObjectPositionZ(_levelID),Quaternion.identity);
        }

        private void OnResetLevel()
        {
            
        }
        
        private Vector3 CalculateFinishLineObjectPositionZ(int levelID){
            
            var levelObjectData = _levelData.Levels[levelID];
            var levelObjectCount = levelObjectData.PlatformCount;
            var levelObjectSize = levelObjectData.PlatformZScale;
            
            var finishLinePositionZ = levelObjectCount * levelObjectSize +_finishLineOffsetGap; 
            _FinishLinePositionZ += finishLinePositionZ;
            finishLinePosition = new Vector3(finishLinePosition.x,_finishlineObjectHeight,_FinishLinePositionZ);
            return finishLinePosition;
            
            // Level 0 spawn position = 0
            // Level 1 spawn position = finislinepositionZ + finisLineOffsetGap
            // Level 2 spawn position = finishlinepositionZ + finishLineOffsetGap
        }
        
        private Vector3 OnSetLevelPlatformSpawnPosition() 
        {
            if (_levelID <= 0)
            {
                return Vector3.zero;
            }
                
            var positionZ = finishLinePosition.z + _finishLineOffsetGap;
            var spawnPosition = new Vector3(0,0,positionZ);
            return spawnPosition;
        }

        private void OnInitializeLevel()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>(_levelPath).Data.Levels.Count;
            _levelLoaderCommand.Execute(newLevelData);
            Instantiate(finishLineObject,CalculateFinishLineObjectPositionZ(_levelID),Quaternion.identity);
        }

        private void OnClearActiveLevel() => _levelClearerCommand.Execute();

        public void Save(int uniqueId) => SaveLoadSignals.Instance.onSaveData?.Invoke(_levelData,_levelID);

        public void Load(int uniqueId) => SaveLoadSignals.Instance.onLoadData?.Invoke(_levelData.GetKey(),_levelID);

    }
}