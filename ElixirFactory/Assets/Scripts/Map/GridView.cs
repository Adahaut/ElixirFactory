using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    public void CreateGrid(GridModel model)
    {
        for (int i = 0; i < model.gridSize; i++)
        {
            for (int j = 0; j < model.gridSize; j++)
            {
                model.grid[i, j].GetComponent<Case>().ShowCase();
            }
        }
    }
}