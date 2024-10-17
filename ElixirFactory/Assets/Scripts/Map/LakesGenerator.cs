using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LakesGenerator : MonoBehaviour
{
    GridModel _gridModel;
    private List<List<Case>> lakes = new();

    private void Awake()
    {
        _gridModel = GetComponent<GridModel>();
    }

    public void
        AddLakeCase(Case newWaterCase) // Add a water case to a list of lake if it is adjacent to another water case.
    {
        HashSet<List<Case>>
            contactLakes = new HashSet<List<Case>>(); // In the case where a Lake case is in contact with another Lake.
        for (int i = 0; i < lakes.Count; i++)
        {
            List<Case> lake = lakes[i];
            foreach (Case waterCase in lake)
            {
                if (GetAdjacentCase(_gridModel, waterCase).Contains(newWaterCase.gameObject))
                {
                    contactLakes.Add(lake);
                    break;
                }
            }
        }

        if (contactLakes.Count > 0) // If is in contact, then recreate the lake with all the adjacents water cases.
        {
            List<Case> newLake = new();
            newLake.AddRange(contactLakes);
            newLake.Add(newWaterCase);
            foreach (List<Case> contactLake in contactLakes)
            {
                lakes.Remove(contactLake);
            }

            lakes.Add(newLake);

            return;
        }

        lakes.Add(new List<Case>() { newWaterCase });
    }


    public List<GameObject>
        GetAdjacentCase(GridModel gridModel, Case _case) // Get the Left, Right, Up and Down Case of a case
    {
        List<GameObject> adjacentCase = new();

        int i = _case.x;
        int j = _case.y;
        if (gridModel.grid[i, j] == _case.gameObject)
        {
            if (i > 0)
            {
                adjacentCase.Add(gridModel.grid[i - 1, j]);
            }

            if (i < gridModel.gridSize - 1)
            {
                adjacentCase.Add(gridModel.grid[i, j]);
            }

            if (j > 0)
            {
                adjacentCase.Add(gridModel.grid[i, j - 1]);
            }

            if (j < gridModel.gridSize - 1)
            {
                adjacentCase.Add(gridModel.grid[i, j]);
            }

            return adjacentCase;
        }

        return adjacentCase;
    }


    public void DeleteWrongLakes() //Delete lakes that aren't 3x3 lakes.
    {
    }
}