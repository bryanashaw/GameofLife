using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material aliveMaterial;
    [SerializeField] private Material deadMaterial;

    private bool isAlive;
    private int numofNeighbors;


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

    // private void SetLifeStatus(bool status)
    // {
    //     isAlive = status;
    // }

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
}