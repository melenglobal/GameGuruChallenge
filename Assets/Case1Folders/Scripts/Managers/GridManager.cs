using System.Collections.Generic;
using Case1Folders.Scripts.Signals;
using UnityEngine;

namespace Case1Folders.Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        [HideInInspector]
        public GameObject[,] GridArray;
        #endregion
        
        #region Private Variables
        
        private float _gridWidth;
        private float _gridHeight;
        private float defaultOffSet;
        
        

        #endregion
        
        #region Serialized Variables
        
        [SerializeField] private List<GameObject> gridPrefabs = new List<GameObject>();
        [SerializeField] private int gridSize = 10;
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private GameObject gridParent;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float offsetValueX;
        [SerializeField] private float offsetValueY;
        [SerializeField] private float individualObjectOffset = 1.1f;
        private const float cameraOffsetRatioFixer = 1.8f;
 

        #endregion
        
        #endregion

        private void Awake() => Initialize();

        private void Initialize()
        {   
            FitAllGridsToCamera();
            GridArray = new GameObject[gridSize, gridSize];
            CreateGrid();
        }
        private void OnEnable() =>  SubscribeEvents();
        private void SubscribeEvents() =>  CoreGameSignals.Instance.onGetNeighbours += SetNeighbours;
        private void UnsubscribeEvents() =>CoreGameSignals.Instance.onGetNeighbours -= SetNeighbours;
        private void OnDisable() =>UnsubscribeEvents();

        private void CreateGrid()
        {
            float defaultOffset = 0.5f;
            var gridOffsetX = (float)gridSize / 2 - defaultOffset;
            var gridOffSetY = (float)gridSize / 2 - defaultOffset;
            
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    GridArray[i, j] = Instantiate(gridPrefab, new Vector3((i - gridOffsetX) *individualObjectOffset, (j - gridOffSetY)*individualObjectOffset, 0) , Quaternion.identity);
                    GridArray[i, j].transform.SetParent(gridParent.transform);
                    GridArray[i, j].name = "GridElement " + "[" +i+ "," +j+ "]";
                    var gridElement = GridArray[i, j].GetComponent<GridElement>();
                    gridElement.x = i;
                    gridElement.y = j;
                }
            }
        }

        private void FitAllGridsToCamera()
        {
            if (Screen.width >= Screen.height)
            {
                mainCamera.orthographicSize = gridSize * 2; 
            }
            else
            {
                float width = Screen.width; // 1080
                float height = Screen.height; // 1920
                mainCamera.orthographicSize = gridSize * individualObjectOffset * (height / width) / cameraOffsetRatioFixer;
            }
        }

        private List<GridElement> SetNeighbours(GridElement gridElement)
        {
            List<GridElement> neighbours = new List<GridElement>();
            int x = gridElement.x;
            int y = gridElement.y;
            if (x > 0) //  we want to check the left side of the grid
            {
                neighbours.Add(GridArray[x - 1, y].GetComponent<GridElement>()); // left
            }
            if (x < gridSize - 1) // because we want to check the right side of the grid
            {
                neighbours.Add(GridArray[x + 1, y].GetComponent<GridElement>()); // right
            }
            if (y > 0) // we want to check the bottom side of the grid
            {
                neighbours.Add(GridArray[x, y - 1].GetComponent<GridElement>()); // down
            }
            if (y < gridSize - 1) //we want to check the top side of the grid
            {
                neighbours.Add(GridArray[x, y + 1].GetComponent<GridElement>()); // up
            }
            return neighbours;
        }

    }
    
}
