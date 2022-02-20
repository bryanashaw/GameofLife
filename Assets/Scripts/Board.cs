using System;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int boardHeight = 100;
    [SerializeField] private int boardWidth = 100;
    [SerializeField] private float gameSpeed = 0.1f;

    [SerializeField] private Cell cellPrefab;


    private Cell[,] _boardofCells;
    private float internalTimer;
    private bool _gamePaused = true;

    public static event Action OnNextGeneration;


    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
        // _gamePaused = true;
    }

    private void UserInput()
    {
        if (Input.GetMouseButton(0))
        {
            var rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

            _gamePaused = true;

            if (Physics.Raycast(rayCast, out var raycastHit))
            {
                var target = raycastHit.transform;
                if (target.GetComponent<Cell>())
                {
                    var cell = target.GetComponent<Cell>();
                    cell.SelectedbyMouse();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _gamePaused = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _gamePaused = !_gamePaused;
        }
    }

    private void PlaceCells()
    {
        _boardofCells = new Cell[boardHeight, boardWidth];
        for (int z = 0; z < boardHeight; z++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(x, 0, z), quaternion.identity);
                cell.transform.SetParent(gameObject.transform);
                _boardofCells[x, z] = cell;
            }
        }
    }

    private void CountNeighbors()
    {
        for (int z = 0; z < boardHeight; z++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                var numofNeighbors = 0;

                //North of Cell
                if (z + 1 < boardHeight)
                {
                    if (_boardofCells[x, z + 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //East of Cell
                if (x + 1 < boardWidth)
                {
                    if (_boardofCells[x + 1, z].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //South of Cell
                if (z - 1 >= 0)
                {
                    if (_boardofCells[x, z - 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //West of Cell
                if (x - 1 >= 0)
                {
                    if (_boardofCells[x - 1, z].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //NorthEast of Cell
                if (x + 1 < boardWidth && z + 1 < boardHeight)
                {
                    if (_boardofCells[x + 1, z + 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //NorthWest of Cell
                if (x - 1 >= 0 && z + 1 < boardHeight)
                {
                    if (_boardofCells[x - 1, z + 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //SouthEast of Cell
                if (x + 1 < boardWidth && z - 1 >= 0)
                {
                    if (_boardofCells[x + 1, z - 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                //SouthWest of Cell
                if (x - 1 >= 0 && z - 1 >= 0)
                {
                    if (_boardofCells[x - 1, z - 1].getLifeStatus())
                    {
                        numofNeighbors++;
                    }
                }

                _boardofCells[x, z].SetNeighbors(numofNeighbors);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        if (!_gamePaused)
        {
            if (internalTimer >= gameSpeed)
            {
                internalTimer = 0f;

                CountNeighbors();

                OnNextGeneration?.Invoke();
            }
            else
            {
                internalTimer += Time.deltaTime;
            }
        }
    }
}