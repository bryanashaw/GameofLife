using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material aliveMaterial;
    [SerializeField] private Material deadMaterial;

    private bool isAlive;
    private int numofNeighbors;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        RandomizeLife();
    }

    private void OnEnable()
    {
        GetComponent<Board>().onNextGeneration += PopulationControl;
    }

    private void OnDisable()
    {
        GetComponent<Board>().onNextGeneration -= PopulationControl;
    }

    public bool getLifeStatus()
    {
        return isAlive;
    }

    public void SetNeighbors(int num)
    {
        numofNeighbors = num;
    }

    private void PopulationControl()
    {
        if (isAlive)
        {
            if (numofNeighbors != 2 && numofNeighbors != 3)
            {
                isAlive = false;
            }
        }
        else
        {
            if (numofNeighbors == 3)
            {
                isAlive = true;
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