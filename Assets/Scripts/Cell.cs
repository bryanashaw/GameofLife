using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material aliveMaterial;
    [SerializeField] private Material deadMaterial;

    private bool isAlive;
    private int _numofNeighbors;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        RandomizeLife();
    }

    private void OnEnable()
    {
        Board.OnNextGeneration += PopulationControl;
    }

    private void OnDisable()
    {
        Board.OnNextGeneration -= PopulationControl;
    }

    public bool getLifeStatus()
    {
        return isAlive;
    }

    public void SetNeighbors(int num)
    {
        _numofNeighbors = num;
    }

    public void SelectedbyMouse()
    {
        SetLifeStatus(true);
    }

    private void PopulationControl()
    {
        if (isAlive)
        {
            if (_numofNeighbors != 2 && _numofNeighbors != 3)
            {
                SetLifeStatus(false);
            }
        }
        else
        {
            if (_numofNeighbors == 3)
            {
                SetLifeStatus(true);
            }
        }
    }

    private void SetLifeStatus(bool status)
    {
        isAlive = status;
        if (isAlive)
        {
            _meshRenderer.material = aliveMaterial;
        }
        else
        {
            _meshRenderer.material = deadMaterial;
        }
    }

    private void RandomizeLife()
    {
        var randNumber = Random.Range(0, 100);

        if (randNumber > 50)
        {
            SetLifeStatus(true);
        }
        else
        {
            SetLifeStatus(false);
        }
    }
}