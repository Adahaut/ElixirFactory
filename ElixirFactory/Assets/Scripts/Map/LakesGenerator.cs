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

    public void AddLakeCase(Case newWaterCase) // Add a water case to a list of lake if it is adjacent to another water case.
    {
        HashSet<List<Case>> contactLakes = new HashSet<List<Case>>(); // In the case where a Lake case is in contact with another Lake.
        for (int i = 0; i < lakes.Count; i++)
        {
            List<Case> lake = lakes[i];
            foreach (Case waterCase in lake)
            {
                if (GetAdjacentCase(_gridModel, waterCase, false).Contains(newWaterCase.gameObject))
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


    public List<GameObject> GetAdjacentCase(GridModel gridModel, Case _case, bool includeDiagonals = false) // Get the Left, Right, Up and Down Case of a case
    {
        List<GameObject> adjacentCase = new();

        int i = _case.x;
        int j = _case.y;
        if (_gridModel.grid[i, j] == _case.gameObject)
        {
            List<Vector2Int> directions = new()
            {
                new Vector2Int(-1, 0), // Gauche
                new Vector2Int(1, 0), // Right
                new Vector2Int(0, -1), // Down
                new Vector2Int(0, 1) // Up
            };
            if (includeDiagonals)
            {
                directions.AddRange(new List<Vector2Int>
                {
                    new Vector2Int(-1, -1), // Left-Down
                    new Vector2Int(-1,  1), // Left-Up
                    new Vector2Int( 1, -1), // Right-Down
                    new Vector2Int( 1,  1)  // Right-Up
                });
            }
            foreach (Vector2Int dir in directions)
            {
                int newI = i + dir.x;
                int newJ = j + dir.y;

                if (newI >= 0 && newI < gridModel.gridSize && newJ >= 0 && newJ < gridModel.gridSize)
                {
                    adjacentCase.Add(gridModel.grid[newI, newJ]);
                }
            }
        }
        return adjacentCase;
    }

    public void DeleteWrongLakes(Case waterCase, List<Case> caseInLake) //Delete lakes that aren't 3x3 lakes.
    {
        foreach (Case waterCaseInLake in caseInLake)
        {
            if (GetAdjacentCase(_gridModel, waterCase, true).Contains(waterCase.gameObject))
            {
                
            }
        }
        
    }
    
}