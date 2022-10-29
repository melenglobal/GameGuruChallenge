using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Case1Folders.Scripts.Enums;
using Case1Folders.Scripts.Signals;
using UnityEngine;

namespace Case1Folders.Scripts
{
    public class GridElement : MonoBehaviour
    {
        
        public List<GridElement> ClickedNeighbours;
        
        public GameObject XObject;

        public GridState GridState = GridState.UnClicked;
        
        public float gridElementSize = 1f;
        
        public int x;
        public int y;
        
        public List<GridElement> neighbours = new List<GridElement>();

        private void OnEnable()
        {
            CoreGameSignals.onGridClicked += OnCheckNeighbours;
        }

        private void OnDisable()
        {
            CoreGameSignals.onGridClicked -= OnCheckNeighbours;
        }
        private void Start()
        {
            GetNeighbours(CoreGameSignals.Instance.onGetNeighbours?.Invoke(this)); ;
        }
        private void OnMouseDown()
        {
            if (GridState != GridState.UnClicked) return;
            GridState = GridState.Clicked;
            XObject.SetActive(true);
            CoreGameSignals.onGridClicked.Invoke();
        }
        private void GetNeighbours(List<GridElement> gridElements)
        {
            neighbours = gridElements;
        }
        private void OnCheckNeighbours()
        {
            if (GridState != GridState.Clicked) return;
            
            ClickedNeighbours = neighbours.Where(x => x.GridState == GridState.Clicked).ToList();
            if (ClickedNeighbours.Count >= 2)
            {

                StartCoroutine(ClearListCoroutine());
            }
            else
            {

                ClickedNeighbours.Clear();
            }
        }
        
        private IEnumerator ClearListCoroutine()
        { 
           yield return new WaitForSeconds(0.1f);
           XObject.SetActive(false);
           GridState = GridState.UnClicked;
           foreach (var clickedNeighbour in ClickedNeighbours)
           {
               clickedNeighbour.XObject.SetActive(false);
               clickedNeighbour.GridState = GridState.UnClicked;
           }
           ClickedNeighbours.Clear();
        }
        public void ResetGrid()
        {
            GridState = GridState.UnClicked;
            XObject.SetActive(false);
            ClickedNeighbours.Clear();
        }
        
    }
}
