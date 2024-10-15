using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildProperties : MonoBehaviour
{
    private Vector2 coordinates;
    public Vector2 size;

    public void SetCoordinates(Vector2 newCoordinates)
    {
        coordinates = newCoordinates;
    }
    
    public Vector2 GetCoordinates()
    {
        return coordinates;
    }
    
    public Vector2 GetSize()
    {
        return size;
    }

    public void SetCoordinatesOfBuildInGrid(GameObject[,] grid)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                grid[(int)coordinates.x + x, (int)coordinates.y + y].GetComponent<Case>().isOccupied = true;
            }
            
        }
    }
    
}
