using System;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int gridHeight = 100;
    [SerializeField] private int gridWidth = 100;
    [SerializeField] private Cell cellPrefab;

    private Cell[,] _boardofCells;
    
    public event Action onNextGeneration;


    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    private void PlaceCells()
    {
        _boardofCells = new Cell[gridHeight, gridWidth];
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(x, 0, z), quaternion.identity);
                cell.transform.SetParent(gameObject.transform);
                _boardofCells[x, z] = cell;
            }
        }
    }

    private void CheckNeighbors()
    {
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var numofNeighbors = 0;

                if (z + 1 < gridHeight)
                {

                }

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}